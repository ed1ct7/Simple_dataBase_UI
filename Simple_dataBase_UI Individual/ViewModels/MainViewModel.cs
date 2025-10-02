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
using System.Windows.Controls;
using System.Windows.Input;

namespace Simple_dataBase_UI_Individual.ViewModels
{
    class MainViewModel : INotifyPropertyChanged
    {
        private object _selectedTable;
        public object SelectedTable
        {
            get { return _selectedTable; }
            set {
                _selectedTable = value;
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
            DatabaseManager.getInstance("testdb.sqlite");

            _repositories = new Dictionary<string, object> {
                { "ComponentType", new ComponentTypeRepository("testdb")},
                { "Employee", new EmployeeRepository("testdb")},
                { "Order", new OrderRepository("testdb")},
                { "Position", new PositionRepository("testdb")},
                { "Service", new ServiceRepository("testdb")},
                { "Сomponent", new СomponentRepository("testdb")},
                { "Customer", new CustomerRepository("testdb")}
                };

            SelectionTableCommand = new RelayCommand(SelectionTable);
            SelectionChangedCommand = new RelayCommand(SelectionChanged);
            CellEditEndingCommand = new RelayCommand(CellEditEnding);
            RowEditEndingCommand = new RelayCommand(RowEditEnding);


            Tables = new ObservableCollection<string>(_repositories.Keys);
        }
        public ICommand SelectionTableCommand { get; }
        public ICommand SelectionChangedCommand { get; }
        public ICommand CellEditEndingCommand { get; }
        public ICommand RowEditEndingCommand { get; }
        public void CellEditEnding(object parameter)
        {

        }
        public void SelectionChanged(object parameter)
        {

        }
        /*
        public void RowEditEnding(object parameter)
        {
            if (parameter is DataGridCellEditEndingEventArgs e)
            {
                if (e.EditAction == DataGridEditAction.Commit)
                {
                    var rowData = e.Row.DataContext;

                    // Safe way to access repository
                    if (_repositories.TryGetValue(SelectedTable.ToString(), out var repository))
                    {
                        // Assuming repository is BaseRepository<T>
                        var addMethod = repository.GetType().GetMethod("Add");
                        if (addMethod != null)
                        {
                            addMethod.Invoke(repository, new[] { rowData });
                        }
                    }
                }
            }
        }
        */
        public void RowEditEnding(object parameter)
        {
            if (parameter is DataGridCellEditEndingEventArgs e)
            {
                if (e.EditAction == DataGridEditAction.Commit)
                {
                    var rowData = e.Row.DataContext;

                    var properties = rowData.GetType().GetProperties();

                    var values = new object[properties.Length];
                    for (int i = 0; i < properties.Length; i++)
                    {
                        values[i] = properties[i].GetValue(rowData);
                    }

                    dynamic repository = _repositories[SelectedTable.ToString()];
                    var entity = repository.CreateInstance(repository);

                    repository.Add(values);
                }
            }
        }
        private void SelectionTable(object parameter)
        {
            dynamic repository = _repositories[SelectedTable.ToString()];
            DataTableC = repository.GetAll();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
