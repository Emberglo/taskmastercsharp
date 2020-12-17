using System;
using System.Collections.Generic;
using taskmastercsharp.Models;
using taskmastercsharp.Repositories;

namespace taskmastercsharp.Services
{
    public class ListTaskService
    {
        private readonly ListTaskRepository _repo;

        public ListTaskService(ListTaskRepository repo)
        {
            _repo = repo;
        }

        public ListTask Create(ListTask newLt)
        {
            newLt.Id = _repo.Create(newLt);
            return newLt;
        }

        internal IEnumerable<MyTask> GetTasksByListId(int listId)
        {
            return _repo.GetTasksByListId(listId);
        }

        internal string Delete(int id, string userId)
        {
            ListTask original = _repo.GetOne(id);
            if (original == null) { throw new Exception("Bad ID"); };
            if (original.CreatorId != userId)
            {
                throw new Exception("Not your List : Access Denied");
            }
            if (_repo.Remove(id))
            {
                return "Great Success!";
            }
            return "Much Failure.";
        }
    }
}