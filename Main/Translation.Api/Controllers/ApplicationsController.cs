// Copyright 2020, Vladislav Harudka. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License in the project root for license information.


using AutoMapper;
using Harudka.Translation.Api.Domain;
using Harudka.Translation.Api.Dto;
using Harudka.Translation.Api.Extentions;
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
        private readonly IApplicationLanguageRepository _applicationLanguageRepository;
        private readonly IMapper _mapper;

        public ApplicationsController(IApplicationRepository applicationRepository,
                                      IApplicationLanguageRepository applicationLanguageRepository,
                                      IMapper mapper)
        {
            _applicationRepository = applicationRepository;
            _applicationLanguageRepository = applicationLanguageRepository;
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
        public async Task<ActionResult<ApplicationDto>> CreateApplicationAsync([FromBody] ApplicationForCreationDto applicationForCreationDto)
        {
            var applicationForCreation = _mapper.Map<Application>(applicationForCreationDto);

            var application = await _applicationRepository.CreateAsync(applicationForCreation);

            var applicationDto = _mapper.Map<ApplicationDto>(application);

            return CreatedAtRoute("GetApplication", new { applicationId = applicationDto.Id }, applicationDto);
        }

        // PUT
        // api/applications/1

        /// <summary>
        /// Updates an existing application
        /// </summary>
        /// <param name="applicationId">application id</param>
        /// <param name="applicationForUpdatingDto">Request model</param>
        /// <returns>A response with no content</returns>
        /// <response code="204">If an application was successfully updated</response>
        /// <response code="422">If there was a validation error</response>
        /// <response code="404">If an application does not exists</response>
        /// <response code="500">If there was an internal server error</response>

        [HttpPut("{applicationId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateApplicationAsync(Guid applicationId, [FromBody] ApplicationForUpdatingDto applicationForUpdatingDto)
        {
            var application = await _applicationRepository.GetAsync(applicationId);

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
        /// <param name="applicationId">application id</param>
        /// <returns>A response with an application</returns>
        /// <response code="200">If an application was successfully found</response>
        /// <response code="404">If an application does not exists</response>
        /// <response code="500">If there was an internal server error</response>

        [HttpGet("{applicationId}", Name = "GetApplication")]
        [HttpHead("{applicationId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApplicationDto>> GetApplicationAsync(Guid applicationId)
        {
            var application = await _applicationRepository.GetAsync(applicationId);

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
        public async Task<ActionResult<IReadOnlyList<ApplicationDto>>> GetAllApplicationsAsync()
        {
            var applications = await _applicationRepository.GetAllAsync();

            return Ok(_mapper.Map<IReadOnlyList<ApplicationDto>>(applications));
        }

        // DELETE
        // api/applications/1

        /// <summary>
        /// Deletes an application by id
        /// </summary>
        /// <param name="applicationId">application id</param>
        /// <returns>A response with no content</returns>
        /// <response code="204">If an application was successfully deleted</response>
        /// <response code="404">If an application does not exists</response>
        /// <response code="500">If there was an internal server error</response>

        [HttpDelete("{applicationId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteApplicationAsync(Guid applicationId)
        {
            var application = await _applicationRepository.GetAsync(applicationId);

            if(application == null)
            {
                return NotFound();
            }

            await _applicationRepository.DeleteAsync(application);

            return NoContent();
        }

        // POST
        // api/applications/1/languages

        /// <summary>
        /// Creates a new application language
        /// </summary>
        /// <param name="applicationId">application id</param>
        /// <param name="applicationLanguageForCreationDto">Request model</param>
        /// <returns>A response with a created application language</returns>
        /// <response code="201">If an application language was successfully created</response>
        /// <response code="422">If there was a validation error</response>
        /// <response code="500">If there was an internal server error</response>

        [HttpPost("{applicationId}/Languages")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApplicationLanguageDto>> CreateApplicationLanguageAsync(
            [FromRoute] string applicationId,
            [FromBody] ApplicationLanguageForCreationDto applicationLanguageForCreationDto)
        {
            var applicationLanguageForCreation = _mapper.Map<ApplicationLanguage>(applicationId, applicationLanguageForCreationDto);

            var applicationLanguage = await _applicationLanguageRepository.CreateAsync(applicationLanguageForCreation);

            var createdApplicationLanguageDto = _mapper.Map<ApplicationLanguageDto>(applicationLanguage);

            return CreatedAtRoute("GetApplicationLanguage",
                                  new { applicationId = applicationLanguageForCreation.ApplicationId, languageId = createdApplicationLanguageDto.LanguageId},
                                  createdApplicationLanguageDto);
        }

        // GET HEAD
        // api/applications/1/languages/1

        /// <summary>
        /// Retrieves an application language by applicationId and languageId
        /// </summary>
        /// <param name="applicationId">application id</param>
        /// <param name="languageId">language id</param>
        /// <returns>A response with an application language</returns>
        /// <response code="200">If an application language was successfully found</response>
        /// <response code="404">If an application language does not exists</response>
        /// <response code="500">If there was an internal server error</response>

        [HttpGet("{applicationId}/Languages/{languageId}", Name = "GetApplicationLanguage")]
        [HttpHead("{applicationId}/Languages/{languageId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApplicationLanguageDto>> GetApplicationLanguageAsync(Guid applicationId, short languageId)
        {
            var applicationLanguage = await _applicationLanguageRepository.GetAsync(applicationId, languageId);

            if(applicationLanguage == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ApplicationLanguageDto>(applicationLanguage));
        }

        // GET
        // api/applications

        /// <summary>
        /// Retrieves application languages
        /// </summary>
        /// <param name="applicationId">application id</param>
        /// <returns>A response with a list of application languages</returns>
        /// <response code="200">If an application languages were successfully returned</response>
        /// <response code="500">If there was an internal server error</response>

        [HttpGet("{applicationId}/Languages")]
        [HttpHead("{applicationId}/Languages")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IReadOnlyList<ApplicationLanguageDto>>> GetAllApplicationLanguagesAsync(Guid applicationId)
        {
            var applicationLanguages = await _applicationLanguageRepository.GetAllAsync(applicationId);

            return Ok(_mapper.Map<IReadOnlyList<ApplicationLanguageDto>>(applicationLanguages));
        }

        // DELETE
        // api/applications/1

        /// <summary>
        /// Deletes an application language by applicationId and languageId
        /// </summary>
        /// <param name="applicationId">application id</param>
        /// <param name="languageId">language id</param>
        /// <returns>A response with no content</returns>
        /// <response code="204">If an application language was successfully deleted</response>
        /// <response code="404">If an application language does not exists</response>
        /// <response code="500">If there was an internal server error</response>

        [HttpDelete("{applicationId}/Languages/{languageId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteApplicationLanguageAsync(Guid applicationId, short languageId)
        {
            var applicationLanguage = await _applicationLanguageRepository.GetAsync(applicationId, languageId);

            if(applicationLanguage == null)
            {
                return NotFound();
            }

            await _applicationLanguageRepository.DeleteAsync(applicationLanguage);

            return NoContent();
        }
    }
}
