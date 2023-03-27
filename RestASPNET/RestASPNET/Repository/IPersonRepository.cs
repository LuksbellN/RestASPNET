using RestASPNET.Model;
using RestASPNET.Repository.Generic;

namespace RestASPNET.Repository
{
    public interface IPersonRepository : IRepository<Person>
    {
        Person Disable(int id);
        List<Person> FindByName(string firstName, string lastName);
    }
}
