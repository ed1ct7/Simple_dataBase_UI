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
using System.Windows.Threading;

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
        public void RowEditEnding(object parameter)
        {
            if (parameter is DataGridRowEditEndingEventArgs e)
            {
                if (e.EditAction == DataGridEditAction.Commit)
                {
                    if (e.Row.DataContext is DataRowView dataRowView)
                    {
                        Dispatcher dispatcher = System.Windows.Application.Current.Dispatcher;
                        Action myAction = delegate ()
                        {
                            ProcessRowEdit(dataRowView.Row);
                        };
                        dispatcher.BeginInvoke(myAction, DispatcherPriority.Background);
                    }
                }
            }
        }
        private void ProcessRowEdit(DataRow dataRow)
        {
            try
            {
                bool isNewRow = dataRow.RowState == DataRowState.Added ||
                                dataRow.RowState == DataRowState.Detached ||
                                dataRow.IsNull("id") ||
                                dataRow["id"] == DBNull.Value ||
                                Convert.ToInt32(dataRow["id"]) == 0;

                dynamic repository = _repositories[SelectedTable.ToString()];
                var entity = repository.CreateInstanceFromDataRow(dataRow);

                if (isNewRow)
                {
                    repository.Add(entity);
                    dataRow["id"] = entity.Id;
                }
                else
                {
                    repository.Update(entity);
                }
                Dispatcher dispatcher = System.Windows.Application.Current.Dispatcher;
                Action myAction = delegate ()
                {
                    RefreshDataTable(repository);
                };
                dispatcher.BeginInvoke(myAction, DispatcherPriority.ApplicationIdle);
            }
            catch (Exception ex) { }
        }

        private void RefreshDataTable(dynamic repository)
        {
            var newDataTable = repository.GetAll();
            DataTableC = newDataTable;
        }
        private void SelectionTable(object parameter)
        {   
            dynamic repository = _repositories[SelectedTable.ToString()];
            RefreshDataTable(repository);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
