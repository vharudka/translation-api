// Copyright 2020, Vladislav Harudka. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License in the project root for license information.


using AutoMapper;
using Harudka.Translation.Api.Domain;
using Harudka.Translation.Api.Dto;
using Harudka.Translation.Api.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Harudka.Translation.Api.Controllers
{
    [ApiController]
    [Route("api/languages")]
    public class LanguageController : ControllerBase
    {
        private readonly ILanguageRepository _languageRepository;
        private readonly IMapper _mapper;

        public LanguageController(ILanguageRepository languageRepository, IMapper mapper)
        {
            _languageRepository = languageRepository;
            _mapper = mapper;
        }

        // OPTIONS
        // api/languages

        /// <summary>
        /// Retrieves allow header
        /// </summary>
        /// <returns>An empty response with allow header for language</returns>
        /// <response code="200">Returns allow header for language</response>

        [HttpOptions]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult GetOptions()
        {
            Response.Headers.Add("Allow", "OPTIONS, POST, PUT, GET, DELETE");

            return Ok();
        }

        // POST
        // api/languages

        /// <summary>
        /// Creates a new language
        /// </summary>
        /// <param name="languageForCreationDto">Request model</param>
        /// <returns>A response with a created language</returns>
        /// <response code="201">If a language was successfully created</response>
        /// <response code="422">If there was a validation error</response>
        /// <response code="500">If there was an internal server error</response>

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<LanguageDto>> CreateAsync([FromBody] LanguageForCreationDto languageForCreationDto)
        {
            var languageForCreation = _mapper.Map<Language>(languageForCreationDto);

            var language = await _languageRepository.CreateAsync(languageForCreation);

            var languageDto = _mapper.Map<LanguageDto>(language);

            return CreatedAtRoute("GetLanguage", new { id = languageDto.Id }, languageDto);
        }

        // PUT
        // api/languages/1


        /// <summary>
        /// Updates an existing language
        /// </summary>
        /// <param name="id">language id</param>
        /// <param name="languageForUpdatingDto">Request model</param>
        /// <returns>A response with no content</returns>
        /// <response code="204">If a language was successfully updated</response>
        /// <response code="422">If there was a validation error</response>
        /// <response code="404">If a language does not exists</response>
        /// <response code="500">If there was an internal server error</response>

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateAsync(short id, [FromBody] LanguageForUpdatingDto languageForUpdatingDto)
        {
            var language = await _languageRepository.GetAsync(id);

            if(language == null)
            {
                return NotFound();
            }

            _mapper.Map(languageForUpdatingDto, language);

            await _languageRepository.UpdateAsync(language);

            return NoContent();
        }

        // GET HEAD
        // api/languages/1

        /// <summary>
        /// Retrieves a language by id
        /// </summary>
        /// <param name="id">language id</param>
        /// <returns>A response with a language</returns>
        /// <response code="200">If a language was successfully found</response>
        /// <response code="404">If a language does not exists</response>
        /// <response code="500">If there was an internal server error</response>

        [HttpGet("{id}", Name = "GetLanguage")]
        [HttpHead("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<LanguageDto>> GetAsync(short id)
        {
            var language = await _languageRepository.GetAsync(id);

            if(language == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<LanguageDto>(language));
        }

        // GET
        // api/languages

        /// <summary>
        /// Retrieves languages
        /// </summary>
        /// <returns>A response with a list of languages</returns>
        /// <response code="200">If a languages were successfully returned</response>
        /// <response code="500">If there was an internal server error</response>

        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IReadOnlyList<LanguageDto>>> GetAllAsync()
        {
            var languages = await _languageRepository.GetAllAsync();

            return Ok(_mapper.Map<IReadOnlyList<LanguageDto>>(languages));
        }

        // DELETE
        // api/languages/1

        /// <summary>
        /// Deletes a language by id
        /// </summary>
        /// <param name="id">language id</param>
        /// <returns>A response with no content</returns>
        /// <response code="204">If a language was successfully deleted</response>
        /// <response code="404">If a language does not exists</response>
        /// <response code="500">If there was an internal server error</response>

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteAsync(short id)
        {
            var language = await _languageRepository.GetAsync(id);

            if(language == null)
            {
                return NotFound();
            }

            await _languageRepository.DeleteAsync(language);

            return NoContent();
        }
    }
}
