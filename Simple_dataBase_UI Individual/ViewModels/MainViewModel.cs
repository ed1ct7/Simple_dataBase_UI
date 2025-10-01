using Simple_dataBase_UI_Individual.Data;
using Simple_dataBase_UI_Individual.Data.Interfaces;
using Simple_dataBase_UI_Individual.Data.Repositories;
using Simple_dataBase_UI_Individual.ViewModels.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Simple_dataBase_UI_Individual.ViewModels
{
    class MainViewModel : INotifyPropertyChanged
    {
        private object _selectedItem;
        public object SelectedItem
        {
            get { return _selectedItem; }
            set { _selectedItem = value;
                OnPropertyChanged();
            }
        }

        private DataTable _dataTable;
        public DataTable DataTableC
        {
            get { return _dataTable; }
            set { _dataTable = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<string> Tables { get; set; }

        private ObservableCollection<object> _items;
        public ObservableCollection<object> Items
        {
            get { return _items; }
            set { _items = value;
                OnPropertyChanged();
            }
        }

        private readonly Dictionary<string, object> _repositories;

        public MainViewModel()
        {
            DatabaseManager.getInstance("testdb");

            _repositories = new Dictionary<string, object> {
                { "ComponentType", new ComponentTypeRepository("testdb")},
                { "Employee", new EmployeeRepository("testdb")},
                { "Order", new OrderRepository("testdb")},
                { "Position", new PositionRepository("testdb")},
                { "Service", new ServiceRepository("testdb")},
                { "Сomponent", new СomponentRepository("testdb")},
                { "Сustomer", new СustomerRepository("testdb")}
                };

            SelectionTableCommand = new RelayCommand(SelectionTable);
            AddItemCommand = new RelayCommand(AddItem);
            LoadItemsCommand = new RelayCommand(LoadItems);

            Tables = new ObservableCollection<string>(_repositories.Keys);
        }
        public ICommand SelectionTableCommand { get; }
        public ICommand AddItemCommand { get; }
        public ICommand LoadItemsCommand { get; }
        public ICommand SelectionChangedCommand { get; }

        private void SelectionTable(object parameter)
        {
            dynamic repository = _repositories[SelectedItem.ToString()];
            DataTableC = repository.GetAll();
        }
        private void AddItem(object parametr)
        {

        }
        private void LoadItems(object parametr)
        {

        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
