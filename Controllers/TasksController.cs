using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using taskmastercsharp.Models;
using taskmastercsharp.Services;

namespace taskmastercsharp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class TasksController : ControllerBase
    {

        private readonly TasksService _ts;

        public TasksController(TasksService ts)
        {
            _ts = ts;
        }

        [HttpGet]
        public ActionResult<IEnumerable<MyTask>> Get()
        {
            try
            {
                return Ok(_ts.Get());
            }
            catch (Exception err)
            {
                return BadRequest(err.Message);
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<TasksController>> Create([FromBody] MyTask newTask)
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                newTask.CreatorId = userInfo.Id;
                MyTask created = _ts.Create(newTask);
                created.Creator = userInfo;
                return Ok(created);
            }
            catch (Exception err)
            {
                return BadRequest(err.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<MyTask>> Edit(int id, [FromBody] MyTask editTask)
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                editTask.Id = id;
                return Ok(_ts.Edit(editTask, userInfo.Id));
            }
            catch (Exception err)
            {
                return BadRequest(err.Message);
            }
        }
    }
}