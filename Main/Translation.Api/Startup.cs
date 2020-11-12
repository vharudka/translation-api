// Copyright 2020, Vladislav Harudka. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License in the project root for license information.


using AutoMapper;
using FluentValidation.AspNetCore;
using Harudka.Translation.Api.Data;
using Harudka.Translation.Api.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace Harudka.Translation.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationContext>(options =>
            {
                options.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"])
                       .EnableSensitiveDataLogging();
            });

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped<ILanguageService, LanguageService>();

            services.AddControllers()
                    .AddFluentValidation(options =>
                    {
                        options.RegisterValidatorsFromAssemblyContaining<Startup>();
                    })
                    .ConfigureApiBehaviorOptions(setupAction =>
                    {
                        setupAction.InvalidModelStateResponseFactory = context =>
                        {
                            var problemDetailsFactory = context.HttpContext.RequestServices.GetRequiredService<ProblemDetailsFactory>();
                            var problemDetails = problemDetailsFactory.CreateValidationProblemDetails(context.HttpContext, context.ModelState);

                            problemDetails.Detail = "See the errors field for details.";
                            problemDetails.Instance = context.HttpContext.Request.Path;

                            var actionExecutingContext = context as ActionExecutingContext;

                            if((context.ModelState.ErrorCount > 0) &&
                               (actionExecutingContext?.ActionArguments.Count == context.ActionDescriptor.Parameters.Count))
                            {
                                problemDetails.Type = "https://tools.ietf.org/html/rfc4918#section-11.2";
                                problemDetails.Status = StatusCodes.Status422UnprocessableEntity;
                                problemDetails.Title = "One or more validation errors occurred.";
                            }

                            return new UnprocessableEntityObjectResult(problemDetails)
                            {
                                ContentTypes = { "application/problem+json" }
                            };
                        };
                    });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if(env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(appBuilder =>
                {
                    appBuilder.Run(async context =>
                    {
                        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                        await context.Response.WriteAsync("An error has occured while processing the request");
                    });
                });
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
