using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Tavisca.Person.Services;

namespace Tavisca.Person.Controllers
{
    public class PersonController : ApiController
    {
        public PersonController()
        {
            var personStore = new SqlPersonStore();
            PersonManager = new PersonManager(personStore);
        }

        private IPersonManager PersonManager { get; }

        public List<Services.Person> Get()
        {
            return PersonManager.GetAll();
        }

        public Services.Person Get(int id)
        {
            return PersonManager.Get(id);
        }

        public Services.Person Post([FromBody] Services.Person value)
        {
            return PersonManager.Add(value);
        }

        public Services.Person Put(int id,[FromBody] Services.Person value)
        {
            return PersonManager.Update(id, value);
        }

        public bool Delete(int id)
        {
            return PersonManager.Delete(id);
        }
    }
}