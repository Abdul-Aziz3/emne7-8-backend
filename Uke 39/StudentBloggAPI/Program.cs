using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Serilog;
using StudentBloggAPI.Data;
using StudentBloggAPI.Extensions;
using StudentBloggAPI.Mappers;
using StudentBloggAPI.Mappers.Interfaces;
using StudentBloggAPI.Middelware;
using StudentBloggAPI.Models.DTOs;
using StudentBloggAPI.Models.Entities;
using StudentBloggAPI.Repository;
using StudentBloggAPI.Repository.Interfaces;
using StudentBloggAPI.Services;
using StudentBloggAPI.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();


// Extension methods
builder.RegisterMappers();
builder.AddSwaggerWithBasicAuthentication();

// DI- container // services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<ICommentService, CommentService>();


// Mappers
//builder.Services.AddScoped(typeof(IMapper<User, UserDTO>), typeof(UserMapper));
//builder.Services.AddScoped<IMapper<User, UserDTO>, UserMapper>();
//builder.Services.AddScoped<IMapper<User, UserRegistrationDTO>, UserRegMapper>();


//repos
builder.Services.AddScoped<IUserRepository, UserRepositoryDbContext>();
builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();

builder.Services.AddScoped<StudentBlogBasicAuthentication>();

//validering
builder.Services.AddValidatorsFromAssemblyContaining<Program>();
builder.Services.AddFluentValidationAutoValidation(config => config.DisableDataAnnotationsValidation = false);

// DbMysql
builder.Services.AddDbContext<StudentBloggDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 21))));

//Logger
builder.Services.AddTransient<GlobalExceptionMiddleware>();
builder.Host.UseSerilog((hostingContext, loggerConfiguration) =>
    loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
// Register Middelwares
app.UseMiddleware<GlobalExceptionMiddleware>();
app.UseSerilogRequestLogging();
app.UseMiddleware<StudentBlogBasicAuthentication>(); // nå har vi registert

//app.UseAuthorization(); // i basic auto- vi trenger ikke.

app.MapControllers();

app.Run();

public partial class Program { }