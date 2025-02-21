using System.Data.SqlClient;

namespace ChatClient
{
    internal class DataBase
    {
        // Строка подключения
        SqlConnection sqlConnection = new SqlConnection(@"Data Source=DESKTOP-HR0C7M0\SQLEXPRESS; Initial Catalog=sevenFirst; Integrated Security=True; Encrypt=False");
        //SqlConnection sqlConnection = new SqlConnection(@"Data Source=DESKTOP-HR0C7M0\SQLEXPRESS;Initial Catalog=[7_1]; Integrated Security=True; TrustServerCertificate=false;");

        // Метод открывающий связь с базой данных
        public void openConnection()
        {
            // Если статус подключения закрыт, открыть 
            if (sqlConnection.State == System.Data.ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
        }

        // Метод закрывающий связь с базой данных
        public void closeConnection()
        {
            // Если статус подключения закрыт, открыть 
            if (sqlConnection.State == System.Data.ConnectionState.Open)
            {
                sqlConnection.Close();
            }
        }

        // Возвращает строку о состоянии открытия
        public SqlConnection getConnection()
        {
            return sqlConnection;
        }
    }
}
