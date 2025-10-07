using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_dataBase_UI_Individual.Models
{
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

    public class Component
    {
        public Component() { }
        public Component(int Id, int Type_Id,
            string Brand, string Manufacturer_Company,
            string Manufacturer_Country, string ReleaseDate,
            string Specifications, string Warranty,
            string Description, int Price
            ) { 
            this.Id = Id;
            this.Type_Id = Type_Id;
            this.Brand = Brand;
            this.Manufacturer_Company = Manufacturer_Company;
            this.Manufacturer_Country = Manufacturer_Country;
            this.ReleaseDate = ReleaseDate;
            this.Specifications = Specifications;
            this.Warranty = Warranty;
            this.Description = Description;
            this.Price = Price;
        }
        // Constructor that takes a List<object>
        public Component(List<object> array)
        {
            if (array == null || array.Count < 10)
                throw new ArgumentException("Array must contain at least 10 elements");

            this.Id = Convert.ToInt32(array[0]);
            this.Type_Id = Convert.ToInt32(array[1]);
            this.Brand = array[2]?.ToString() ?? string.Empty;
            this.Manufacturer_Company = array[3]?.ToString() ?? string.Empty;
            this.Manufacturer_Country = array[4]?.ToString() ?? string.Empty;
            this.ReleaseDate = array[5]?.ToString() ?? string.Empty;
            this.Specifications = array[6]?.ToString() ?? string.Empty;
            this.Warranty = array[7]?.ToString() ?? string.Empty;
            this.Description = array[8]?.ToString() ?? string.Empty;
            this.Price = Convert.ToInt32(array[9]);
        }
        public int Id { get; set; }
        public int Type_Id { get; set; }
        public string Brand { get; set; }
        public string Manufacturer_Company { get; set; }
        public string Manufacturer_Country { get; set; }
        public string ReleaseDate { get; set; }
        public string Specifications { get; set; }
        public string Warranty { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
