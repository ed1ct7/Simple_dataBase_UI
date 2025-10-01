using Simple_dataBase_UI_Individual.Data.Interfaces;
using Simple_dataBase_UI_Individual.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

//  Услуги(
//      Код услуги,
//      Наименование,
//      Описание,
//      Стоимость)

namespace Simple_dataBase_UI_Individual.Data.Repositories
{
    public class ServiceRepository : BaseRepository<Service>
    {
        public ServiceRepository(string dbFilePath) : base(dbFilePath)
        {

        }

        public void Add(Service entity)
        {
            try
            {
                DatabaseManager.m_sqlCmd.CommandText = "INSERT INTO services('id', 'name', 'description', 'price')" +
                    "values('"
                    + entity.Id + "','"
                    + entity.Name + "','"
                    + entity.Description + "','"
                    + entity.Price + "');"
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
