using System.Collections.Generic;
using System.ServiceModel;

namespace wcf_chat
{    
    // Атрибутом CallbackContract даем сервису знать, что у него есть интерфейс с колбеком,
    // который он может вызывать на стороне клиента
    [ServiceContract(CallbackContract = typeof(IServerChatCallback))]
    public interface IServiceChat
    {
        // Метод для создания новой строки с данными о пользователе в таблице с данными о пользователях.
        [OperationContract]
        string NewUserData(string name, string last_name, string status);
        // Метод для получения данных о пользователе
        [OperationContract]
        (string, string, string) GetUserData();
        // Метод для добавления нового сообщения в таблицу чата
        [OperationContract]
        string NewMessChat(string log, string message);
        // Метод для получения списка контактов 
        [OperationContract]
        (List<string>, List<string>) GetContList();
        // Метод для добавления нового контакта
        [OperationContract]
        string NewCont(string log);
        // Метод для получения списка сообщений чата из базы данных
        [OperationContract]
        List<string> GetChatMesList(string log);
        // Метод для добавления контакта в черный список
        [OperationContract]
        string NewBlackCont(string log);
        // Метод для удаления контакта из черного списка
        [OperationContract]
        string DelBlackCont(string log);
        // Метод для удаления контакта
        [OperationContract]
        string DelCont(string log);
        // Метод для авторизации пользователя
        [OperationContract]
        (string log, string email, string twoStatus) Login(string log, string pas);
        // Метод для проверки кода двухфакторной аутентификации 
        [OperationContract]
        string TwoFact(string UserCode);
        // Метод для получения списка заметок пользователя
        [OperationContract]
        List<string> GetUserNotes();
        // Метод для добавления новой заметки в таблицу с заметками пользователя
        [OperationContract]
        string AddUserNote(string note);
        // Метод для очистки таблицы заметок пользователя
        [OperationContract]
        string DellUserNote();
        // Метод для получения статуса двухфакторной аутентификации пользователя
        [OperationContract]
        string two_access();
        // Метод для изменения статуса двух
        [OperationContract]
        void two_access_izm(string status);
        // Метод для получения логина активного пользователя
        [OperationContract]
        string get_active_login();
        // Метод для регистрации пользователя
        [OperationContract]
        (string, string) Register(string log, string pass, string email);

        // [OperationContract]
        // Этот атрибут говорит о том, что этот интерфейс будет представлять собой 
        // договоренность о том, каким образом клиент будет взаимодействовать с сервисом
        // У каждого метода в этом интерфейсе, с помощью которого мы должны иметь возможность взаимодействовать с сервисом
        // , со стороны клиента, долженм быть атрибут OperationContract
        // Метод, у которого есть этот атрибут, будет виден со стороны клиента
        // 
        /*
        

        // Методы описанные в этом сервисе, это те функции, которыми может пользоваться клиент работая с сервером
        // Здесь описывается основной функционал сервиса
        // Это только интерфейс

        // Методы для работы с сервисом:


        // Метод для подключения к сервису
        int Connect(string name);

        // Метод для отключения от сервиса. 
        // Вызывается когда клиент покидает чат, либо нажимает кнопку отключения от чата
        // Нужен чтобы сообщить серсису о том, что такого клиента у него больше нет.
        [OperationContract]
        void Disconnect(int id);

        // Если вызывается какой-то метод, клиент посылает сервису какое-то сообщение и ждет
        // ответ от сервиса о получении ответа. И даже если метод не возвращает никагого ответа, 
        // то клиент все равно будет заблокирован до того момента, пока сервер ему не ответит и клиент не получит такой ответ.
        // Но в некоторых случаях этот ответ ждать не требуется
        // Если не нужно ожидать ответа от сервера, то нужно добавить атрибут к OperationContract


        
        */
    }

    // Для того, чтобы сервер разослал сообщение всем клиентам


    // Чтобы вызвать какое либо действия на стороне клиента со стороны сервера, требуется колбек функция 
    // Колбек функция - функция обратного вызова, передает исполняемы код в качестве одного из параметров другого кода.
    // Обратный вызов позволяет в функции исполять код, который задается в аргументах при ее вызове

    // На серверной части для этого описывается интерфейс, а реализация этой колбек функции должна быть в клиентской части 
    // (то, каким образом клиент будет получать сообщение от сервера)
    // Интерфейс для этого: 
    public interface IServerChatCallback
    {
        // Добавляю параметр (IsOneWay = true), потому что без этого, сервер будет ожидать ответа клиента о получении колбека
        [OperationContract(IsOneWay = true)]
        void MsgCallback(string msg);
    }




}
