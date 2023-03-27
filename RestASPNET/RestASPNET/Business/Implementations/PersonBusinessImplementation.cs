using Mysqlx.Crud;
using RestASPNET.Data.Converter.Implementations;
using RestASPNET.Data.VO;
using RestASPNET.Hypermedia.Utils;
using RestASPNET.Model;
using RestASPNET.Repository;
using RestASPNET.Repository.Generic;

namespace RestASPNET.Business.Implementations
{
    public class PersonBusinessImplementation : IPersonBusiness
    {
        private readonly IPersonRepository _repository;
        private readonly PersonConverter _converter;

        public PersonBusinessImplementation(IPersonRepository repository)
        {
            _repository = repository;
            _converter = new PersonConverter();
        }

        public PersonVO FindById(int id)
        {
            return _converter.Parse(_repository.FindById(id));
        }
        public List<PersonVO> FindByName(string firstName, string lastName)
        {
            return _converter.Parse(_repository.FindByName(firstName, lastName));
        }

        public async Task<List<PersonVO>> AllAsync()
        {
            return await _converter.ParseAsync(_repository.GetAllAsync());
        }
        public PagedSearchVO<PersonVO> FindWithPagedSearch(
            string name, string sortDirection, int pageSize, int page)
        {
            string sort = (!string.IsNullOrEmpty(sortDirection) && !sortDirection.Equals("desc")) 
                ? "asc" : "desc";
            int size = pageSize < 1 ? 10 : pageSize;
            int offSet = page > 0 ? (page - 1) * size : 0;

            string query = @"select * from person p where 1=1";

            if (!string.IsNullOrWhiteSpace(name))
            {
                query += $" and p.first_name like '%{name}%'";
                query += $" order by p.first_name {sort} limit {size} offset {offSet}";
            }

            var persons = _repository.FindWithPagedSearch(query);

            string countQuery = @"select Count(*) from person p where 1=1";

            if (!string.IsNullOrWhiteSpace(name))
            {
                countQuery += $" and p.first_name like '%{name}%'";
            }


            int totalResults = _repository.GetCount(countQuery);

            return new PagedSearchVO<PersonVO> { 
                CurrentPage = page,
                Data = _converter.Parse(persons),
                PageSize = size,
                SortDirections = sort,
                TotalResults = totalResults
            };
        }

        public PersonVO Create(PersonVO person)
        {
            var personEntity = _converter.Parse(person);
            return _converter.Parse(_repository.Create(personEntity));
        }
        public PersonVO Update(PersonVO person)
        {
            var personEntity = _converter.Parse(person);
            return _converter.Parse(_repository.Update(personEntity));
        }
        public PersonVO Disable(int id)
        {
            var personEntity = _repository.Disable(id);
            if (personEntity == null) return null;
            return _converter.Parse(personEntity);
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

    }
}
