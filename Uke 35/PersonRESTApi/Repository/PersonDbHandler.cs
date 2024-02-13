using MySql.Data.MySqlClient;
using PersonRESTApi.Model;

namespace PersonRESTApi.Repository;

public class PersonDbHandler : IPersonRepository
{
    // trenger connection string
    private readonly string? _connectionString;
    private readonly ILogger<PersonDbHandler> _logger;

    // hvordan får vi tak i den => DI-container (dependency Injection)
    public PersonDbHandler(IConfiguration config, ILogger<PersonDbHandler> logger)
    {
        // CTRL + D + Q
        _connectionString = config.GetConnectionString("DefaultConnection");
        _connectionString = _connectionString?
            .Replace("{DB_USERNAME}", Environment.GetEnvironmentVariable("DB_USERNAME"))
            .Replace("{DB_PASSWORD}", Environment.GetEnvironmentVariable("DB_PASSWORD"));

        _logger = logger;
    }
    public Person Add(Person person)
    {
        // Legger til log !!

        _logger?.LogDebug("Legger til enn ny person i db med navn: {@Person}", person);

        // Exception handling, logging
        using MySqlConnection conn = new(_connectionString);
        conn.Open();

        // lage query
        MySqlCommand cmd = new("INSERT INTO Person(FirstName, LastName, Age) " +
            "VALUES (@FirstName, @LastName, @Age)", conn);

        cmd.Parameters.AddWithValue("@FirstName", person.FirstName);
        cmd.Parameters.AddWithValue("@LastName", person.LastName);
        cmd.Parameters.AddWithValue("@Age", person.Age);

        var rowsAffected = cmd.ExecuteNonQuery();

        cmd.CommandText = "SELECT LAST_INSERT_ID()";
        var lastIdObject = cmd.ExecuteScalar();

        person.Id = Convert.ToInt32(lastIdObject);
        return person;
    }

    public Person? Delete(int id)
    {
        var personToDelete = GetById(id);

        if(personToDelete == null)
        {
            return null;
        }
        // lag connection
        using MySqlConnection conn = new(_connectionString);
        // Open connection
        conn.Open();

        //cmd med spørring og knytting mot connection
        MySqlCommand cmd = new("DELETE FROM Person WHERE Id=@Id", conn);
        cmd.Parameters.AddWithValue("@Id", id);

        // execute (NonQuery)
        var rowsAffected = cmd.ExecuteNonQuery();
        if (rowsAffected == 0)
        {
            return null;
        }

        //Return Person ??
        return personToDelete;

    }

    public IEnumerable<Person> GetAll()
    {
        throw new Exception("Vi tester med en exception");
        var personList = new List<Person>();
        using MySqlConnection conn = new(_connectionString);
        conn.Open();

        MySqlCommand cmd = new("SELECT Id, FirstName, LastName, Age FROM Person", conn);

        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            var person = new Person
            {
                Id = reader.GetInt32("Id"),
                Age = reader.GetInt32("Age"),
                FirstName = reader.GetString("FirstName"),
                LastName = reader.GetString("LastName")
            };
            personList.Add(person);
        }

        return personList;
    }

    public Person? GetById(int id)
    {
        throw new Exception("Vi tester med en exception");
        using MySqlConnection conn = new(_connectionString);
        conn.Open();

        MySqlCommand cmd = new("SELECT Id, FirstName, LastName, Age FROM Person where id=@Id", conn);
        cmd.Parameters.AddWithValue("@Id", id);

        using var reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            return new Person
            {
                Id = reader.GetInt32("Id"),
                Age = reader.GetInt32("Age"),
                FirstName = reader.GetString("FirstName"),
                LastName = reader.GetString("LastName")
            };
        }
        return null;
    }

    
    public async Task<Person?> UpdateAsync(int id, Person person)
    {
        
        using MySqlConnection conn = new(_connectionString);
        await conn.OpenAsync();

        // SKjekk om personen
        //MySqlCommand Checkcmd = new("", conn);


        // Starter en transaksjon --> hvis man kjører flere tabelller
        MySqlTransaction mySqlTransaction = conn.BeginTransaction();

        try
        {

            MySqlCommand updateCmd = new(
                "UPDATE Person " +
                "SET FirstName=@FirstName, LastName=@LastName, Age=@Age " +
                "WHERE Id=@Id", conn);

            updateCmd.Parameters.AddWithValue("@FirstName", person.FirstName);
            updateCmd.Parameters.AddWithValue("@LastName", person.LastName);
            updateCmd.Parameters.AddWithValue("@Age", person.Age);
            updateCmd.Parameters.AddWithValue("@Id", id);

            var rowsAffected = await updateCmd.ExecuteNonQueryAsync();

            if (rowsAffected == 0)
                return null;

            // Gjør endringer i databasen
            mySqlTransaction.Commit();

            return GetById(id);
        }
        catch (Exception ex)
        {
            try
            {
                mySqlTransaction.Rollback();
            }
            catch 
            {
                // feil i transaksjon
            }
            // Throw original exepetion with stack-trace
            _logger.LogError("Failed to update database", ex.StackTrace);
            throw;
        }

    }
    
}
