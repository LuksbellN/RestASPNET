using RestASPNET.Model;
using RestASPNET.Model.Context;
using System;

namespace RestASPNET.Services.Implementations
{
    public class PersonServiceImplementation : IPersonService
    {
        private MySqlContext _context;
        public PersonServiceImplementation(MySqlContext context)
        {
            _context = context;
        }

        public Person FindById(long id)
        {
            return _context.Persons.SingleOrDefault(p => p.Id == id);
        }

        public List<Person> GetAll()
        {
            return _context.Persons.ToList();
        }

        public Person Create(Person person)
        {
            try
            {
                _context.Add(person);
                _context.SaveChanges();
            } 
            catch (Exception)
            {
                throw;
            }
            return person;
        }
        public Person Update(Person person)
        {
            if(!Exists(person.Id)) return new Person();

            var result = _context.Persons.FirstOrDefault(p => p.Id.Equals(person.Id));
            if(result != null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(person);
                    _context.SaveChanges();
                } catch (Exception)
                {
                    throw;
                }
            }
            return person;
        }

        public void Delete(long id)
        {
            var result = _context.Persons.FirstOrDefault(p => p.Id.Equals(id));
            if(result != null)
            {
                try
                {
                    _context.Persons.Remove(result);
                    _context.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        private bool Exists(long id) 
        {
            return _context.Persons.Any(p => p.Id.Equals(id));
        }
    }
}
