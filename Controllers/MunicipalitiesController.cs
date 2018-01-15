using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartUnitApi.Entities;
using SmartUnitApi.Repositories;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace SmartUnitApi.Controllers
{
    [Route("api/[controller]")]
    public class MunicipalitiesController : Controller
    {
        IMunicipalityRepository repo;
        public MunicipalitiesController(IMunicipalityRepository repo)
        {
            this.repo = repo;
        }
        // GET: api/values
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var locations = repo.GetAllByUser(id);
            if(locations != null)
            {
                return Ok(locations);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpGet("ids/{id}")]
        public IActionResult GetPermissionIds(int id)
        {
            return Ok(repo.GetMunicipalitiesIdsByUser(id));
        }
        [HttpPost]
        public IActionResult Post([FromBody] List<UserMunicipality> userMunicipalities)
        {
            try
            {
                if (userMunicipalities == null)
                {
                    return BadRequest();
                }

                var result = repo.CreateMultiple(userMunicipalities);
                if (result.Status == RepositoryActionStatus.Created)
                {

                    return Created("api/permissions", userMunicipalities);
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
