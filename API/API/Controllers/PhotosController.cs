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
    public class PhotosController : ControllerBase
    {
        private readonly ITravelerRepository _eventRepository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;

        public PhotosController(ITravelerRepository eventRepository, IMapper mapper, LinkGenerator linkGenerator)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
        }

        [HttpGet]
        public async Task<ActionResult<PhotosDto[]>> Get()
        {
            try
            {
                var results = await _eventRepository.GetPhotos();

                var mappedEntities = _mapper.Map<PhotosDto[]>(results);
                return Ok(mappedEntities);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpGet("{photoId}")]
        public async Task<ActionResult<PhotosDto>> Get(int photoId)
        {
            try
            {
                var result = await _eventRepository.GetPhoto(photoId);

                if (result == null) return NotFound();

                var mappedEntity = _mapper.Map<PhotosDto>(result);
                return Ok(mappedEntity);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPost]
        public async Task<ActionResult<PhotosDto>> Post(PhotosDto dto)
        {
            try
            {
                var mappedEntity = _mapper.Map<Photos>(dto);
                _eventRepository.Add(mappedEntity);

                if (await _eventRepository.Save())
                {
                    var location = _linkGenerator.GetPathByAction("Get", "Photos", new { mappedEntity.Id });
                    return Created(location, _mapper.Map<PhotosDto>(mappedEntity));
                }
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.InnerException.Message);
            }

            return BadRequest();
        }


        [HttpPut("{photoId}")]
        public async Task<ActionResult<PhotosDto>> Put(int photoId, PhotosDto dto)
        {
            try
            {
                var oldPhoto = await _eventRepository.GetPhoto(photoId);
                if (oldPhoto == null) return NotFound($"Could not find Photo with id {photoId}");

                var newPhoto = _mapper.Map(dto, oldPhoto);
                _eventRepository.Update(newPhoto);
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

        [HttpDelete("{photoId}")]
        public async Task<IActionResult> Delete(int photoId)
        {
            try
            {
                var oldPhoto = await _eventRepository.GetPhoto(photoId);
                if (oldPhoto == null) return NotFound($"Could not find Photo with id {photoId}");

                _eventRepository.Delete(oldPhoto);
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