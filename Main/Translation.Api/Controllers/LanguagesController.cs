// Copyright 2020, Vladislav Harudka. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License in the project root for license information.


using System;
using System.Threading.Tasks;
using Harudka.Translation.Api.Service;
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

        // GET
        // api/languages
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                var response = await _languageService.GetAsync();

                return new ObjectResult(response)
                {
                    StatusCode = StatusCodes.Status200OK
                };
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occured while processing the request");
            }
        }
    }
}
