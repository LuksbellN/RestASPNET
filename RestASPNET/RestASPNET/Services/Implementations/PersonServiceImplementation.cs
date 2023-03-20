using RestASPNET.Model;

namespace RestASPNET.Services.Implementations
{
    public class PersonServiceImplementation : IPersonService
    {
        private volatile int count;

        public Person Create(Person person)
        {
            return person;
        }

        public void Delete(long id)
        {
            
        }

        public Person FindById(long id)
        {
            return new Person
            {
                Id = id,
                FirstName = "Lucas",
                LastName = "Costa",
                Address = "Uberlandia - Minas Gerais - Brasil",
                Gender = "Male"
            };
        }

        public List<Person> GetAll()
        {
            List<Person> persons = new List<Person>();
            for(int i = 0; i < 8; i++)
            {
                Person person = MockPerson(i);
                persons.Add(person);
            }
            return persons;
        }


        public Person Update(Person person)
        {

            return person;
        }
        private Person MockPerson(int i)
        {
            return new Person
            {
                Id = IncrementAndGet(),
                FirstName = "Person Name" + i,
                LastName = "Person Last Name"+i,
                Address = "Some Address" + i,
                Gender = "Random"
            };
        }

        private long IncrementAndGet()
        {
            return Interlocked.Increment(ref count);
        }
    }
}
