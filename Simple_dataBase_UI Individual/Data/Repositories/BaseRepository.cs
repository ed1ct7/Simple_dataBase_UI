using Simple_dataBase_UI_Individual.Data;
using Simple_dataBase_UI_Individual.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Simple_dataBase_UI_Individual.Data.Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly string tableName;
        public BaseRepository()
        {
            this.tableName = typeof(T).Name;
        }
        public T CreateInstance()
        {
            return Activator.CreateInstance<T>();
        }
        public virtual DataTable GetAll()
        {
            DataTable dataTable = new DataTable();
            try
            {
                string sqlQuery = $"SELECT * FROM \"{tableName}\"";
                using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqlQuery, DatabaseManager.m_dbConn))
                {
                    adapter.Fill(dataTable);
                }
                Console.WriteLine($"Retrieved {dataTable.Rows.Count} rows from {tableName}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting all from {tableName}: {ex.Message}");
                MessageBox.Show($"Error loading data from {tableName}: {ex.Message}");
            }
            return dataTable;
        }
        public virtual T CreateInstanceFromDataRow(DataRow row) { throw new NotImplementedException(); }
        public virtual void Add(T entity){throw new NotImplementedException();}
        public virtual void Delete(int id) { throw new NotImplementedException(); }
        public virtual void Save() { throw new NotImplementedException(); }
        public virtual void Update(T entity) { throw new NotImplementedException(); }
    }
}
