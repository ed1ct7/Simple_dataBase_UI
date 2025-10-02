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
        Component() { }
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
        public int Id { get; set; }
        public int Type_Id { get; set; }
        public string Brand { get; set; }
        public string Manufacturer_Company { get; set; }
        public string Manufacturer_Country { get; set; }
        public string ReleaseDate { get; set; }
        public string Specifications { get; set; }
        public string Warranty { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
    }
}
