using Simple_dataBase_UI_Individual.Data.Interfaces;
using Simple_dataBase_UI_Individual.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SQLite;

namespace Simple_dataBase_UI_Individual.Data.Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly string tableName;
        protected BaseRepository(string dbFilePath) {
            this.tableName = typeof(T).Name;
            DatabaseManager.TableList.Add(tableName);
        }
        public Type GetEntityType()
        {
            return typeof(T);
        }
        public virtual void Add(T entity)
        {
            throw new NotImplementedException();
        }
        public virtual void Delete(int id)
        {
            throw new NotImplementedException();
        }
        public virtual DataTable GetAll()
        {
            DataTable dataTable = new DataTable();
            string sqlQuery;
            sqlQuery = $"SELECT * FROM \"{tableName}\"";
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqlQuery,DatabaseManager.m_dbConn);
            adapter.Fill(dataTable);
            return dataTable;
        }
        public virtual T GetById(int id)
        {
            throw new NotImplementedException();
        }
        public virtual void Save()
        {
            throw new NotImplementedException();
        }
        public virtual void Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
