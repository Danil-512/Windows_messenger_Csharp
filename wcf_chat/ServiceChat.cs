using ChatClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.ServiceModel;

namespace wcf_chat
{
    // По умолчанию, когда клиент обращается к сервису, то сервер(хост), для каждого
    // такого клиента, создает свой экземпляр сервиса (даже для каждого подключения)
    // По этому, когда нескоько клиентов подключаются к сервису, у каждого клиента свой сервис
    // Но в чате сервис должен быть один и тот же, т.к. сервис должен знать о всех клиентах и посылать сообщение всем
    // клиентам сразу, которые поключились к сервису
    // Для того чтобы это сделать, есть два способа:
    // 1) Создать список клиентов, которые подключаются к сервису в статическом списке
    // т.к. поле со списком будет static, то оно будет общее для всех сервисов
    // 2) Функционал WCF для того, чтобы сделать сервис общим для всех клиентов:
    // указать атрибут [ServiceBehavior] (поведение сервиса) 
    // Параметр (InstanceContextMode =) определяет то, каким образом будет работать сервис
    // InstanceContextMode.PerCall - при каждом обращении к сервису, будет создаваться свой собственный сервис при каждом обращении клиента. И как только он отработает, он уничтожается
    // InstanceContextMode.PerSession - как только клиент подключился, для него создается собственная сессия, когда он отключается, сессия уничтожается
    // Но в таком случае у каждого клиента все равно будет своя отдельная сессия, свой сервис
    // InstanceContextMode.Single - все клиенты, которые будут подключаться к хосту, будут работать с единым сервисом
    // Это очень важный параметр

