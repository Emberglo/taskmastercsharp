using System.Collections.Generic;
using taskmastercsharp.Models;
using taskmastercsharp.Repositories;

namespace taskmastercsharp.Services
{
    public class ListsService
    {
        private readonly ListsRepository _repo;

        public ListsService(ListsRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<MyList> GetAll()
        {
            return _repo.GetAll();
        }

        public MyList Create(MyList newMyList)
        {
            newMyList.Id = _repo.Create(newMyList);
            return newMyList;
        }

    }
}