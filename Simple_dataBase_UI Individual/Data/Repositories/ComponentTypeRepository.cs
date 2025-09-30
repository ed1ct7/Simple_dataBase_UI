using Simple_dataBase_UI_Individual.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple_dataBase_UI_Individual.Data.Interfaces;
using System.ComponentModel;

//  Виды комплектующих(
//          Код вида,
//          Наименование,
//          Описание)

namespace Simple_dataBase_UI_Individual.Data.Repositories
{
    public class ComponentTypeRepository : BaseRepository<Component>
    {
        public ComponentTypeRepository(string dbFilePath) : base(dbFilePath)
        {

        }

        public void Add(ComponentType entity)
        {
            
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ComponentType> GetAll()
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
