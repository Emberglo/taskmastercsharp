using System.Collections.Generic;
using System.Data;
using Dapper;
using taskmastercsharp.Models;

namespace taskmastercsharp.Repositories
{
    public class TasksRepository
    {
        private readonly IDbConnection _db;

        private readonly string populateCreator = "SELECT task.*, profile.* FROM tasks task INNER JOIN profiles profile ON task.creatorId = profile.id";

        public TasksRepository(IDbConnection db)
        {
            _db = db;
        }

        public int Create(MyTask newTask)
        {
            string sql = @"
            INSERT INTO tasks
            (body, creatorId)
            VALUES
            (@Body, @CreatorId);
            SELECT LAST_INSERT_ID();";
            return _db.ExecuteScalar<int>(sql, newTask);
        }

        internal IEnumerable<MyTask> GetTasksByProfile(string profileId)
        {
            string sql = @"
            SELECT
            task.*
            p.*
            FROM tasks task
            JOIN profiles p ON task.creatorId = p.id
            WHERE task.creatorId = profileId;";
            return _db.Query<MyTask, Profile, MyTask>(sql, (task, profile) => { task.Creator = profile; return task; }, new { profileId }, splitOn: "id");
        }

        internal MyTask GetOne(int id)
        {
            string sql = @"
            SELECT *
            FROM tasks
            WHERE id = @id";
            return _db.QueryFirstOrDefault<MyTask>(sql, new { id });
        }

        internal void Edit(MyTask editData)
        {
            string sql = @"
            UPDATE tasks
            SET
            body = @Body
            WHERE id = @Id;";
            _db.Execute(sql, editData);
        }

        public IEnumerable<MyTask> Get()
        {
            string sql = populateCreator;
            return _db.Query<MyTask, Profile, MyTask>(sql, (task, profile) => { task.Creator = profile; return task; }, splitOn: "id");
        }
    }
}