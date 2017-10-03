using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tavisca.Person.Services
{
    public class SqlPersonStore : IPersonStore
    {
        private static string ConnectionStringKey = "connection-string";
        public SqlPersonStore()
        {
            var connectionString = ConfigurationManager.AppSettings[ConnectionStringKey];
            connection = new SqlConnection(connectionString);
        }
        SqlConnection connection;
        public int Add(Person person)
        {
            var id = 0;
            try
            {
                connection.Open();
                string query = $"select MAX(id) as maxId from people ";
                SqlCommand command = new SqlCommand(query, connection);
                using (var sqlReader = command.ExecuteReader())
                {
                    sqlReader.Read();
                    id = sqlReader.GetInt32(0) + 1;
                }

                string commandString = $"insert into people values ('{id}','{person.Name}','{person.Dob}','{person.Mobile}')";
                SqlCommand cmd = new SqlCommand(commandString, connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                id = -1;
            }
            finally
            {
                connection.Close();
            }
            return id;
        }

        public bool Delete(int id)
        {
            try
            {
                connection.Open();
                string deleteQuery = $"delete from people where id={id}";
                SqlCommand comd = new SqlCommand(deleteQuery, connection);
                comd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                connection.Close();
            }
            return true;
        }

        public List<Person> Get()
        {
            var personList = new List<Person>();
            try
            {
                connection.Open();
                string selectQuery = $"select * from people";
                SqlCommand commd = new SqlCommand(selectQuery, connection);
                SqlDataAdapter da = new SqlDataAdapter(commd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                var data = ds.Tables[0].Rows;

                for(int i=0;i< ds.Tables[0].Rows.Count; i++)
                {
                    var person = new Person
                    {
                        Id = Convert.ToInt32( ds.Tables[0].Rows[i][0]),
                        Name = ds.Tables[0].Rows[i][1].ToString(),
                        Dob = ds.Tables[0].Rows[i][2].ToString(),
                        Mobile = ds.Tables[0].Rows[i][3].ToString()
                    };
                    personList.Add(person);
             
                }
        
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
            return personList;
        }

        public bool Update(int id, Person person)
        {
            try
            {
                connection.Open();
                var updateCommand = $"update people SET name = '{person.Name}', dob = '{person.Dob}' , mobile = '{person.Mobile}'  where id = {id}; ";
                SqlCommand cmnd = new SqlCommand(updateCommand, connection);
                cmnd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                connection.Close();
            }
            return true;
        }
    }
}