// Copyright 2020, Vladislav Harudka. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License in the project root for license information.


using System;
using System.Threading.Tasks;
using Harudka.Translation.Api.Models.Requests;
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

        public LanguagesController(ILanguageService languageService)
        {
            _languageService = languageService;
        }

        // POST
        // api/languages
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateOrUpdateLanguageRequest createLanguageRequest)
        {
            var validator = new CreateOrUpdateLanguageRequestValidator();
            var validationResult = validator.Validate(createLanguageRequest);

            if(!validationResult.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            var response = await _languageService.CreateAsync(createLanguageRequest);

            return new ObjectResult(response)
            {
                StatusCode = StatusCodes.Status201Created
            };
        }

        // PUT
        // api/languages/1
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(short id, [FromBody] CreateOrUpdateLanguageRequest request)
        {
            var validator = new CreateOrUpdateLanguageRequestValidator();
            var validationResult = validator.Validate(request);

            if(!validationResult.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            var language = await _languageService.GetOneAsync(id);

            if(language == null)
            {
                return NotFound();
            }

            var response = await _languageService.UpdateAsync(request, language);

            return new ObjectResult(response)
            {
                StatusCode = StatusCodes.Status200OK
            };
        }

        // GET
        // api/languages
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var response = await _languageService.GetAsync();

            return new ObjectResult(response)
            {
                StatusCode = StatusCodes.Status200OK
            };
        }

        // DELETE
        // api/languages
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(short id)
        {
            var language = await _languageService.GetOneAsync(id);

            if(language == null)
            {
                return NotFound();
            }

            var response = await _languageService.DeleteAsync(language);

            return new ObjectResult(response)
            {
                StatusCode = StatusCodes.Status200OK
            };
        }
    }
}
