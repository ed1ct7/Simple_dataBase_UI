using Simple_dataBase_UI_Individual.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_dataBase_UI_Individual.Data.Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly string dbFilePath;
        protected abstract string tableName { get; }

        protected BaseRepository(string dbFilePath) { 
            this.dbFilePath = dbFilePath;
        }
        public virtual void Add(T entity)
        {
            throw new NotImplementedException();
        }

        public virtual void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public virtual IEnumerable<T> GetAll()
        {
            throw new NotImplementedException();
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
