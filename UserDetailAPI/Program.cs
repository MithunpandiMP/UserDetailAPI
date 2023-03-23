global using AutoMapper;
global using Microsoft.EntityFrameworkCore;
using UserDetailAPI.BusinessLayer.Interface;
using UserDetailAPI.DataAccessLayer.Repository.Implementation;
using UserDetailAPI.DataAccessLayer.Repository.Interface;
using UserDetailAPI.BusinessLayer.Implementation;
using UserDetailAPI.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserDetailAPI.DataAccessLayer;
using UserDetailAPI.BusinessLayer.DTO;
using UserDetailAPI;
using UserDetailAPI.DataAccessLayer.Entities;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddTransient<IUserDetailBusiness, UserDetailBusiness>();
builder.Services.AddTransient<IUserDetailDataRepositry, UserDetailDataRepositry>();
builder.Services.AddAutoMapper(typeof(MapperConfig));
builder.Services.AddDbContext<UserDetailDbContext>();
builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("corsapp");
app.UseAuthorization();
app.MapControllers();
app.UseCustomAPIKeyMiddleware();
app.Run();
