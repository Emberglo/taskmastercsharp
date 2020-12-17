using System.Collections.Generic;
using System.Data;
using Dapper;
using taskmastercsharp.Models;

namespace taskmastercsharp.Repositories
{
    public class ListTaskRepository
    {

        private readonly IDbConnection _db;

        public ListTaskRepository(IDbConnection db)
        {
            _db = db;
        }

        public int Create(ListTask newLt)
        {
            string sql = @"
            INSERT INTO listtask
            (listId, taskId, creatorId)
            VALUES
            (@ListId, @TaskId, @CreatorId);
            SELECT LAST_INSERT_ID();";
            return _db.ExecuteScalar<int>(sql, newLt);
        }

        internal IEnumerable<MyTask> GetTasksByListId(int listId)
        {
            string sql = @"
            SELECT l.*,
            lt.id as ListTaskId,
            p.*
            FROM listtask lt
            JOIN tasks t ON t.id = lt.taskId
            JOIN profiles p ON p.id = l.creatorId
            WHERE listId = @listId;";
            return _db.Query<ListTaskViewModel, Profile, ListTaskViewModel>(sql, (task, profile) => { task.Creator = profile; return task; }, new { listId }, splitOn: "id");
        }

        internal bool Remove(int id)
        {
            string sql = "DELETE from listtask WHERE id = @id";
            int valid = _db.Execute(sql, new { id });
            return valid > 0;
        }

        internal ListTask GetOne(int id)
        {
            string sql = @"SELECT * from listtask WHERE id = @id";
            return _db.QueryFirstOrDefault<ListTask>(sql, new { id });
        }
    }
}