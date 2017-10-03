using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tavisca.Person.Services
{
    public interface IPersonManager
    {
        Person Get(int id);
        List<Person> GetAll();
        Person Add(Person person);
        Person Update(int id, Person person);
        bool Delete(int id);
    }
}
