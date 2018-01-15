using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartUnitApi.Repositories;
using SmartUnitApi.Entities;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace SmartUnitApi.Controllers
{
    [Route("api/users")]
    public class UsersController : Controller
    {
        IUserRepository repo;
        public UsersController(IUserRepository repo)
        {
            this.repo = repo;  
        }

        [HttpPost]
        public IActionResult Post([FromBody] User user)
        {
            try
            {
                if (user == null)
                {
                    return BadRequest();
                }

                var result = repo.Create(user);
                if (result.Status == RepositoryActionStatus.Created)
                {
                    
                    return Created(Request.Path +
                        result.Model.UserId.ToString(), user);
                }
                return BadRequest();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

    }
}
