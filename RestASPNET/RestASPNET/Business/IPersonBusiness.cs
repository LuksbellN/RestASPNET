using RestASPNET.Data.VO;
using RestASPNET.Hypermedia.Utils;
using RestASPNET.Model;

namespace RestASPNET.Business
{
    public interface IPersonBusiness
    {
        PersonVO Create(PersonVO person);
        PersonVO FindById(int id);
        List<PersonVO> FindByName(string firstName, string  lastName);
        Task<List<PersonVO>> AllAsync();
        PagedSearchVO<PersonVO> FindWithPagedSearch(
            string name, string sortDirection, int pageSize, int page);
        PersonVO Update(PersonVO person); 
        void Delete(int id);
        PersonVO Disable(int id);
    }
}
