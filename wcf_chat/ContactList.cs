using ChatClient;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace wcf_chat
{
    // Класс для работы с контактами и чатами
    internal class ContactList
    {
        // Объект для работы с базой данных
        DataBase database = new DataBase();

        // Создание переменной с списком контактов
        private List<string> contList;
        // Создание переменной с черным списком
        private List<string> contBlackList;
        // Создание переменной с логином
        private string login;
        // Создание переменной с списком всех пользователей в базе
        private List<string> allUsersList;

        // Главный конструктор 
        public ContactList(string login)
        {
            this.login = login;
            contList = new List<string>();
            contBlackList = new List<string>();
            allUsersList = new List<string>();
            
            // Классы, необходимые для работы с базой данных
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();
            // Строка запроса
            string queryString = $"use kurs; select contact_login from {login + "Contacts"};";
            string queryBlackString = $"use kurs; select contact_login from {login + "BlackContacts"};";

            // Объект класса sqlCommand
            SqlCommand command1 = new SqlCommand(queryString, database.getConnection());
            SqlCommand commandBlack = new SqlCommand(queryBlackString, database.getConnection());

            // Работа с адаптером
            adapter.SelectCommand = command1;
            adapter.Fill(table);

            DataRow[] rows = table.Select();

            for (int i = 0; i < rows.Length; i++) { contList.Add(rows[i][0].ToString()); }

            // Работа с адаптером
            adapter.SelectCommand = commandBlack;
            adapter.Fill(table);

            DataRow[] rowsBlack = table.Select();

            for (int i = 0; i < rowsBlack.Length; i++) { contBlackList.Add(rowsBlack[i][0].ToString()); }

            database.closeConnection();
            
        }

        // Метод для создания нового сообщения в чате
        public string NewMessage(string log, string actLog, string message)
        {
            try
            {
                List<string> listMess = new List<string>();

                SqlDataAdapter adapter = new SqlDataAdapter();
                DataTable table = new DataTable();

                string log1 = actLog;
                string log2 = log;

                // Сравнение того, какая строка выше по словарю.
                // Чтобы создавалась таблица с корректным названием
                int sravn = string.Compare(log1, log2);

                if (sravn > 0)
                {
                    string per = log1;
                    log1 = log2;
                    log2 = per;
                }

                string queryString = $"use kurs; insert into {log1 + "CHAT" + log2} (mess) values ('{message}');";

                SqlCommand commandBlack = new SqlCommand(queryString, database.getConnection());

                // Работа с адаптером
                adapter.SelectCommand = commandBlack;
                adapter.Fill(table);

                database.closeConnection();

                return "OK";
            }
            catch
            {
                database.closeConnection();
                return "NOTOK";
            }
        }

        // Метод для получения списка сообщений в чате
        public List<string> GetChatMesList(string log, string activeLogin)
        {
            List<string> listMess = new List<string>();

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();
            
            string log1 = activeLogin;
            string log2 = log;

            // Сравнение того, какая строка выше по словарю.
            // Чтобы создавалась таблица с корректным названием
            int sravn = string.Compare(log1, log2);

            if (sravn > 0)
            {
                string per = log1;
                log1 = log2;
                log2 = per;
            }

            string queryString = $"use kurs; select mess from {log1 + "CHAT" + log2};";

            SqlCommand commandBlack = new SqlCommand(queryString, database.getConnection());

            // Работа с адаптером
            adapter.SelectCommand = commandBlack;
            adapter.Fill(table);

            DataRow[] rows = table.Select();

            for (int i = 0; i < rows.Length; i++) { listMess.Add(rows[i][0].ToString()); }

            database.closeConnection();

            return listMess;
        }

        // Метод для получения списка всех пользователей
        public List<string> GetAllUsersList()
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            // Строка запроса
            string queryString = $"use kurs; select user_login from users;";

            SqlCommand commandBlack = new SqlCommand(queryString, database.getConnection());

            // Работа с адаптером
            adapter.SelectCommand = commandBlack;
            adapter.Fill(table);

            allUsersList.Clear();
            DataRow[] rows = table.Select();

            for (int i = 0; i < rows.Length; i++) { allUsersList.Add(rows[i][0].ToString()); }

            database.closeConnection();
            return allUsersList;
        }

        // Метод для получения списка контактов пользователя
        public (List<string>, List<string>) GetContactLists()
        {
            contList = new List<string>(); 

            // Классы, необходимые для работы с базой данных
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            // Строка запроса
            string queryString = $"use kurs; select contact_login from {login + "Contacts"};";

            // Объект класса sqlCommand, чтобы все заработало
            SqlCommand command1 = new SqlCommand(queryString, database.getConnection());
            
            // Работа с адаптером
            adapter.SelectCommand = command1;
            adapter.Fill(table);

            DataRow[] rows = table.Select();

            for (int i = 0; i < rows.Length; i++) { contList.Add(rows[i][0].ToString()); }

            contBlackList = new List<string>();
            
            SqlDataAdapter adapter1 = new SqlDataAdapter();
            DataTable table1 = new DataTable();

            string queryBlackString = $"use kurs; select contact_login from {login + "BlackContacts"};";

            database.closeConnection();

            SqlCommand commandBlack = new SqlCommand(queryBlackString, database.getConnection());

            // Работа с адаптером
            adapter1.SelectCommand = commandBlack;
            adapter1.Fill(table1);

            DataRow[] rowsBlack = table1.Select();

            for (int i = 0; i < rowsBlack.Length; i++) { contBlackList.Add(rowsBlack[i][0].ToString()); }

            database.closeConnection();

            return (contList, contBlackList);
        }

        // Метод для добавления нового контакта 
        public string NewContact(string log, string activeLogin)
        {
            string actLog = $"{activeLogin}";

            contList = new List<string>();
            contList = GetContactLists().Item1;

            allUsersList = GetAllUsersList();

            int o = 0;
            // Проверка на то, есть ли уже такой контакт
            for (int i = 0; i < contList.Count; i++)
            {
                if (contList[i] == log)
                {
                    o = 1;
                }
            }

            int h = 0;
            // Проверка на то, есть ли такой пользователь
            for (int i = 0; i < allUsersList.Count; i++)
            {
                if (allUsersList[i] == log)
                {
                    h = 2;
                }
            }

            if (o == 0 & h == 2)
            {
                // Классы, необходимые для работы с базой данных
                SqlDataAdapter adapter = new SqlDataAdapter();
                DataTable table = new DataTable();

                // Строка запроса
                string queryString = $"use kurs; insert into {actLog + "Contacts"} (contact_login) values ('{log}');";

                string queryObrString = $"use kurs; insert into {log + "Contacts"} (contact_login) values ('{actLog}');";

                string log1 = actLog;
                string log2 = log;

                // Сравнение того, какая строка выше по словарю.
                // Чтобы создавалась таблица с корректным названием
                int sravn = string.Compare(log1, log2);

                if (sravn > 0)
                {
                    string per = log1;
                    log1 = log2;
                    log2 = per;
                }

                string queryString2 = $"use kurs; create table {log1 + "CHAT" + log2} (mess_id int identity(1, 1) not null, mess varchar(80) not null, primary key(mess_id));";
                
                // Объект класса sqlCommand, чтобы все заработало
                SqlCommand command1 = new SqlCommand(queryString, database.getConnection());

                // Работа с адаптером
                adapter.SelectCommand = command1;
                adapter.Fill(table);

                database.closeConnection();

                SqlCommand command2 = new SqlCommand(queryString2, database.getConnection());

                SqlDataAdapter adapter2 = new SqlDataAdapter();
                DataTable table2 = new DataTable();

                // Работа с адаптером
                adapter2.SelectCommand = command2;
                adapter2.Fill(table2);

                database.closeConnection();
                
                if (log != actLog)
                {
                    SqlCommand command3 = new SqlCommand(queryObrString, database.getConnection());

                    SqlDataAdapter adapter3 = new SqlDataAdapter();
                    DataTable table3 = new DataTable();

                    // Работа с адаптером
                    adapter3.SelectCommand = command3;
                    adapter3.Fill(table3);

                    database.closeConnection();
                }
                return "OK";
            }
            else if (h == 0)
            {
                return "NOTUSERS";
            }
            else
            {
                return "NOTOK";
            }
        }

        // Метод для удаления контакта
        public string DelContact(string log, string activeLogin)
        {
            try
            {
                string actLog = $"{activeLogin}";

                // Классы, необходимые для работы с базой данных
                SqlDataAdapter adapter = new SqlDataAdapter();
                DataTable table = new DataTable();

                // Строка запроса
                string queryString = $"use kurs; delete {actLog + "Contacts"} where contact_login = '{log}';";

                // Объект класса sqlCommand, чтобы все заработало
                SqlCommand command1 = new SqlCommand(queryString, database.getConnection());

                // Удаление контакта активного пользователя из списка контактов второго пользователя

                // Работа с адаптером
                adapter.SelectCommand = command1;
                adapter.Fill(table);

                database.closeConnection();

                SqlDataAdapter adapter4 = new SqlDataAdapter();
                DataTable table4 = new DataTable();

                // Строка запроса
                string queryString4 = $"use kurs; delete {log + "Contacts"} where contact_login = '{actLog}';";

                // Объект класса sqlCommand
                SqlCommand command4 = new SqlCommand(queryString4, database.getConnection());

                // Работа с адаптером
                adapter4.SelectCommand = command4;
                adapter4.Fill(table4);

                database.closeConnection();

                string log1 = actLog;
                string log2 = log;

                // Сравнение того, какая строка выше по словарю.
                // Чтобы создавалась таблица с корректным названием
                int sravn = string.Compare(log1, log2);

                if (sravn > 0)
                {
                    string per = log1;
                    log1 = log2;
                    log2 = per;
                }

                string queryString2 = $"use kurs; drop table {log1 + "CHAT" + log2};";

                // Работа с адаптером
                adapter.SelectCommand = command1;
                adapter.Fill(table);

                database.closeConnection();

                SqlCommand command2 = new SqlCommand(queryString2, database.getConnection());

                SqlDataAdapter adapter2 = new SqlDataAdapter();
                DataTable table2 = new DataTable();

                // Работа с адаптером
                adapter2.SelectCommand = command2;
                adapter2.Fill(table2);

                database.closeConnection();
                return "OK";
            }
            catch
            {
                database.closeConnection();
                return "NOTOK";
            }
        }

        // Метод для добавления контакта в черный список
        public string NewBlackContact(string log, string activeLogin)
        {
            string actLog = $"{activeLogin}";

            contBlackList = new List<string>();
            contBlackList = GetContactLists().Item2;

            int o = 0;
            // Проверка на то, есть ли уже такой контакт
            for (int i = 0; i < contBlackList.Count; i++)
            {
                if (contBlackList[i] == log)
                {
                    o = 1;
                }
            }
            if (o == 0)
            {
                // Классы, необходимые для работы с базой данных
                SqlDataAdapter adapter = new SqlDataAdapter();
                DataTable table = new DataTable();
                // Строка запроса
                string queryString = $"use kurs; insert into {actLog + "BlackContacts"} (contact_login) values ('{log}');";

                // Объект класса sqlCommand, чтобы все заработало
                SqlCommand command1 = new SqlCommand(queryString, database.getConnection());

                // Работа с адаптером
                adapter.SelectCommand = command1;
                adapter.Fill(table);

                database.closeConnection();
                return "OK";
            }
            else
            {
                return "NOTOK";
            }    
        }

        // Метод для удаления контакта из черного списка
        public string DelBlackContact(string log, string activeLogin)
        {
            try
            {
                string actLog = $"{activeLogin}";
                
                // Классы, необходимые для работы с базой данных
                SqlDataAdapter adapter = new SqlDataAdapter();
                DataTable table = new DataTable();
                // Строка запроса
                string queryString = $"use kurs; delete {actLog + "BlackContacts"} where contact_login = '{log}';";

                // Объект класса sqlCommand, чтобы все заработало
                SqlCommand command1 = new SqlCommand(queryString, database.getConnection());

                // Работа с адаптером
                adapter.SelectCommand = command1;
                adapter.Fill(table);

                database.closeConnection();
                return "OK";
            }
            catch
            {
                database.closeConnection();
                return "NOTOK";
            }
        }
    }
}
