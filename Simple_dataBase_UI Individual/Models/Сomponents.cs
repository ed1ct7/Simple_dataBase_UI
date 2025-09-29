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

    public class Сomponents
    {
        public int ComponentId { get; set; }
        public int TypeId { get; set; }
        public string Brand { get; set; }
        public string ManufacturerCountry { get; set; }
        public string ReleaseDate { get; set; }
        public string Specifications { get; set; }
        public string WarrantyPeriod { get; set; }
        public string Discription { get; set; }
        public int Price { get; set; }
    }
}
