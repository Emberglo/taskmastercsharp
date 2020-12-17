using System;
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
    public class ListTaskController : ControllerBase
    {

        private readonly ListTaskService _lts;

        public ListTaskController(ListTaskService lts)
        {
            _lts = lts;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ListTask>> Post([FromBody] ListTask newLt)
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                newLt.CreatorId = userInfo.Id;
                return Ok(_lts.Create(newLt));
            }
            catch (Exception err)
            {
                return BadRequest(err.Message);
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> Delete(int id)
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                return Ok(_lts.Delete(id, userInfo.Id));
            }
            catch (Exception err)
            {
                return BadRequest(err.Message);
            }
        }
    }
}