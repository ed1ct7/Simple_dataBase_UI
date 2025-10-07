using Simple_dataBase_UI_Individual.Data;
using Simple_dataBase_UI_Individual.Data.Interfaces;
using Simple_dataBase_UI_Individual.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

//  Виды комплектующих(
//          Код вида,
//          Наименование,
//          Описание)

namespace Simple_dataBase_UI_Individual.Data.Repositories
{
    public class ComponentTypeRepository : BaseRepository<ComponentType>
    {
        public ComponentTypeRepository(string dbFilePath) { }

        public void Add(ComponentType entity)
        {
            try
            {
                DatabaseManager.m_sqlCmd.CommandText = "INSERT INTO ComponentType('id', 'name', 'description')" +
                    "values('"
                    + entity.Id + "','"
                    + entity.Name + "','" 
                    + entity.Description + "');"
                    ;
                DatabaseManager.m_sqlCmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        public void Update(ComponentType entity)
        {
            throw new NotImplementedException();
        }
    }
}
