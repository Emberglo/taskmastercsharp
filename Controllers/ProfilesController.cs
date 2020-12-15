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
    [Route("[controller]")]
    //NOTE - Link is https://localhost:5001/profiles
    public class ProfilesController : ControllerBase
    {
        private readonly ProfilesService _ps;

        public ProfilesController(ProfilesService ps)
        {
            _ps = ps;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<Profile>> Get()
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                return Ok(_ps.GetOrCreateProfile(userInfo));
            }
            catch (Exception err)
            {
                return BadRequest(err.Message);
            }
        }


    }
}