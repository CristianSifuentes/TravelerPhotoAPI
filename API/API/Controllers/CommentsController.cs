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
    public class CommentsController : ControllerBase
    {
        private readonly ITravelerRepository _eventRepository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;

        public CommentsController(ITravelerRepository eventRepository, IMapper mapper, LinkGenerator linkGenerator)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
        }
        [HttpGet]
        public async Task<ActionResult<CommentsDto[]>> Get()
        {
            try
            {
                var results = await _eventRepository.GetComments();

                var mappedEntities = _mapper.Map<CommentsDto[]>(results);
                return Ok(mappedEntities);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpGet("{commentId}")]
        public async Task<ActionResult<CommentsDto>> Get(int commentId)
        {
            try
            {
                var result = await _eventRepository.GetComment(commentId);

                if (result == null) return NotFound();

                var mappedEntity = _mapper.Map<CommentsDto>(result);
                return Ok(mappedEntity);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPost]
        public async Task<ActionResult<CommentsDto>> Post(CommentsDto dto)
        {
            try
            {
                var mappedEntity = _mapper.Map<Comments>(dto);
                _eventRepository.Add(mappedEntity);

                if (await _eventRepository.Save())
                {
                    var location = _linkGenerator.GetPathByAction("Get", "Comments", new { mappedEntity.Id });
                    return Created(location, _mapper.Map<CommentsDto>(mappedEntity));
                }
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.InnerException.Message);
            }

            return BadRequest();
        }

    }
}