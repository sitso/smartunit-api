using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartUnitApi.Repositories;
using SmartUnitApi.Entities;
using SmartUnitApi.Models;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace SmartUnitApi.Controllers
{
    [Route("api/units")]
    public class UnitsController : Controller
    {
        IUnitRepository repo;
        public UnitsController(IUnitRepository repo)
        {
            this.repo = repo;
        }

        // GET units by id
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(repo.Get(id));
        }

        // GET units by area id
        [HttpGet("byarea/{id}")]
        public IActionResult GetAllByAreaId(int id)
        {
            return Ok(repo.GetAllUnitsByArea(id));
        }

        //GET units by user id
        [HttpGet("byuser/{id}")]
        public IActionResult GetAllByUserId(int id)
        {
            return Ok(repo.GetAllUnitsByUser(id));
        }

        // GET units by municipality id
        [HttpGet("bymunicipality/{id}")]
        public IActionResult GetAllByMunicipality(int id)
        {
            return Ok(repo.GetAllUnitsByMunicipality(id));
        }

        // GET units by location
        [HttpGet("bylocation/{longitude}&{latitude}")]
        public IActionResult GetAllByLocation(float longitude, float latitude)
        {
            return Ok(repo.GetAllUnitsByLocation(longitude, latitude));
        }

        // POST add new unit
        [HttpPost]
        public IActionResult Post([FromBody]Unit unit)
        {
            var newUnit = repo.Create(unit);
            if (newUnit.Status == RepositoryActionStatus.Created)
            {
                PushNotifications.DataChangeNotification(newUnit, "Unit added");
                return Created(Request.Path + "/" + newUnit.Model.UnitId, newUnit);
            }
            return BadRequest();
        }

        // PUT update unit
        [HttpPut]
        public IActionResult Put([FromBody]Unit unit)
        {
            var updatedUnit = repo.Update(unit);
            if(updatedUnit.Status == RepositoryActionStatus.Updated)
            {
                PushNotifications.DataChangeNotification(updatedUnit, "Unit modified");
                return Ok(updatedUnit);
            }
            return NoContent();
        }

        // DELETE delete unit
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deletedUnit = repo.Delete(id);
            if (deletedUnit.Status == RepositoryActionStatus.Deleted)
            {
                PushNotifications.DataChangeNotification(deletedUnit, "Unit removed");
                return NoContent();
            }
            else
                return NotFound();
        }
    }
}
