using Simple_dataBase_UI_Individual.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple_dataBase_UI_Individual.Data.Interfaces;

//      Заказы(
//          Дата заказа,
//          Дата исполнения,
//          Код заказчика,
//          Код комплектующего 1,
//          Код комплектующего 2,
//          Код комплектующего 3,
//          Доля предоплаты,
//          Отметка об оплате,
//          Отметка об исполнении,
//          Общая стоимость,
//          Срок общей гарантии,
//          Код услуги 1,
//          Код услуги 2,
//          Код услуги 3,
//          Код сотрудника)

namespace Simple_dataBase_UI_Individual.Data.Repositories
{
    public class OrderRepository : BaseRepository<Order>
    {
        public OrderRepository(string dbFilePath) : base(dbFilePath)
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
