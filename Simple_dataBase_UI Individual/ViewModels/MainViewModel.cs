using Simple_dataBase_UI_Individual.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Simple_dataBase_UI_Individual.ViewModels
{
    class MainViewModel
    {

        MainViewModel()
        {
            PrepareProgramm();
            CheckConnectionCommand = new RelayCommand(ChechConnection);
        }

        public ICommand CheckConnectionCommand { get; }

        private void ChechConnection(object parameter)
        {

        }

        private void PrepareProgramm()
        {

        }
    }
}
