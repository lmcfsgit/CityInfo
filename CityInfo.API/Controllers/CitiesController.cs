﻿using CityInfo.API.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using CityInfo.API.Models;
using System.Collections.Generic;
using AutoMapper;

namespace CityInfo.API.Controllers
{

    [Route("api/cities")]
    public class CitiesController : Controller
    {
        private ICityInfoRepository _cityInfoRepository;

        public CitiesController(ICityInfoRepository cityInfoRepository)
        {
            _cityInfoRepository = cityInfoRepository;
        }
        public IActionResult GetCities()
        {
            var cityEntities = _cityInfoRepository.GetCities();
            
            //var results = new List<CityWithoutPointsOfInterestDto>();

            //foreach (var cityEntity in cityEntities)
            //{
            //    results.Add(new CityWithoutPointsOfInterestDto
            //    {
            //        Id = cityEntity.Id,
            //        Description = cityEntity.Description,
            //        Name = cityEntity.Name
            //    });
            //}
            var results = Mapper.Map<IEnumerable<CityWithoutPointsOfInterestDto>>(cityEntities);
            return Ok(results);
        }
        
        [HttpGet("{id}")]
        public IActionResult GetCity(int id, bool includePointsOfInterest = false)
        {

            var city = _cityInfoRepository.GetCity(id, includePointsOfInterest);

            if (city == null)
            {
                return NotFound();
            }

            if (includePointsOfInterest)
            {
                var cityResult = Mapper.Map<CityDto>(city);
                //var cityResult = new CityDto()
                //{
                //    Id = city.Id,
                //    Name = city.Name,
                //    Description = city.Description
                //};

                //foreach (var poi in city.PointsOfInterest)
                //{
                //    cityResult.PointsOfInterest.Add(
                //        new PointOfInterestDto()
                //        {
                //            Id = poi.Id,
                //            Name = poi.Name,
                //            Description = poi.Description
                //        });
                //}
                return Ok(cityResult);
            }

            var cityWithoutPointsOfInterest = Mapper.Map<CityWithoutPointsOfInterestDto>(city);
            //var cityWithoutPointsOfInterest =
            //    new CityWithoutPointsOfInterestDto()
            //    {
            //        Id = city.Id,
            //        Name = city.Name,
            //        Description = city.Description
            //    };


            return Ok(cityWithoutPointsOfInterest);
            // find city
            //var cityToReturn = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == id);

            //if (cityToReturn == null)
            //{
            //    return NotFound();
            //}

            //return Ok(cityToReturn);

        }

    }
}
