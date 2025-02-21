using ChatClient;
using System.Data.SqlClient;
using System.Data;

namespace wcf_chat
{
    // Класс для создания новой таблицы списка контактов пользователя и новой таблицы черного списка
    internal class NewContactList
    {
        DataBase database = new DataBase();

        public NewContactList(string newLogin)
        {
            // Классы, необходимые для работы с базой данных
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();
            // Строка запроса
            string queryString1 = $"use kurs; create table {newLogin + "Contacts"} (contact_id int identity(1,1) not null, contact_login varchar(20) not null primary key (contact_id));";
            string queryString2 = $"use kurs; create table {newLogin + "BlackContacts"} (contact_id int identity(1,1) not null, contact_login varchar(20) not null primary key (contact_id));";


            // Объект класса sqlCommand, чтобы все заработало
            SqlCommand command1 = new SqlCommand(queryString1, database.getConnection());
            SqlCommand command2 = new SqlCommand(queryString2, database.getConnection());


            // Работа с адаптером
            adapter.SelectCommand = command1;
            adapter.Fill(table);

            adapter.SelectCommand = command2;
            adapter.Fill(table);
            database.closeConnection();

        }
    }
}
