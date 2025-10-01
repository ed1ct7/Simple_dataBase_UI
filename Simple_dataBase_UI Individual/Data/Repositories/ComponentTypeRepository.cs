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
        public ComponentTypeRepository(string dbFilePath) : base(dbFilePath) { }

        public void Add(ComponentType entity)
        {
            try
            {
                DatabaseManager.m_sqlCmd.CommandText = "INSERT INTO component_type('id', 'name', 'description')" +
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

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public ComponentType GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Update(ComponentType entity)
        {
            throw new NotImplementedException();
        }
    }
}
