using Simple_dataBase_UI_Individual.ViewModels.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Simple_dataBase_UI_Individual.ViewModels
{
    class MainViewModel : INotifyPropertyChanged
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

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
