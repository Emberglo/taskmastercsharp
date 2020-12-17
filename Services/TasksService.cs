using System;
using System.Collections.Generic;
using System.Linq;
using taskmastercsharp.Models;
using taskmastercsharp.Repositories;

namespace taskmastercsharp.Services
{
    public class TasksService
    {

        private readonly TasksRepository _repo;

        public TasksService(TasksRepository repo)
        {
            _repo = repo;
        }

        public MyTask Create(MyTask newTask)
        {
            newTask.Id = _repo.Create(newTask);
            return newTask;
        }

        public IEnumerable<MyTask> Get()
        {
            return _repo.Get();
        }

        internal IEnumerable<MyTask> GetTasksByProfile(string profileId, string userId)
        {
            return _repo.GetTasksByProfile(profileId).ToList().FindAll(t => t.CreatorId == userId);
        }

        internal MyTask Edit(MyTask editData, string userId)
        {
            MyTask original = _repo.GetOne(editData.Id);
            if (original == null) { throw new Exception("Bad Id"); }
            if (original.CreatorId != userId)
            {
                throw new Exception("Not your task : Access Denied");
            }
            _repo.Edit(editData);
            return _repo.GetOne(editData.Id);
        }

    }
}