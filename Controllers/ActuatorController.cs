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
    [Route("api/actuators")]
    public class ActuatorController : Controller
    {
        IActuatorRepository repo;
        public ActuatorController(IActuatorRepository repo)
        {
            this.repo = repo;
        }

        // GET actuator by id
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(repo.Get(id));
        }

        // GET actuators by unit id
        [HttpGet("byunit/{id}")]
        public IActionResult ActuatorsByUnitId(int id)
        {
            if (repo.GetByUnitId(id).Any())
            {
                return Ok(repo.GetByUnitId(id));
            }
            else
                return NotFound();
        }

        // GET actuatorlog by actuator id
        [HttpGet("actuatorlog/byactuator/{id}")]
        public IActionResult ActuatorLogByActuatorId(int id)
        {
            if (repo.GetActuatorLogById(id).Any())
            {
                return Ok(repo.GetActuatorLogById(id));
            }
            else
                return NotFound();
        }

        // GET actuatorlog by unit id
        //actuators/actuatorlog/byunitid
        [HttpGet("actuatorlog/byunit/{id}")]
        public IActionResult ActuatorLogByUnitId(int id)
        {
            if (repo.GetActuatorLogByUnitId(id).Any())
            {
                return Ok(repo.GetActuatorLogByUnitId(id));
            }
            else
                return NotFound();
        }

        // POST create actuator
        [HttpPost]
        public IActionResult Post([FromBody]Actuator actuator)
        {
            var newActuator = repo.Create(actuator);
            if (newActuator.Status == RepositoryActionStatus.Created)
            {
                PushNotifications.DataChangeNotification(actuator, "Actuator added");
                return Created(Request.Path + "/" + newActuator.Model.ActuatorId, newActuator);
            }
            return BadRequest();
        }

        [HttpPut("{id}")]
        public IActionResult Put([FromBody]Actuator actuator)
        {
            var updatedActuator = repo.Update(actuator);
            if (updatedActuator.Status == RepositoryActionStatus.Updated)
            {
                PushNotifications.DataChangeNotification(updatedActuator, "Actuator state changed");
                return Ok(updatedActuator);
            }
            return NoContent();
        }


        // DELETE actuator by actuator id
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deletedActuator = repo.Delete(id);
            if(deletedActuator.Status == RepositoryActionStatus.Deleted)
            {
                PushNotifications.DataChangeNotification(deletedActuator, "Actuator removed");
                return NoContent();
            }
            return NotFound();
        }
    }
}
