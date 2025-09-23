using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_dataBase_UI_Individual.Models
{
    class PropertiesModel : INotifyPropertyChanged
    {
        private Enum _E_State;

        public Enum E_State
        {
            get { return _E_State; }
            set { _E_State = value; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

    }
}
