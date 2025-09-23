using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace Simple_dataBase_UI_Individual.DBLibriary
{
    class MainFunctions
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


        private String dbFilePath;
        private SQLiteConnection m_dbConn;
        private SQLiteCommand m_sqlCmd;
        MainFunctions(String dbFilePath)
        {

            m_dbConn = new SQLiteConnection();
            m_sqlCmd = new SQLiteCommand();
            this.dbFilePath = dbFilePath;
        }
        public void CreateTable()
        {
            if (!File.Exists(dbFilePath))
                SQLiteConnection.CreateFile(dbFilePath);
            try
            {
                m_dbConn = new SQLiteConnection("Data Source=" + dbFilePath + ";Vresion=3");
                m_dbConn.Open();
                m_sqlCmd.Connection = m_dbConn;

                m_sqlCmd.CommandText = "CREATE TABLE IF NOT EXISTS Catalog (id INTEGER PRIMARY KEY AUTOINCREMENT, " +
                    "author TEXT, book TEXT, count_page int check(count_page>10))";
                m_sqlCmd.ExecuteNonQuery();

                _dbStatus = true;
            }
            catch (SQLiteException ex)
            {
                _dbStatus = false;
                MessageBox.Show("Error: " + ex.Message);
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
            if (m_dbConn.State != System.Data.ConnectionState.Open)
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
                String sqlQuery;
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
