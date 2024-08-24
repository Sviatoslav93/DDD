﻿using Application.Common.Abstractions.Services;
using Microsoft.OpenApi.Models;
using ToDoList.Api.Mappings;
using ToDoList.Api.Services;
using ToDoList.Application;
using ToDoList.DataAccess;

namespace ToDoList.Api;

public static class ConfigureServices
{
    public static IServiceCollection ConfigureApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddEndpointsApiExplorer()
            .AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "ToDoList.Api", Version = "v1" });
                options.SupportNonNullableReferenceTypes();
            })
            .AddMappings();

        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddSingleton(TimeProvider.System);

        services
            .AddApplicationServices()
            .AddDataAccessServices(configuration);

        return services;
    }
}
