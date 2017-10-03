using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tavisca.Person.Services
{
    public interface IPersonStore
    {
        List<Person> Get();
        int Add(Person person);
        bool Update(int id, Person person);
        bool Delete(int id);
    }
}
