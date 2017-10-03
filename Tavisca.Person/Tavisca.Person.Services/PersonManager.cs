using System;
using System.Collections.Generic;
using System.Linq;

namespace Tavisca.Person.Services
{
    public class PersonManager : IPersonManager
    {
        private IPersonStore PersonStore { get; }

        public PersonManager(IPersonStore personStore)
        {
            PersonStore = personStore;
        }
        public Person Get(int id)
        {
            var person = GetAll().FirstOrDefault(x => x.Id == id);
            if (person == null)
                throw new Exception("Person not found.");
            return person;
        }

        public List<Person> GetAll()
        {
            var personList = new List<Person>();
            personList.AddRange(PersonStore.Get());
            return personList;
        }

        public Person Add(Person person)
        {
            var id = PersonStore.Add(person);
            if (id == -1)
                throw new Exception("Some error occurred.");
            person.Id = id;
            return person;
        }

        public Person Update(int id, Person person)
        {
            var isUpdateSuccessful = PersonStore.Update(id, person);
            if (isUpdateSuccessful == true)
                return person;
            throw new Exception("Some error occured.");
        }

        public bool Delete(int id)
        {
            var isDeleteSuccessful = PersonStore.Delete(id);
            if (isDeleteSuccessful == true)
                return true;
            throw new Exception("Some error occured.");
        }
    }
}
