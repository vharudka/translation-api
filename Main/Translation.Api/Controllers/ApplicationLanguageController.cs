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
    [Route("api/applications/{applicationId}/languages")]
    public class ApplicationLanguageController : ControllerBase
    {
        private readonly IApplicationLanguageRepository _applicationLanguageRepository;
        private readonly IMapper _mapper;

        public ApplicationLanguageController(IApplicationLanguageRepository applicationLanguageRepository,
                                             IMapper mapper)
        {
            _applicationLanguageRepository = applicationLanguageRepository;
            _mapper = mapper;
        }

        // POST
        // api/applications/e507b516-77d7-48a4-53d1-08d8e4f05701/languages

        /// <summary>
        /// Creates a new application language
        /// </summary>
        /// <param name="applicationId">application id</param>
        /// <param name="applicationLanguageForCreationDto">Request model</param>
        /// <returns>A response with a created application language</returns>
        /// <response code="201">If an application language was successfully created</response>
        /// <response code="422">If there was a validation error</response>
        /// <response code="500">If there was an internal server error</response>

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApplicationLanguageDto>> CreateAsync(
            [FromRoute] string applicationId,
            [FromBody] ApplicationLanguageForCreationDto applicationLanguageForCreationDto)
        {
            var applicationLanguageForCreation = _mapper.Map<ApplicationLanguage>(applicationId, applicationLanguageForCreationDto);

            var applicationLanguage = await _applicationLanguageRepository.CreateAsync(applicationLanguageForCreation);

            var createdApplicationLanguageDto = _mapper.Map<ApplicationLanguageDto>(applicationLanguage);

            return CreatedAtRoute("GetApplicationLanguage",
                                  new { applicationId = applicationLanguageForCreation.ApplicationId, languageId = createdApplicationLanguageDto.LanguageId },
                                  createdApplicationLanguageDto);
        }

        // GET HEAD
        // api/applications/e507b516-77d7-48a4-53d1-08d8e4f05701/languages/1

        /// <summary>
        /// Retrieves an application language by applicationId and languageId
        /// </summary>
        /// <param name="applicationId">application id</param>
        /// <param name="languageId">language id</param>
        /// <returns>A response with an application language</returns>
        /// <response code="200">If an application language was successfully found</response>
        /// <response code="404">If an application language does not exists</response>
        /// <response code="500">If there was an internal server error</response>

        [HttpGet("{languageId}", Name = "GetApplicationLanguage")]
        [HttpHead("{languageId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApplicationLanguageDto>> GetAsync(Guid applicationId, short languageId)
        {
            var applicationLanguage = await _applicationLanguageRepository.GetAsync(applicationId, languageId);

            if(applicationLanguage == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ApplicationLanguageDto>(applicationLanguage));
        }

        // GET
        // api/applications/e507b516-77d7-48a4-53d1-08d8e4f05701/languages

        /// <summary>
        /// Retrieves application languages
        /// </summary>
        /// <param name="applicationId">application id</param>
        /// <returns>A response with a list of application languages</returns>
        /// <response code="200">If an application languages were successfully returned</response>
        /// <response code="500">If there was an internal server error</response>

        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IReadOnlyList<ApplicationLanguageDto>>> GetAllAsync(Guid applicationId)
        {
            var applicationLanguages = await _applicationLanguageRepository.GetAllAsync(applicationId);

            return Ok(_mapper.Map<IReadOnlyList<ApplicationLanguageDto>>(applicationLanguages));
        }

        // DELETE
        // api/applications/e507b516-77d7-48a4-53d1-08d8e4f05701/languages/1

        /// <summary>
        /// Deletes an application language by applicationId and languageId
        /// </summary>
        /// <param name="applicationId">application id</param>
        /// <param name="languageId">language id</param>
        /// <returns>A response with no content</returns>
        /// <response code="204">If an application language was successfully deleted</response>
        /// <response code="404">If an application language does not exists</response>
        /// <response code="500">If there was an internal server error</response>

        [HttpDelete("{languageId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteAsync(Guid applicationId, short languageId)
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
