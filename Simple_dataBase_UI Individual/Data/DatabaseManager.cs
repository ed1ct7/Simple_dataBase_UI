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
    class DatabaseManager
    {
        private bool _dbStatus;
        public bool dbStatus
        {
            get { return _dbStatus; }
            set { _dbStatus = value; }
        }

        private Enum _dbState;
        public Enum dbState
        {
            get { return _dbState; }
            set { _dbState = value; }
        }


        private string _dbFilePath;
        public string dbFilePath
        {
            get { return _dbFilePath; }
            set { _dbFilePath = value; }
        }



        private SQLiteConnection m_dbConn;
        private SQLiteCommand m_sqlCmd;
        DatabaseManager(string dbFilePath)
        {

            m_dbConn = new SQLiteConnection();
            m_sqlCmd = new SQLiteCommand("PRAGMA foreign_keys = ON");
            this.dbFilePath = dbFilePath;
        }
        /*
        Задание №19: БД Компьютерной фирмы.
        Таблицы: 
        1) Сотрудники(Код сотрудника, ФИО, Возраст, Пол, Адрес, Телефон,
        Паспортные данные, Код должности).

        2) Должности(Код должности, Наименование должности, Оклад, Обязанности,
        Требования)

        3) Виды комплектующих(Код вида, Наименование, Описание)

        4) Комплектующие(Код комплектующего, Код вида, Марка, Фирма
        производитель, Страна производитель, Дата выпуска, Характеристики, Срок
        гарантия, Описание, Цена)

        5) Заказчики(Код заказчика, ФИО, Адрес, Телефон).

        6) Услуги(Код услуги, Наименование, Описание, Стоимость)

        7) Заказы(Дата заказа, Дата исполнения, Код заказчика, Код комплектующего 1,
        Код комплектующего 2, Код комплектующего 3, Доля предоплаты, Отметка
        об оплате, Отметка об исполнении, Общая стоимость, Срок общей гарантии,
        Код услуги 1, Код услуги 2, Код услуги 3, Код сотрудника).


        "CREATE TABLE IF NOT EXISTS Catalog (id INTEGER PRIMARY KEY AUTOINCREMENT, " +
                    "author TEXT, book TEXT, count_page int check(count_page>10))";
        */
        public void CreateTable()
        {
            if (!File.Exists(dbFilePath))
                SQLiteConnection.CreateFile(dbFilePath);

            try
            {
                m_dbConn = new SQLiteConnection("Data Source=" + dbFilePath + ";Version=3");
                m_dbConn.Open();
                m_sqlCmd.Connection = m_dbConn;

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
                    @"CREATE TABLE IF NOT EXISTS component_categories (
                        id INTEGER PRIMARY KEY AUTOINCREMENT, 
                        name TEXT, 
                        description TEXT
                    )",        
                    // Таблица Комплектующие
                    @"CREATE TABLE IF NOT EXISTS components (
                        id INTEGER PRIMARY KEY AUTOINCREMENT, 
                        category_id INTEGER,
                        brand TEXT, 
                        manufacturer_company TEXT, 
                        manufacturer_country TEXT, 
                        production_date TEXT,
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

                _dbStatus = true;
                MessageBox.Show("База данных успешно создана!");
            }
            catch (SQLiteException ex)
            {
                _dbStatus = false;
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                m_dbConn?.Close();
            }
        }
        public void DeleteTable() 
        { 
        
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
                _dbStatus = true;
            }
            catch (SQLiteException ex)
            {
                _dbStatus = true;
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void InsertInto()
        {
            if (m_dbConn.State != ConnectionState.Open)
            {
                MessageBox.Show("Open connection");
                return;
            }
            try
            {
                //m_sqlCmd.CommandText = "INSERT INTO Catalog('author', 'book', 'count_page')values('"
                //    + TB_Author.Text + "','" + TB_Book.Text + "','" + Convert.ToInt32(TB_CountPage.Text) + "');"
                //    ;
                m_sqlCmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
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
    }
}
