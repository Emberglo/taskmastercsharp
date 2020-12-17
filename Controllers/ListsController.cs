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
    public class ListsController : ControllerBase
    {
        private readonly ListsService _ls;

        private readonly ListTaskService _lts;

        public ListsController(ListsService ls, ListTaskService lts)
        {
            _ls = ls;
            _lts = lts;
        }

        [HttpGet]
        public ActionResult<IEnumerable<MyList>> GetAll()
        {
            try
            {
                return Ok(_ls.GetAll());
            }
            catch (Exception err)
            {
                return BadRequest(err.Message);
            }
        }

        [HttpGet("{listId}/listtasks")]
        public ActionResult<IEnumerable<MyList>> GetOne(int listId)
        {
            try
            {
                return Ok(_lts.GetTasksByListId(listId));
            }
            catch (Exception err)
            {
                return BadRequest(err.Message);
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<MyList>> Create([FromBody] MyList newMyList)
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                newMyList.CreatorId = userInfo.Id;
                MyList created = _ls.Create(newMyList);
                created.Creator = userInfo;
                return Ok(created);
            }
            catch (Exception err)
            {
                return BadRequest(err.Message);
            }
        }

    }
}