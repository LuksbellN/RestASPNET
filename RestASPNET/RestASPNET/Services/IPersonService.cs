using RestASPNET.Model;

namespace RestASPNET.Services
{
    public interface IPersonService
    {
        Person Create(Person person);
        Person FindById(long id);
        List<Person> GetAll();
        Person Update(Person person);
        void Delete(long id);
    }
}
