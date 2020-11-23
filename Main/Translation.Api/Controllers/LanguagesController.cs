// Copyright 2020, Vladislav Harudka. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License in the project root for license information.


using AutoMapper;
using Harudka.Translation.Api.Domain;
using Harudka.Translation.Api.Dto;
using Harudka.Translation.Api.Service;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Harudka.Translation.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LanguagesController : ControllerBase
    {
        private readonly ILanguageService _languageService;
        private readonly IMapper _mapper;

        public LanguagesController(ILanguageService languageService, IMapper mapper)
        {
            _languageService = languageService;
            _mapper = mapper;
        }

        // OPTIONS
        // api/languages
        [HttpOptions]
        public ActionResult GetOptions()
        {
            Response.Headers.Add("Allow", "OPTIONS, POST, PUT, GET, DELETE");

            return Ok();
        }

        // POST
        // api/languages
        [HttpPost]
        public async Task<ActionResult<LanguageDto>> CreateAsync([FromBody] LanguageForCreationDto languageForCreationDto)
        {
            var languageForCreation = _mapper.Map<Language>(languageForCreationDto);

            var language = await _languageService.CreateAsync(languageForCreation);

            var languageDto = _mapper.Map<LanguageDto>(language);

            return CreatedAtRoute(nameof(GetAsync), new { id = languageDto.Id }, languageDto);
        }

        // PUT
        // api/languages/1
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAsync(short id, [FromBody] LanguageForUpdatingDto languageForUpdatingDto)
        {
            var language = await _languageService.GetAsync(id);

            if(language == null)
            {
                return NotFound();
            }

            _mapper.Map(languageForUpdatingDto, language);

            await _languageService.UpdateAsync(language);

            return NoContent();
        }

        // GET
        // api/languages/1
        [HttpGet("{id}", Name = nameof(GetAsync))]
        [HttpHead("{id}")]
        public async Task<ActionResult<LanguageDto>> GetAsync(short id)
        {
            var language = await _languageService.GetAsync(id);

            if(language == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<LanguageDto>(language));
        }

        // GET
        // api/languages
        [HttpGet]
        [HttpHead]
        public async Task<ActionResult<IReadOnlyList<LanguageDto>>> GetAllAsync()
        {
            var languages = await _languageService.GetAllAsync();

            return Ok(_mapper.Map<IReadOnlyList<LanguageDto>>(languages));
        }

        // DELETE
        // api/languages/1
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(short id)
        {
            var language = await _languageService.GetAsync(id);

            if(language == null)
            {
                return NotFound();
            }

            await _languageService.DeleteAsync(language);

            return NoContent();
        }
    }
}
