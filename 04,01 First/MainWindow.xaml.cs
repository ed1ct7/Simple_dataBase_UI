using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _04_01_First
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private String dbFileName;
        private SQLiteConnection m_dbConn;
        private SQLiteCommand m_sqlCmd;

        public MainWindow()
        {
            m_dbConn = new SQLiteConnection();
            m_sqlCmd = new SQLiteCommand();
            dbFileName = "sample.sqlite";

            InitializeComponent();
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            if (!File.Exists(dbFileName))
                SQLiteConnection.CreateFile(dbFileName);
            try
            {
                m_dbConn = new SQLiteConnection("Data Source=" + dbFileName + ";Vresion=3");
                m_dbConn.Open();
                m_sqlCmd.Connection = m_dbConn;

                m_sqlCmd.CommandText = "CREATE TABLE IF NOT EXISTS Catalog (id INTEGER PRIMARY KEY AUTOINCREMENT, " +
                    "author TEXT, book TEXT, count_page int check(count_page>10))";
                m_sqlCmd.ExecuteNonQuery();

                TB_Status.Text = "Connected";
            }
            catch (SQLiteException ex)
            {
                TB_Status.Text = "Disconnected";
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            if (!File.Exists(dbFileName))
            {
                MessageBox.Show("Please, create DB and blank table (Push\"Create\" button)");
            }
            try
            {
                m_dbConn = new SQLiteConnection("Data Source=" + dbFileName + ";Version=3");
                m_dbConn.Open();
                m_sqlCmd.Connection= m_dbConn;
                TB_Status.Text = "Connected";
            }
            catch (SQLiteException ex){
                TB_Status.Text = "Disconnected";
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        private void AddButton_Click(Object sender, RoutedEventArgs e)
        {
            if(m_dbConn.State != System.Data.ConnectionState.Open)
            {
                MessageBox.Show("Open connection");
                return;
            }
            try
            {
                m_sqlCmd.CommandText = "INSERT INTO Catalog('author', 'book', 'count_page')values('"
                    +TB_Author.Text + "','" + TB_Book.Text + "','" + Convert.ToInt32(TB_CountPage.Text) + "');"
                    ;
                m_sqlCmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void ReadAllButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataSet dataSet = new DataSet();
                String sqlQuery;
                sqlQuery = "SELECT * FROM Catalog ";
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqlQuery, m_dbConn);
                adapter.Fill(dataSet);
                dbView.ItemsSource = dataSet.Tables[0].DefaultView;
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

    }
}