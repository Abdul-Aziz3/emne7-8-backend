using PersonRESTApi.Model;

namespace PersonRESTApi.Repository;

public class PersonInMemoryDb //: IPersonRepository
{
    private List<Person> _persons = new();
    private int _lastId = 0;

    public Person Add(Person person)
    {
        _lastId++;
        person.Id = _lastId;
        _persons.Add(person);
        return person;
    }

    public Person? Delete(int id)
    {
        // finne personen med den gitte ID-en i listen
        var personToDelete = _persons.FirstOrDefault(person => person.Id == id); //GetById(id)

        if (personToDelete != null)
        {
            _persons.Remove(personToDelete);
        }

        return personToDelete;

    }

    public IEnumerable<Person> GetAll()
    {
        return _persons;
    }

    public Person? GetById(int id)
    {
        var person = _persons.FirstOrDefault( person  => person.Id == id);

        return person;
    }

    public Person Update(int id, Person person)
    {
        var existingPerson = _persons.FirstOrDefault(person => person.Id == id);
        if (existingPerson != null)
        {
            person.FirstName = existingPerson.FirstName;
            person.LastName = existingPerson.LastName;
            person.Age = existingPerson.Age;
        }

        return person;
    }
}
