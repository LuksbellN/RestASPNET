using RestASPNET.Model;
using RestASPNET.Model.Context;
using RestASPNET.Repository.Generic;

namespace RestASPNET.Repository.Implementations
{
    public class PersonRepository : GenericRepository<Person>, IPersonRepository
    {
        public PersonRepository(MySqlContext context) : base(context) { }

        public Person Disable(int id)
        {
            if (!_context.Persons.Any(p => p.Id == id)) return null;
            var person = _context.Persons.SingleOrDefault(p => p.Id.Equals(id));
            if(person != null)
            {
                person.Enabled = false;
                try
                {
                    _context.Entry(person).CurrentValues.SetValues(person);
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            return person;
        }

        public List<Person> FindByName(string firstName, string lastName)
        {
            if(!string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName))
            {
                return _context.Persons.Where(
                    p => p.FirstName.Contains(firstName)
                    && p.LastName.Contains(lastName)).ToList();
            } 
            else if (!string.IsNullOrEmpty(firstName))
            {
                return _context.Persons.Where(
                    p => p.FirstName.Contains(firstName)).ToList();
            }
            else if (!string.IsNullOrEmpty(lastName))
            {
                return _context.Persons.Where(
                    p => p.LastName.Contains(lastName)).ToList();
            }

            return null;
        }
    }
}
