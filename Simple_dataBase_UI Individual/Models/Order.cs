using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_dataBase_UI_Individual.Models
{
    //      Заказы(
    //          Дата заказа,
    //          Дата исполнения,
    //          Код заказчика,
    //          Код комплектующего 1,
    //          Код комплектующего 2,
    //          Код комплектующего 3,
    //          Доля предоплаты,
    //          Отметка об оплате,
    //          Отметка об исполнении,
    //          Общая стоимость,
    //          Срок общей гарантии,
    //          Код услуги 1,
    //          Код услуги 2,
    //          Код услуги 3,
    //          Код сотрудника)

    public class Order
    {
        public string Order_Date {  get; set; }
        public string Order_Execution {  get; set; }
        public int Customer_ID { get; set; }
        public int Component1_ID {  get; set; }
        public int Component2_ID { get; set; }
        public int Component3_ID { get; set; }


    }
}
