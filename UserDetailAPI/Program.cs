global using AutoMapper;
global using Microsoft.EntityFrameworkCore;
using UserDetailAPI.BusinessLayer.Interface;
using UserDetailAPI.DataAccessLayer.Repository.Implementation;
using UserDetailAPI.DataAccessLayer.Repository.Interface;
using UserDetailAPI.BusinessLayer.Implementation;
using UserDetailAPI.Configuration;
using UserDetailAPI.DataAccessLayer.Entities;
using UserDetailAPI.CustomMiddleware;
using Microsoft.OpenApi.Models;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc.Formatters;
using UserDetailAPI.Swagger;
using BenchmarkDotNet.Running;
using UserDetailAPI.Benchmark;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("MySqliteConnection");
//BenchmarkRunner.Run<BenchmarkManager>();
// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.OperationFilter<HeaderParameter>();
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "UserDetail API", Version = "v1" });
    option.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid ApiKey",
        Name = "ApiKey",
        Type = SecuritySchemeType.ApiKey,
        BearerFormat = "JWT",
        Scheme = "ApiKeyScheme"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="ApiKey"
                },
                In = ParameterLocation.Header
            },
            new string[]{}
        }
    });
});
//builder.Services.AddControllers();
builder.Services.AddControllers(options => options.OutputFormatters.RemoveType<StringOutputFormatter>());
builder.Services.AddScoped<IUserDetailBusiness, UserDetailBusiness>();
builder.Services.AddScoped<IUserDetailDataRepositry, UserDetailDataRepositry>();
builder.Services.AddAutoMapper(typeof(MapperConfig));
builder.Services.AddDbContext<UserDetailDbContext>(option => option.UseSqlite(connectionString));
builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.DocumentTitle = "UserDetails API - Swagger docs";
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "UserDeatail API V1");
    });
}
app.UseMiddleware(typeof(ExceptionHandlingMiddleware));
app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("corsapp");
app.UseAuthorization();
app.MapControllers();   
app.UseCustomAPIKeyMiddleware();
app.Run();
