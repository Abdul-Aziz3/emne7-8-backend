using Microsoft.AspNetCore.Mvc;
using PersonRESTApi.Endpoints;
using PersonRESTApi.Middelware;
using PersonRESTApi.Model;
using PersonRESTApi.Repository;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IPersonRepository, PersonDbHandler>();
builder.Services.AddTransient<GlobalExceptionMiddleware>();
//builder.Services.AddSingleton<IPersonRepository, PersonInMemoryDb>();

// ILogger 
// fy, fy hardkode config !!
//builder.Logging.AddConsole(); //aktivere loggingen
builder.Host.UseSerilog((context, configuration) =>
{
    configuration.ReadFrom.Configuration(context.Configuration);
});


var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//// null forgiving operator => !
//var logger = (ILogger<Program>)app.Services.GetService(typeof(ILogger<Program>))!;
//// Middelware legges til i pipline
//app.Use(async (context, next) =>
//{
//    try
//    {
//        await next(context);
//    }
//    catch (Exception ex)
//    {
//        logger.LogError(ex, "Noe gikk galt - test exception {@Machine} {TraceId}",
//                Environment.MachineName,
//                System.Diagnostics.Activity.Current?.Id);


//        await Results.Problem(
//            title: "noe fryktelig har skjedd !!",
//            statusCode: StatusCodes.Status500InternalServerError,
//            extensions: new Dictionary<string, Object?>
//            {
//                    { "traceId", System.Diagnostics.Activity.Current?.Id },
//            })
//            .ExecuteAsync(context);
//    }
//});

// Middleware
app.UseMiddleware<GlobalExceptionMiddleware>();
app.UseSerilogRequestLogging();
app.UseHttpsRedirection();


app.MapPersonEndpoints();

// https://localhost:7183/filtertest?start=3&stop=10
app.MapGet("/filtertest", ([FromQuery] int? start, [FromQuery] int? stop) =>
{
    return $"hello, start={start}, stop={stop}";
});


// HTTP metode: GET
app.MapGet("/httpheader", (HttpContext context) =>
{
    return $"{context.Request.Host.Value}, \n{context.Request.Headers["User-Agent"]}, " +
    $"\n{context.Request.Headers["Accept"]}";
})
  .WithName("GetHttpHeaderInfo")
  .WithOpenApi();

app.Run();

