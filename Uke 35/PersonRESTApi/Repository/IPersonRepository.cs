using PersonRESTApi.Model;

namespace PersonRESTApi.Repository;

public interface IPersonRepository
{
    Person Add(Person person);
    Task<Person?> UpdateAsync(int id,Person person);
    Person? Delete(int id);
    Person? GetById(int id);
    IEnumerable<Person> GetAll();
    //list<>, Dictionary<>
}


