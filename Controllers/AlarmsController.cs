using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartUnitApi.Repositories;
using SmartUnitApi.Entities;
using SmartUnitApi;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace SmartUnitApi.Controllers
{
    [Route("api/alarms")]
    public class AlarmsController : Controller
    {

        IAlarmRepository repo;
        public AlarmsController(IAlarmRepository repo)
        {
            this.repo = repo;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(repo.Get(id));
        }

        [HttpGet("byunit/{id}")]
        public IActionResult GetByUnitId(int id)
        {
            if (repo.GetAlarmsByUnitId(id).Any())
            {
                return Ok(repo.GetAlarmsByUnitId(id));
            }
            else
                return NotFound();
        }

        [HttpGet("logbyunit/{id}")]
        public IActionResult GetLogByUnitId(int id)
        {
            if (repo.GetAlarmLogByUnitId(id).Any())
            {
                return Ok(repo.GetAlarmLogByUnitId(id));
            }
            else
                return NotFound();
        }

        [HttpGet("logbyalarm/{id}")]
        public IActionResult GetLogByAlarmId(int id)
        {
            if (repo.GetAlarmLogByAlarmId(id).Any())
            {
                return Ok(repo.GetAlarmLogByAlarmId(id));
            }
            else
                return NotFound();
        }

        public IActionResult GetLogByAlarmId(int id, int amount)
        {
            if (repo.GetAlarmLogByAlarmId(id, amount).Any())
            {
                return Ok(repo.GetAlarmLogByAlarmId(id, amount));
            }
            else
                return NotFound();
        }

        [HttpGet("byuser/{id}")]
        public IActionResult GetByUserId(int id)
        {
            if (repo.GetAlarmsByUserId(id).Any())
            {
                return Ok(repo.GetAlarmsByUserId(id));
            }
            else
                return NotFound();
        }
        [HttpGet("types")]
        public IActionResult GetAlarmTypes()
        {
            if (repo.GetAllAlarmTypes().Any())
            {
                return Ok(repo.GetAllAlarmTypes());
            }
            else
                return NotFound();
        }
        // POST api/values
        [HttpPost("alarmlogvalue")]
        public IActionResult Post([FromBody]AlarmLog alarmlogvalue)
        {
            var newAlarmLogValue = repo.AddAlarmLogValue(alarmlogvalue);
            if (newAlarmLogValue.Status == RepositoryActionStatus.Created)
            {
                PushNotifications.AlarmNotification(alarmlogvalue);
                return Ok();
            }
            else
                return BadRequest();
        }

        // PUT api/values/5
        [HttpPut]
        public IActionResult Update([FromBody]Alarm alarm)
        {
            var newAlarm = repo.Update(alarm);

            if(newAlarm.Status == RepositoryActionStatus.Updated)
            {
                return Ok();
            }
            else
                return NoContent();
        } 

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
