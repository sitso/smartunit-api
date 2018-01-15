using Microsoft.AspNetCore.Mvc;
using SmartUnitApi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartUnitApi.Controllers
{
    [Route("api/counties")]
    public class CountiesController : Controller
    {
        ICountyRepository repo;
        public CountiesController(ICountyRepository repo)
        {
            this.repo = repo;
        }

        [HttpGet]
        public IActionResult Get()
        {
            if (repo.GetAll().Any())
            {
                return Ok(repo.GetAll());
            }
            else
                return NotFound(); 
        } 
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var counties = repo.Get(id);
            return Ok(counties);
        }
    }
}
