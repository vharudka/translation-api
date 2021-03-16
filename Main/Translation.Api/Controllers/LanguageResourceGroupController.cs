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
    [Route("api/applications/{applicationId}/language-resource-groups")]
    public class LanguageResourceGroupController : ControllerBase
    {
        private readonly ILanguageResourceGroupRepository _languageResourceGroupRepository;
        private readonly IMapper _mapper;

        public LanguageResourceGroupController(ILanguageResourceGroupRepository languageResourceGroupRepository,
                                               IMapper mapper)
        {
            _languageResourceGroupRepository = languageResourceGroupRepository;
            _mapper = mapper;
        }

        // POST
        // api/applications/e507b516-77d7-48a4-53d1-08d8e4f05701/language-resource-groups

        /// <summary>
        /// Creates a new application language resource group
        /// </summary>
        /// <param name="applicationId">application id</param>
        /// <param name="languageResourceGroupForCreationDto">Request model</param>
        /// <returns>A response with a created application language resource group</returns>
        /// <response code="201">If an application language resource group was successfully created</response>
        /// <response code="422">If there was a validation error</response>
        /// <response code="500">If there was an internal server error</response>

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<LanguageResourceGroupDto>> CreateAsync(
            [FromRoute] string applicationId,
            [FromBody] LanguageResourceGroupForCreationDto languageResourceGroupForCreationDto)
        {
            var languageResourceGroupForCreation = _mapper.Map<LanguageResourceGroup>(applicationId, languageResourceGroupForCreationDto);

            var languageResourceGroup = await _languageResourceGroupRepository.CreateAsync(languageResourceGroupForCreation);

            var createdApplicationLanguageDto = _mapper.Map<LanguageResourceGroupDto>(languageResourceGroup);

            return CreatedAtRoute("GetApplicationLanguageResourceGroup",
                                  new { applicationId = languageResourceGroupForCreation.ApplicationId, languageResourceGroupId = createdApplicationLanguageDto.Id },
                                  createdApplicationLanguageDto);
        }

        // GET HEAD
        // api/applications/e507b516-77d7-48a4-53d1-08d8e4f05701/language-resource-groups/1

        /// <summary>
        /// Retrieves an application language resource group by applicationId and language resource group id
        /// </summary>
        /// <param name="applicationId">application id</param>
        /// <param name="languageResourceGroupId">language resource group id</param>
        /// <returns>A response with an application language resource group</returns>
        /// <response code="200">If an application language resource group was successfully found</response>
        /// <response code="404">If an application language resource group does not exists</response>
        /// <response code="500">If there was an internal server error</response>

        [HttpGet("{languageResourceGroupId}", Name = "GetApplicationLanguageResourceGroup")]
        [HttpHead("{languageResourceGroupId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<LanguageResourceGroupDto>> GetAsync(
            Guid applicationId, int languageResourceGroupId)
        {
            var languageResourceGroup = await _languageResourceGroupRepository.GetAsync(applicationId, languageResourceGroupId);

            if(languageResourceGroup == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<LanguageResourceGroupDto>(languageResourceGroup));
        }

        // GET
        // api/applications/e507b516-77d7-48a4-53d1-08d8e4f05701/language-resource-groups

        /// <summary>
        /// Retrieves application language resource groups
        /// </summary>
        /// <param name="applicationId">application id</param>
        /// <returns>A response with a list of application language resource groups</returns>
        /// <response code="200">If an application language resource groups were successfully returned</response>
        /// <response code="500">If there was an internal server error</response>

        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IReadOnlyList<LanguageResourceGroupDto>>> GetAllAsync(Guid applicationId)
        {
            var languageResourceGroups = await _languageResourceGroupRepository.GetAllAsync(applicationId);

            return Ok(_mapper.Map<IReadOnlyList<LanguageResourceGroupDto>>(languageResourceGroups));
        }

        // DELETE
        // api/applications/e507b516-77d7-48a4-53d1-08d8e4f05701/language-resource-groups/1

        /// <summary>
        /// Deletes an application language resource group by applicationId and language resource group id
        /// </summary>
        /// <param name="applicationId">application id</param>
        /// <param name="languageResourceGroupId">language resource group id</param>
        /// <returns>A response with no content</returns>
        /// <response code="204">If an application language resource group was successfully deleted</response>
        /// <response code="404">If an application language resource group does not exists</response>
        /// <response code="500">If there was an internal server error</response>

        [HttpDelete("{languageResourceGroupId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteAsync(Guid applicationId, int languageResourceGroupId)
        {
            var languageResourceGroup = await _languageResourceGroupRepository.GetAsync(applicationId, languageResourceGroupId);

            if(languageResourceGroup == null)
            {
                return NotFound();
            }

            await _languageResourceGroupRepository.DeleteAsync(languageResourceGroup);

            return NoContent();
        }
    }
}
