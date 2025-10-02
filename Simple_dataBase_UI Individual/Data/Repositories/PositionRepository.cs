using Simple_dataBase_UI_Individual.Data.Interfaces;
using Simple_dataBase_UI_Individual.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

//  Должности(
//      Код должности,
//      Наименование должности,
//      Оклад,
//      Обязанности,
//      Требования)

namespace Simple_dataBase_UI_Individual.Data.Repositories
{
    public class PositionRepository : BaseRepository<Position>
    {
        public PositionRepository(string dbFilePath)
        {

        }

        public void Add(Position entity)
        {
            try
            {
                DatabaseManager.m_sqlCmd.CommandText = "INSERT INTO positions('id', 'name', 'salary', 'duties', 'requirements')" +
                    "values('"
                    + entity.Id + "','"
                    + entity.Name + "','"
                    + entity.Salary + "','"
                    + entity.Duties + "','"
                    + entity.Requirements + "');"
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
