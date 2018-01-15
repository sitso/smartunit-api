using Microsoft.AspNetCore.Mvc;
using SmartUnitApi.Entities;
using SmartUnitApi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartUnitApi.Controllers
{
    [Route("api/permissions")]

    public class PermissionsController : Controller
    {
        IPermissionRepository repo;
        public PermissionsController(IPermissionRepository repo)
        {
            this.repo = repo;
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(repo.GetAllPermissions());
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(repo.GetUserPermissions(id));
        }
        [HttpGet("ids/{id}")]
        public IActionResult GetPermissionIds(int id)
        {
            return Ok(repo.GetPermissionIdsByUser(id));
        }
        [HttpPost]
        public IActionResult Post([FromBody] List<UserPermissions> userPermissions)
        {
            try
            {
                if (userPermissions == null)
                {
                    return BadRequest();
                }

                var result = repo.CreateMultiple(userPermissions);
                if (result.Status == RepositoryActionStatus.Created)
                {

                    return Created("api/permissions", userPermissions);
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
