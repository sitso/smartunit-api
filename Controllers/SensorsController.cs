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
    [Route("api/sensors")]
    public class SensorsController : Controller
    {
        ISensorRepository repo;

        public SensorsController(ISensorRepository repo)
        {
            this.repo = repo;
        }
        // GET sensor by sensorid
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(repo.Get(id));
        }

        // GET sensors by unit id
        [HttpGet("byunit/{id}")]
        public IActionResult GetSensorsByUnitId(int id)
        {
            if (repo.GetByUnitId(id).Any())
            {
                return Ok(repo.GetByUnitId(id));
            }
            else
                return NotFound();
        }

        // GET sensorlog by sensor id
        [HttpGet("sensorlog/bysensor/{id}")]
        public IActionResult SensorLogBySensorId(int id )
        {
            if (repo.GetSensorLogBySensorId(id).Any())
            {
                return Ok(repo.GetSensorLogBySensorId(id));
            }
            else
                return NotFound();
        }
        [HttpGet("sensorlog/bysensor/{id}&{amount}")]
        public IActionResult SensorLogBySensorId(int id, int amount)
        {
            if (repo.GetSensorLogBySensorId(id, amount).Any())
            {
                return Ok(repo.GetSensorLogBySensorId(id, amount));
            }
            else
                return NotFound();
           
        }
        // GET sensorlog by unit id
        //sensors/sensorlog/byunitid
        [HttpGet("sensorlog/byunit/{id}")]
        public IActionResult SensorLogByUnitId(int id)
        {
            if (repo.GetSensorLogByUnitId(id).Any())
            {
                return Ok(repo.GetSensorLogByUnitId(id));
            }
            else
                return NotFound();
            
        }
        [HttpGet("types")]
        public IActionResult GetSensorTypes()
        {
            if (repo.GetSensorTypes().Any())
            {
                return Ok(repo.GetSensorTypes());
            }
            else
                return NotFound();
            
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]Sensor sensor)
        {
            var newSensor = repo.Create(sensor);
            if (newSensor.Status == RepositoryActionStatus.Created)
            {
                return Created(Request.Path + "/" + newSensor.Model.SensorId, newSensor);
            }
            else
                return BadRequest();
        }

        [HttpPost("sensorlog")]
        public IActionResult Post([FromBody]SensorLog sensorlog)
        {
            var newSensorLog = repo.AddSensorValue(sensorlog);
            if (newSensorLog.Status == RepositoryActionStatus.Created)
            {
                PushNotifications.DataChangeNotification(sensorlog, "Sensor data logged");
                return Ok();
            }
            else
                return BadRequest();
        }

        //delete 
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deletedSensor = repo.Delete(id);
            if(deletedSensor.Status == RepositoryActionStatus.Deleted)
            {
                return NoContent();
            }
            return NotFound();
        }
    }
}
