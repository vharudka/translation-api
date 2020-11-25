// Copyright 2020, Vladislav Harudka. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License in the project root for license information.


using AutoMapper;
using Harudka.Translation.Api.Domain;
using Harudka.Translation.Api.Dto;
using Harudka.Translation.Api.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Harudka.Translation.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApplicationsController : ControllerBase
    {
        private readonly IApplicationRepository _applicationRepository;
        private readonly IMapper _mapper;

        public ApplicationsController(IApplicationRepository applicationRepository, IMapper mapper)
        {
            _applicationRepository = applicationRepository;
            _mapper = mapper;
        }

        // OPTIONS
        // api/applications

        /// <summary>
        /// Retrieves allow header
        /// </summary>
        /// <returns>An empty response with allow header for application</returns>
        /// <response code="200">Returns allow header for application</response>

        [HttpOptions]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult GetOptions()
        {
            Response.Headers.Add("Allow", "OPTIONS, POST, PUT, GET, DELETE");

            return Ok();
        }

        // POST
        // api/applications

        /// <summary>
        /// Creates a new application
        /// </summary>
        /// <param name="applicationForCreationDto">Request model</param>
        /// <returns>A response with a created application</returns>
        /// <response code="201">If an application was successfully created</response>
        /// <response code="422">If there was a validation error</response>
        /// <response code="500">If there was an internal server error</response>

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApplicationDto>> CreateAsync([FromBody] ApplicationForCreationDto applicationForCreationDto)
        {
            var applicationForCreation = _mapper.Map<Application>(applicationForCreationDto);

            var application = await _applicationRepository.CreateAsync(applicationForCreation);

            var applicationDto = _mapper.Map<ApplicationDto>(application);

            return CreatedAtRoute("GetApplication", new { id = applicationDto.Id }, applicationDto);
        }

        // PUT
        // api/applications/1


        /// <summary>
        /// Updates an existing application
        /// </summary>
        /// <param name="id">application id</param>
        /// <param name="applicationForUpdatingDto">Request model</param>
        /// <returns>A response with no content</returns>
        /// <response code="204">If an application was successfully updated</response>
        /// <response code="422">If there was a validation error</response>
        /// <response code="404">If an application is not exists</response>
        /// <response code="500">If there was an internal server error</response>

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateAsync(Guid id, [FromBody] ApplicationForUpdatingDto applicationForUpdatingDto)
        {
            var application = await _applicationRepository.GetAsync(id);

            if(application == null)
            {
                return NotFound();
            }

            _mapper.Map(applicationForUpdatingDto, application);

            await _applicationRepository.UpdateAsync(application);

            return NoContent();
        }

        // GET HEAD
        // api/applications/1

        /// <summary>
        /// Retrieves an application by id
        /// </summary>
        /// <param name="id">application id</param>
        /// <returns>A response with an application</returns>
        /// <response code="200">If an application was successfully found</response>
        /// <response code="404">If an application is not exists</response>
        /// <response code="500">If there was an internal server error</response>

        [HttpGet("{id}", Name = "GetApplication")]
        [HttpHead("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApplicationDto>> GetAsync(Guid id)
        {
            var application = await _applicationRepository.GetAsync(id);

            if(application == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ApplicationDto>(application));
        }

        // GET
        // api/applications

        /// <summary>
        /// Retrieves applications
        /// </summary>
        /// <returns>A response with a list of applications</returns>
        /// <response code="200">If an applications were successfully returned</response>
        /// <response code="500">If there was an internal server error</response>

        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IReadOnlyList<ApplicationDto>>> GetAllAsync()
        {
            var applications = await _applicationRepository.GetAllAsync();

            return Ok(_mapper.Map<IReadOnlyList<ApplicationDto>>(applications));
        }

        // DELETE
        // api/applications/1

        /// <summary>
        /// Deletes an application by id
        /// </summary>
        /// <param name="id">application id</param>
        /// <returns>A response with no content</returns>
        /// <response code="204">If an application was successfully deleted</response>
        /// <response code="404">If an application is not exists</response>
        /// <response code="500">If there was an internal server error</response>

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            var application = await _applicationRepository.GetAsync(id);

            if(application == null)
            {
                return NotFound();
            }

            await _applicationRepository.DeleteAsync(application);

            return NoContent();
        }
    }
}
