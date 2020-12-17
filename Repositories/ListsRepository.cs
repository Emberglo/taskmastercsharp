using System.Collections.Generic;
using System.Data;
using Dapper;
using taskmastercsharp.Models;

namespace taskmastercsharp.Repositories
{
    public class ListsRepository
    {
        private readonly IDbConnection _db;

        private readonly string populateCreator = "SELECT list.*, profile.*, FROM lists list INNER JOIN profiles profile ON list.CreatorId = profile.id";

        public ListsRepository(IDbConnection db)
        {
            _db = db;
        }

        public IEnumerable<MyList> GetAll()
        {
            string sql = populateCreator;
            return _db.Query<MyList, Profile, MyList>(sql, (MyList, profile) => { MyList.Creator = profile; return MyList; }, splitOn: "id");
        }

        public int Create(MyList newMyList)
        {
            string sql = @"
            INSERT INTO lists
            (title, creatorId)
            VALUES
            (@Title, @CreatorId)
            SELECT LAST_INSERT_ID()";
            return _db.ExecuteScalar<int>(sql, newMyList);
        }

    }
}