    // Класс для реализации интерфейса, написаного в родиельском классе
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class ServiceChat : IServiceChat
    {
        DataBase database = new DataBase();

        // Логин активного пользователя
        private string active_user_login = null;
        // Почта активного пользователя
        public string active_user_email = null;
        // Переменная для хранения кода двухфакторной аутентификации пользователя
        public string myCode = null;
        // Переменная для хранения правильного кода двухфакторной аутентификации 
        public string temporary_code = null;
        // Переменная для хранения статуса двухфакторной аутентификации
        public string two_access = "NO";
        // Переменная для хранения списка заметок пользователя
        public List<string> user_notes = null;
        // Экземпляр класса для работы с контактами 
        ContactList contLists = null;
        // Экземпляр класса обработки ошибок
        ErrorMy myError = new ErrorMy();

        // Метод для создания новой строки с данными о пользователе в таблице с данными о пользователях.
        public string NewUserData(string name, string last_name, string status)
        {
            try
            {
                // Классы, необходимые для работы с базой данных
                SqlDataAdapter adapter = new SqlDataAdapter();
                DataTable table = new DataTable();
                // Строка запроса
                string queryString = $"use kurs; update usersData set user_first_name = '{name}', user_last_name = '{last_name}', user_status = '{status}' where user_login = '{active_user_login}';";

                // Объект класса sqlCommand, чтобы все заработало
                SqlCommand command1 = new SqlCommand(queryString, database.getConnection());

                // Работа с адаптером
                adapter.SelectCommand = command1;
                adapter.Fill(table);

                return "OK";
            }
            catch
            {
                return "NOTOK";
            }
        }

        // Метод для получения данных о пользователе
        public (string, string, string) GetUserData()
        {
            string name = "";
            string last_name = "";
            string status = "";

            // Классы, необходимые для работы с базой данных
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();
            // Строка запроса
            string queryString = $"use kurs; select user_first_name, user_last_name, user_status from usersData where user_login = '{active_user_login}';";

            // Объект класса sqlCommand, чтобы все заработало
            SqlCommand command1 = new SqlCommand(queryString, database.getConnection());

            // Работа с адаптером
            adapter.SelectCommand = command1;
            adapter.Fill(table);

            DataRow[] rows = table.Select();

            name = rows[0][0].ToString();
            last_name = rows[0][1].ToString();
            status = rows[0][2].ToString();

            return (name, last_name, status);
        }

        // Метод для добавления нового сообщения в таблицу чата
        public string NewMessChat(string log, string message)
        {
            string newMess = $"{active_user_login + " " + DateTime.Now.Date.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + ": " + message}";
            string status = contLists.NewMessage(log, active_user_login, newMess);
                       
            return status;
        }

        // Метод для получения списка сообщений чата из базы данных
        public List<string> GetChatMesList(string log)
        {
            List<string> listMess = new List<string>();
            listMess = contLists.GetChatMesList(log, active_user_login);

            return listMess;
        }
        
        // Метод для добавления нового контакта
        public string NewCont(string log)
        {
            string status = contLists.NewContact(log, active_user_login);

            return status;
        }

        // Метод для добавления контакта в черный список
        public string NewBlackCont(string log)
        {
            string status = contLists.NewBlackContact(log, active_user_login);

            return status;
        }

        // Метод для удаления контакта из черного списка
        public string DelBlackCont(string log)
        {
            contLists.DelBlackContact(log, active_user_login);
            return "OK";
        }

        // Метод для удаления контакта
        public string DelCont(string log)
        {
            contLists.DelContact(log, active_user_login);

            return "OK";
        }

        // Метод для получения списка контактов 
        public (List<string>, List<string>) GetContList()
        {
            contLists = null;
            contLists = new ContactList(active_user_login);

            List<string> listCont = new List<string>();
            List<string> listBlackCont = new List<string>();

            listCont = contLists.GetContactLists().Item1;
            listBlackCont = contLists.GetContactLists().Item2;

            return (listCont, listBlackCont);
        }
        
        // Метод для получения списка заметок пользователя
        public List<string> GetUserNotes()
        {
            user_notes = null;
            user_notes = new List<string>();

            // Классы, необходимые для работы с базой данных
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();
            // Строка запроса
            string queryString = $"use kurs; select note_date, note_text from {active_user_login + "Notes"};";

            // Объект класса sqlCommand, чтобы все заработало
            SqlCommand command1 = new SqlCommand(queryString, database.getConnection());

            // Работа с адаптером
            adapter.SelectCommand = command1;
            adapter.Fill(table);

            DataRow[] rows = table.Select();

            for (int i = 0; i < rows.Length; i++) { user_notes.Add(rows[i][0].ToString() + " " + rows[i][1].ToString()); }

            database.closeConnection();

            return user_notes;
        }

        // Метод для добавления новой заметки в таблицу с заметками пользователя
        public string AddUserNote(string note)
        {
            // Классы, необходимые для работы с базой данных
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            // Строка запроса
            string queryString = $"use kurs; insert into {active_user_login + "Notes"} (user_login, note_date, note_text) values ('{active_user_login}', '{DateTime.Now.Date.ToLongDateString() + " " + DateTime.Now.ToLongTimeString()}', '{note}');";

            // Объект класса sqlCommand, чтобы все заработало
            SqlCommand command1 = new SqlCommand(queryString, database.getConnection());

            // Работа с адаптером
            adapter.SelectCommand = command1;
            adapter.Fill(table);

            database.closeConnection();

            return "OK";
        }

        // Метод для очистки таблицы заметок пользователя
        public string DellUserNote()
        {
            // Классы, необходимые для работы с базой данных
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            // Строка запроса
            string queryString = $"use kurs; delete {active_user_login + "Notes"};";

            // Объект класса sqlCommand, чтобы все заработало
            SqlCommand command1 = new SqlCommand(queryString, database.getConnection());

            // Работа с адаптером
            adapter.SelectCommand = command1;
            adapter.Fill(table);

            database.closeConnection();

            return "OK";
        }

        // Метод для получения логина активного пользователя
        string IServiceChat.get_active_login()
        {
            database.closeConnection();
            string al;
            al = $"{active_user_login}";
            
            return al;
        }

        // Метод для получения статуса двухфакторной аутентификации пользователя
        string IServiceChat.two_access()
        {
            return two_access;
        }
                
        // Метод для изменения статуса двух
        void IServiceChat.two_access_izm(string status)
        {
            two_access = status;
        }
        
        // Метод для регистрации пользователя
        public (string, string) Register(string log, string pass, string email)
        {
            database.closeConnection();

            string newLogin = log;
            //Хэширование пароля
            string newPassword = Md5.hashPassword(pass);
            string newEmail = email;
            string statusReg = "NO";
            string codeSend = null;

            // Классы, необходимые для работы с базой данных
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            // Строка запроса
            string queryString1 = $"use kurs; select user_login from users where user_login = '{newLogin}';";

            // Объект класса sqlCommand, чтобы все заработало
            SqlCommand command1 = new SqlCommand(queryString1, database.getConnection());

            // Работа с адаптером
            adapter.SelectCommand = command1;
            adapter.Fill(table);

            if (table.Rows.Count == 0)
            {
                statusReg = "Успешно добавлен пользователь 1";

                if (newEmail != "")
                {
                    TwoFactClass two = new TwoFactClass(newEmail);
                    //codeSend = $"{two.GetCode()}";
                    codeSend = $"{two.GetCode()}";

                    two.status = null;
                    two = null;
                }

                NewContactList newContactList1 = new NewContactList(newLogin);

                string queryString2 = $"use kurs; insert into users (user_login, user_password, user_email) values ('{newLogin}', '{newPassword}', '{newEmail}');";
                string queryString3 = $"use kurs; create table {newLogin + "Notes"} (note_id int identity(1,1) not null, user_login varchar(20) not null, note_date varchar(30) not null, note_text varchar(200) not null primary key (note_id));";
                string queryString4 = $"use kurs; insert into usersData (user_login, user_first_name, user_last_name, user_status) values ('{newLogin}', ' ', ' ', 'Новый пользователь');";

                // Объект класса sqlCommand
                SqlCommand command2 = new SqlCommand(queryString2, database.getConnection());
                SqlCommand command3 = new SqlCommand(queryString3, database.getConnection());
                SqlCommand command4 = new SqlCommand(queryString4, database.getConnection());

                // Работа с адаптером
                adapter.SelectCommand = command2;
                adapter.Fill(table);

                adapter.SelectCommand = command3;
                adapter.Fill(table);

                adapter.SelectCommand = command4;
                adapter.Fill(table);

                statusReg = "Успешно добавлен пользователь";
            }
            else
            {
                statusReg = myError.registerAlreadyExistingAccount();
            }

            database.closeConnection();

            return (statusReg, codeSend);
        }

        // Метод для проверки кода двухфакторной аутентификации 
        public string TwoFact(string UserCode)
        {
            string usCode = UserCode;

            if (myCode == usCode)
            {
                return "OK";
            }
            return myError.wrongTwoCode();
        }

        // Метод для авторизации пользователя
        public (string log, string email, string twoStatus) Login(string log, string pas)
        {
            database.closeConnection();

            string login = log;
            string password = Md5.hashPassword(pas);

            // Классы, необходимые для работы с базой данных
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();
            
            // Строка запроса
            string queryString = $"use kurs; select user_login, user_password, user_email from users where user_login = '{login}' and user_password = '{password}'";

            // Объект класса sqlCommand
            SqlCommand command = new SqlCommand(queryString, database.getConnection());

            // Работа с адаптером
            adapter.SelectCommand = command;
            adapter.Fill(table);

            DataRow[] rows = table.Select();

            string sqlStr = "";
            for (int i = 0; i < rows.Length; i++) { sqlStr += rows[i][2]; }

            database.closeConnection();

            string twoOk = null;

            if (table.Rows.Count == 1)
            {
                try
                {
                    active_user_email = rows[0][2].ToString();
                }
                catch
                {
                    return ("Ошибка2", "Ошибка2", "Ошибка2");
                }
                active_user_login = $"{login}";

                if (active_user_email == "")
                {
                    active_user_email = "132";
                }
                else
                {
                    TwoFactClass two = new TwoFactClass(active_user_email);
                    twoOk = $"{two.status}";
                    myCode = two.cod;
                    two.status = null;
                }
                active_user_login = log;
                return (active_user_login, active_user_email, twoOk);                             
            }
            else
            {
                return ("Ошибка1", "Ошибка1", "Ошибка1");
            }
        }
    }
}
