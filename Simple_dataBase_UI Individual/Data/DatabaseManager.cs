using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Simple_dataBase_UI_Individual.Data
{
    public enum State
    {
        Disconnected,
        Connected
    }

    class DatabaseManager
    {
        private static DatabaseManager instance;
        private DatabaseManager(string dbFilePath)
        {
            if (!File.Exists(dbFilePath))
                SQLiteConnection.CreateFile(dbFilePath);

            m_dbConn = new SQLiteConnection("Data Source=" + dbFilePath + ";Version=3");
            m_dbConn.Open();

            // Initialize the command and set its connection BEFORE using it
            m_sqlCmd = new SQLiteCommand();
            m_sqlCmd.Connection = m_dbConn; // Set connection first!

            // Now execute the foreign keys command
            m_sqlCmd.CommandText = "PRAGMA foreign_keys = ON";
            m_sqlCmd.ExecuteNonQuery();

            DatabaseManager.TableList = new ObservableCollection<string>();

            CreateTable();
        }

        public static DatabaseManager getInstance(string dbFilePath = "")
        {
            if (instance == null)
                instance = new DatabaseManager(dbFilePath);
            return instance;
        }

        private static List<string> _tables;
        public static List<string> Tables
        {
            get { return _tables; }
            set { _tables = value; }
        }

        private static Enum _dbState;
        public static Enum dbState
        {
            get { return _dbState; }
            set { _dbState = value; }
        }

        private readonly string dbFilePath;

        private static SQLiteConnection _dbConn;
        public static SQLiteConnection m_dbConn
        {
            get { return _dbConn; }
            set { _dbConn = value; }
        }

        private static SQLiteCommand _m_sqlCmd;
        public static SQLiteCommand m_sqlCmd
        {
            get { return _m_sqlCmd; }
            set { _m_sqlCmd = value; }
        }

        private static ObservableCollection<string> _tableList;
        public static ObservableCollection<string> TableList
        {
            get { return _tableList; }
            set { _tableList = value; }
        }

        public void CreateTable()
        {
            try
            {
                string[] createTableCommands = {
                    // Таблица Должности
                    @"CREATE TABLE IF NOT EXISTS Position (
                        id INTEGER PRIMARY KEY, 
                        name TEXT, 
                        salary DECIMAL, 
                        duties TEXT, 
                        requirements TEXT
                    )",
                    // Таблица Сотрудники
                    @"CREATE TABLE IF NOT EXISTS Employee (
                        id INTEGER PRIMARY KEY, 
                        full_name TEXT, 
                        age INT, 
                        gender BOOL, 
                        address TEXT, 
                        phone TEXT, 
                        passport_data TEXT,
                        position_id INTEGER,
                        FOREIGN KEY (position_id) REFERENCES Position(id)
                    )",
                    // Таблица Категории комплектующих
                    @"CREATE TABLE IF NOT EXISTS ComponentType (
                        id INTEGER PRIMARY KEY, 
                        name TEXT, 
                        description TEXT
                    )",        
                    // Таблица Комплектующие
                    @"CREATE TABLE IF NOT EXISTS Component (
                        id INTEGER PRIMARY KEY, 
                        type_id INTEGER,
                        brand TEXT, 
                        manufacturer_company TEXT, 
                        manufacturer_country TEXT, 
                        release_date TEXT,
                        specifications TEXT, 
                        warranty INT, 
                        description TEXT, 
                        price DECIMAL,
                        FOREIGN KEY (type_id) REFERENCES ComponentType(id)
                    )",
                    // Таблица Клиенты
                    @"CREATE TABLE IF NOT EXISTS Customer (
                        id INTEGER PRIMARY KEY, 
                        full_name TEXT,
                        address TEXT,
                        phone TEXT
                    )",
                    // Таблица Услуги
                    @"CREATE TABLE IF NOT EXISTS Service (
                        id INTEGER PRIMARY KEY, 
                        name TEXT,
                        description TEXT,
                        price DECIMAL
                    )",
                    // Таблица Заказы
                    @"CREATE TABLE IF NOT EXISTS [Order] (
                        id INTEGER PRIMARY KEY,
                        order_date TEXT,
                        completion_date TEXT,
                        customer_id INTEGER,
                        component1_id INTEGER,
                        component2_id INTEGER,
                        component3_id INTEGER,
                        prepayment DECIMAL,
                        is_paid BOOL,
                        is_completed BOOL,
                        total_amount DECIMAL,
                        total_warranty TEXT,
                        service1_id INTEGER,
                        service2_id INTEGER,
                        service3_id INTEGER,
                        employee_id INTEGER,
                        FOREIGN KEY (customer_id) REFERENCES Customer(id),
                        FOREIGN KEY (component1_id) REFERENCES Component(id),
                        FOREIGN KEY (component2_id) REFERENCES Component(id),
                        FOREIGN KEY (component3_id) REFERENCES Component(id),
                        FOREIGN KEY (service1_id) REFERENCES Service(id),
                        FOREIGN KEY (service2_id) REFERENCES Service(id),
                        FOREIGN KEY (service3_id) REFERENCES Service(id),
                        FOREIGN KEY (employee_id) REFERENCES Employee(id)
                    )"
                };

                foreach (string commandText in createTableCommands)
                {
                    using (var command = new SQLiteCommand(commandText, m_dbConn))
                    {
                        command.ExecuteNonQuery();
                    }
                }

                dbState = State.Connected;
                //MessageBox.Show("База данных успешно создана!");
            }
            catch (SQLiteException ex)
            {
                dbState = State.Disconnected;
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        public void ConnectToDB()
        {
            if (!File.Exists(dbFilePath))
            {
                MessageBox.Show("Please, create DB and blank table (Push\"Create\" button)");
            }
            try
            {
                m_dbConn = new SQLiteConnection("Data Source=" + dbFilePath + ";Version=3");
                m_dbConn.Open();
                m_sqlCmd.Connection = m_dbConn;
                dbState = State.Connected;
            }
            catch (SQLiteException ex)
            {
                dbState = State.Disconnected;
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        public void Dispose()
        {
            m_sqlCmd?.Dispose();
            m_dbConn?.Close();
            m_dbConn?.Dispose();
        }
    }
}