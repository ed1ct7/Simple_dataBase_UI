using System;
using System.Collections.Generic;
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
        public DatabaseManager(string dbFilePath)
        {
            m_dbConn = new SQLiteConnection("Data Source=" + dbFilePath + ";Version=3");
            m_dbConn.Open();
            m_sqlCmd = new SQLiteCommand("PRAGMA foreign_keys = ON");
            m_sqlCmd.Connection = m_dbConn;
            this.dbFilePath = dbFilePath;
            if (!File.Exists(dbFilePath))
                SQLiteConnection.CreateFile(dbFilePath);
        }
        public void CreateTable()
        {
            try
            {
                string[] createTableCommands = {
                    // Таблица Должности
                    @"CREATE TABLE IF NOT EXISTS positions (
                        id INTEGER PRIMARY KEY AUTOINCREMENT, 
                        name TEXT, 
                        salary DECIMAL, 
                        duties TEXT, 
                        requirements TEXT
                    )",
                    // Таблица Сотрудники
                    @"CREATE TABLE IF NOT EXISTS employees (
                        id INTEGER PRIMARY KEY AUTOINCREMENT, 
                        full_name TEXT, 
                        age INT, 
                        gender BOOL, 
                        address TEXT, 
                        phone TEXT, 
                        passport_data TEXT,
                        position_id INTEGER,
                        FOREIGN KEY (position_id) REFERENCES positions(id)
                    )",
                    // Таблица Категории комплектующих
                    @"CREATE TABLE IF NOT EXISTS component_type (
                        id INTEGER PRIMARY KEY AUTOINCREMENT, 
                        name TEXT, 
                        description TEXT
                    )",        
                    // Таблица Комплектующие
                    @"CREATE TABLE IF NOT EXISTS components (
                        id INTEGER PRIMARY KEY AUTOINCREMENT, 
                        type_id INTEGER,
                        brand TEXT, 
                        manufacturer_company TEXT, 
                        manufacturer_country TEXT, 
                        release_date TEXT,
                        specifications TEXT, 
                        warranty TEXT, 
                        description TEXT, 
                        price DECIMAL,
                        FOREIGN KEY (category_id) REFERENCES component_categories(id)
                    )",
                    // Таблица Клиенты
                    @"CREATE TABLE IF NOT EXISTS customers (
                        id INTEGER PRIMARY KEY AUTOINCREMENT, 
                        full_name TEXT,
                        address TEXT,
                        phone TEXT
                    )",
                    // Таблица Услуги
                    @"CREATE TABLE IF NOT EXISTS services (
                        id INTEGER PRIMARY KEY AUTOINCREMENT, 
                        name TEXT,
                        description TEXT,
                        price DECIMAL
                    )",
                    // Таблица Заказы
                    @"CREATE TABLE IF NOT EXISTS orders (
                        id INTEGER PRIMARY KEY AUTOINCREMENT,
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
                        FOREIGN KEY (customer_id) REFERENCES customers(id),
                        FOREIGN KEY (component1_id) REFERENCES components(id),
                        FOREIGN KEY (component2_id) REFERENCES components(id),
                        FOREIGN KEY (component3_id) REFERENCES components(id),
                        FOREIGN KEY (service1_id) REFERENCES services(id),
                        FOREIGN KEY (service2_id) REFERENCES services(id),
                        FOREIGN KEY (service3_id) REFERENCES services(id),
                        FOREIGN KEY (employee_id) REFERENCES employees(id)
                    )"
                };

                foreach (string commandText in createTableCommands)
                {
                    m_sqlCmd.CommandText = commandText;
                    m_sqlCmd.ExecuteNonQuery();
                }

                dbState = State.Connected;
                MessageBox.Show("База данных успешно создана!");
            }
            catch (SQLiteException ex)
            {
                dbState = State.Disconnected;
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                m_dbConn?.Close();
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
        private void GetTable()
        {
            try
            {
                DataSet dataSet = new DataSet();
                string sqlQuery;
                sqlQuery = "SELECT * FROM Catalog ";
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqlQuery, m_dbConn);
                adapter.Fill(dataSet);
                //dbView.ItemsSource = dataSet.Tables[0].DefaultView;
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        public void DeleteTable()
        {

        }
    }
}
