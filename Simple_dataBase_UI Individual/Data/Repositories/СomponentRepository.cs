using Simple_dataBase_UI_Individual.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple_dataBase_UI_Individual.Data.Interfaces;

//  Комплектующие(
//      Код комплектующего,
//      Код вида,
//      Марка,
//      Фирма производитель,
//      Страна производитель,
//      Дата выпуска,
//      Характеристики,
//      Срок гарантия,
//      Описание,
//      Цена)

namespace Simple_dataBase_UI_Individual.Data.Repositories
{
    public class СomponentRepository : BaseRepository<Employee>
    {
        public СomponentRepository(string dbFilePath) : base(dbFilePath)
        {

        }

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
