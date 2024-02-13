using Microsoft.AspNetCore.Mvc;
using PersonRESTApi.Model;
using PersonRESTApi.Repository;
using System;

namespace PersonRESTApi.Endpoints;

public static class PersonEndpoints
{
    /*
        app.MapGet("/persons", (IPersonRepository repo) =>
        {
            return repo.GetAll();
        }).WithName("GetAllPersons").WithOpenApi();
    */
    public static void MapPersonEndpoints(this WebApplication app)
    {
        app.MapGet("/persons", GetPersons).WithName("GetPersons").WithOpenApi();
        app.MapGet("/persons/{id}", GetPersonById).WithName("GetPersonsById").WithOpenApi();
        app.MapPost("/persons", AddPerson).WithName("AddPerson").WithOpenApi(); // HTTP POST => sende data til server
        app.MapDelete("/persons/{id}",DeletePerson).WithName("DeletePerson").WithOpenApi();
        app.MapPut("/persons/{id}", UpdatePersonAsync).WithName("UpdatePerson").WithOpenApi();
        // Delete HTTP method DELETE
        //app.MapDelete

        // Update HTTP method PUT
        //app.MapPut
    }

    // https://localhost:<>/persons?age=22
    private static IResult GetPersons(
        IPersonRepository repo, 
        ILogger<Program> logger,
        [FromQuery] int? age)
    {
        if (age != null)
            return Results.Ok(repo.GetAll().Where(p => p.Age == age));

        return Results.Ok(repo.GetAll().ToArray());
    }
    private static IResult GetPersonById(IPersonRepository repo, int id)
    {
        if (repo == null)
            return Results.NoContent();

        var person = repo.GetById(id);
        if (person != null)
        {
            return Results.Ok(person);
        }
        return Results.NotFound($"Fant ikke person med id={id}");
    }

    private static IResult AddPerson(IPersonRepository repo, Person person)
    {
        if (person != null)
        {
            var p = repo.Add(person);
            if (p != null)
                return Results.Ok(p);
        }
        return Results.NoContent();
    }

    private static IResult DeletePerson(IPersonRepository repo, int id) 
    {
        var deletedPerson = repo.Delete(id);
        if (deletedPerson == null)
        {
            //person ikke funnet
            return Results.NotFound("Fant ikke personen du ønsker å slette");
        }
        //person ble funnet og slettet
        return Results.Ok(deletedPerson);
    }

    private static async Task<IResult> UpdatePersonAsync(IPersonRepository repo, int id, Person person)
    {
       
        // Lagre de oppdaterte opplysningene til databasen ved å bruke IPersonRepository
        var updatedPerson = await repo.UpdateAsync(id, person);
        if(updatedPerson == null)
            // Hvis oppdateringen mislykkes, kan du returnere en feilmelding
            return Results.NotFound("Fant ikke personen du prøver å oppdatere");
        // Returner OK-status og den oppdaterte personen
        return Results.Ok(updatedPerson);
    }

}
