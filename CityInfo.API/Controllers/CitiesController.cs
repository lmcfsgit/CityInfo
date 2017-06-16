using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CityInfo.API.Models;

namespace CityInfo.API.Controllers
{

    [Route("api/cities")]
    public class CitiesController : Controller
    {

        public IActionResult GetCities()
        {

            return Ok(CitiesDataStore.Current.Cities); 
            //return new JsonResult(new List<object>()
            //{
            //    new { id=1, Name="New York"},
            //    new { id=2, Name="Antwerp"}
            //});
    
        }
        
        [HttpGet("{id}")]
        public IActionResult GetCity(int id)
        {
            // find city
            var cityToReturn = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == id);

            if (cityToReturn == null)
            {
                return NotFound();
            }

            return Ok(cityToReturn);

        }

    }
}
