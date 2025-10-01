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
    public class СomponentRepository : BaseRepository<Models.Component>
    {
        public СomponentRepository(string dbFilePath) : base(dbFilePath)
        {

        }

        public void Add(Models.Component entity)
        {
            try
            {
                DatabaseManager.m_sqlCmd.CommandText = "INSERT INTO components('id', 'type_id', 'brand', 'manufacturer_company', 'manufacturer_country', 'release_date', 'specifications', 'warranty', 'description', 'price')" +
                    "values('"
                    + entity.Id + "','"
                    + entity.Type_Id + "','"
                    + entity.Brand + "','"
                    + entity.Manufacturer_Company + "','"
                    + entity.Manufacturer_Country + "','"
                    + entity.ReleaseDate + "','"
                    + entity.Specifications + "','"
                    + entity.Warranty + "','"
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
