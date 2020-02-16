using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dto;
using API.Models;
using API.Services;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripsController : ControllerBase
    {
        private readonly ITravelerRepository _eventRepository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;

        public TripsController(ITravelerRepository eventRepository, IMapper mapper, LinkGenerator linkGenerator)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
        }
        [HttpGet]
        public async Task<ActionResult<TripsDto[]>> Get()
        {
            try
            {
                var results = await _eventRepository.GetTrips();

                var mappedEntities = _mapper.Map<TripsDto[]>(results);
                return Ok(mappedEntities);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }


        [HttpGet("{tripId}")]
        public async Task<ActionResult<TripsDto>> Get(int tripId)
        {
            try
            {
                var result = await _eventRepository.GetTrip(tripId);

                if (result == null) return NotFound();

                var mappedEntity = _mapper.Map<TripsDto>(result);
                return Ok(mappedEntity);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPost]
        public async Task<ActionResult<TripsDto>> Post(TripsDto dto)
        {
            try
            {
                var mappedEntity = _mapper.Map<Trips>(dto);
                _eventRepository.Add(mappedEntity);

                if (await _eventRepository.Save())
                {
                    var location = _linkGenerator.GetPathByAction("Get", "Trips", new { mappedEntity.Id });
                    return Created(location, _mapper.Map<TripsDto>(mappedEntity));
                }
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.InnerException.Message);
            }

            return BadRequest();
        }


        [HttpPut("{tripId}")]
        public async Task<ActionResult<TripsDto>> Put(int tripId, TripsDto dto)
        {
            try
            {
                var oldtrip = await _eventRepository.GetTrip(tripId);
                if (oldtrip == null) return NotFound($"Could not find trip with id {tripId}");

                var newtrip = _mapper.Map(dto, oldtrip);
                _eventRepository.Update(newtrip);
                if (await _eventRepository.Save())
                {
                    return NoContent();
                }
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest();
        }

        [HttpDelete("{tripId}")]
        public async Task<IActionResult> Delete(int tripId)
        {
            try
            {
                var oldtrip = await _eventRepository.GetTrip(tripId);
                if (oldtrip == null) return NotFound($"Could not find trip with id {tripId}");

                _eventRepository.Delete(oldtrip);
                if (await _eventRepository.Save())
                {
                    return NoContent();
                }
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest();
        }
    }
}