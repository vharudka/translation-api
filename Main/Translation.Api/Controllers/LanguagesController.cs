// Copyright 2020, Vladislav Harudka. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License in the project root for license information.


using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Harudka.Translation.Api.Domain;
using Harudka.Translation.Api.Dto;
using Harudka.Translation.Api.Service;
using Harudka.Translation.Api.Validators;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Harudka.Translation.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LanguagesController : ControllerBase
    {
        private readonly ILanguageService _languageService;
        private readonly IMapper _mapper;

        public LanguagesController(ILanguageService languageService, IMapper mapper)
        {
            _languageService = languageService;
            _mapper = mapper;
        }

        // POST
        // api/languages
        [HttpPost]
        public async Task<ActionResult<LanguageDto>> CreateAsync([FromBody] LanguageForCreationDto languageForCreationDto)
        {
            var validator = new LanguageForCreationDtoValidator();
            var validationResult = validator.Validate(languageForCreationDto);

            if(!validationResult.IsValid)
            {
                return BadRequest();
            }

            var languageForCreation = _mapper.Map<Language>(languageForCreationDto);

            var language = await _languageService.CreateAsync(languageForCreation);

            var languageDto = _mapper.Map<LanguageDto>(language);

            return CreatedAtRoute(nameof(GetAsync), new { id = languageDto.Id }, languageDto);
        }

        // PUT
        // api/languages/1
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(short id, [FromBody] LanguageForUpdatingDto languageForUpdatingDto)
        {
            var validator = new LanguageForUpdatingDtoValidator();
            var validationResult = validator.Validate(languageForUpdatingDto);

            if(!validationResult.IsValid)
            {
                return BadRequest();
            }

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
        public async Task<ActionResult<IReadOnlyList<LanguageDto>>> GetAllAsync()
        {
            var languages = await _languageService.GetAllAsync();

            return Ok(_mapper.Map<IReadOnlyList<LanguageDto>>(languages));
        }

        // DELETE
        // api/languages/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(short id)
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
