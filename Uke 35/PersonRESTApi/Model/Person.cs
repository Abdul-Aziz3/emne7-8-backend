namespace PersonRESTApi.Model; //tar semikolonnen skal gjelde for hele filen

public class Person
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public int Age { get; set; }
}




/* json format
{
    "id": 0,
    "firstName": "Abdul-Aziz",
    "lastName" : "Mohammed",
    "age" : 50
}
*/