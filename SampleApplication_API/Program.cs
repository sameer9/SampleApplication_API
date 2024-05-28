using Microsoft.AspNetCore.Diagnostics;
using SampleApplication.API;
using SampleApplication.API.MyExceptionHandler;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var _configuration = builder.Configuration;
builder.Services.RegisterRepos(_configuration);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddExceptionHandler<ApplicationExceptionHandler>();

builder.Services.AddCors(p => p.AddPolicy("corsappsetting", builder =>
{
    builder.WithOrigins( "https://www.google.com", "https://localhost:7056/api", " * ").AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
}));




var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseExceptionHandler(_ => { });
//app.UseMiddleware<CustomExceptionHandlerMiddleware>(new ApplicationExceptionHandler());

app.UseStatusCodePages();
app.UseExceptionHandler(opt => { });
//app.UseMiddleware<CustomExceptionHandlerMiddleware>();

app.UseHttpsRedirection();
app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.UseAuthorization();

app.MapControllers();

app.Run();
