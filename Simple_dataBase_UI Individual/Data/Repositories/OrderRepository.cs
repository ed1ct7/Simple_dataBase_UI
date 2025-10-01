using Simple_dataBase_UI_Individual.Data.Interfaces;
using Simple_dataBase_UI_Individual.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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

        public void Add(Order entity)
        {
            try
            {
                DatabaseManager.m_sqlCmd.CommandText = "INSERT INTO orders('id', 'order_date', " +
                    "'completion_date', 'customer_id', 'component1_id', 'component2_id', " +
                    "'component3_id', 'prepayment', 'is_paid', 'is_completed', 'total_amount', " +
                    "'total_warranty', 'service1_id', 'service2_id', 'service3_id', 'employee_id')" +
                    "values('"
                    + entity.Id + "','"
                    + entity.Order_Date + "','"
                    + entity.Completion_Date + "','"
                    + entity.Customer_Id + "','"
                    + entity.Component1_Id + "','"
                    + entity.Component2_Id + "','"
                    + entity.Component3_Id + "','"
                    + entity.Prepayment + "','"
                    + entity.Is_Paid + "','"
                    + entity.Is_Completed + "','"
                    + entity.Total_Amount + "','"
                    + entity.Total_Warranty + "','"
                    + entity.Service1_Id + "','"
                    + entity.Service2_Id + "','"
                    + entity.Service3_Id + "','"
                    + entity.Employee_Id + "');"
                    ;
                DatabaseManager.m_sqlCmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        public void Delete(int id)
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
