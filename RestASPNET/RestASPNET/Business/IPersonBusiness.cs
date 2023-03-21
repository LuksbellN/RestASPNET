using RestASPNET.Model;

namespace RestASPNET.Business
{
    public interface IPersonBusiness
    {
        Person Create(Person person);
        Person FindById(int id);
        Task<List<Person>> AllAsync();
        Person Update(Person person);
        void Delete(int id);
    }
}
