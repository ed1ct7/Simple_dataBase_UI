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
        public ComponentType() { }
        public ComponentType(int Id, string Name, string Description) { 
            this.Id = Id;
            this.Name = Name;
            this.Description = Description;
        }
        public ComponentType(List<object> array)
        {
            if (array == null || array.Count < 3)
                throw new ArgumentException("Array must contain at least 3 elements");

            this.Id = Convert.ToInt32(array[0]);
            this.Name = array[1]?.ToString() ?? string.Empty;
            this.Description = array[2]?.ToString() ?? string.Empty;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
