using Simple_dataBase_UI_Individual.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple_dataBase_UI_Individual.Data.Interfaces;

namespace Simple_dataBase_UI_Individual.Data.Repositories
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployee
    {
        public EmployeeRepository(string dbFilePath) : base(dbFilePath)
        {

        }

        protected override string tableName => throw new NotImplementedException();

        public void Add(Employee entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Employee> GetAll()
        {
            throw new NotImplementedException();
        }

        public Employee GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Update(Employee entity)
        {
            throw new NotImplementedException();
        }
    }
}
