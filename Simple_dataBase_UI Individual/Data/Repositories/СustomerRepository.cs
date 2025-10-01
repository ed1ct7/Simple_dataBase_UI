using Simple_dataBase_UI_Individual.Data.Interfaces;
using Simple_dataBase_UI_Individual.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

//  Заказчики(
//      Код заказчика,
//      ФИО,
//      Адрес,
//      Телефон).

namespace Simple_dataBase_UI_Individual.Data.Repositories
{
    public class СustomerRepository : BaseRepository<Сustomer>
    {
        public СustomerRepository(string dbFilePath) : base(dbFilePath)
        {

        }
        public void Add(Сustomer entity)
        {
            try
            {
                DatabaseManager.m_sqlCmd.CommandText = "INSERT INTO customers('id', 'full_name', 'address', 'phone')" +
                    "values('"
                    + entity.Id + "','"
                    + entity.Full_Name + "','"
                    + entity.Address + "','"
                    + entity.Phone + "');"
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
