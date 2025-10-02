using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_dataBase_UI_Individual.Models
{
    //  Виды комплектующих(
    //          Код вида,
    //          Наименование,
    //          Описание)
    public class ComponentType
    {
        ComponentType() { }
        public ComponentType(int Id, string Name, string Description) { 
            this.Id = Id;
            this.Name = Name;
            this.Description = Description;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
