// using для OperationContext
using System.ServiceModel;

namespace wcf_chat
{
    // Класс-контейнер для хранения данных о пользователе
    // 
    internal class ServerUser
    {
        // Свойства-информация о пользователе
        public int ID { get; set; }

        public string loginPublic { get; set; }


        public string mss { get; set; }

        public string Name { get; set; }
        
        // Поле, которое содержит информацию о подключении клиента к сервису
        // Чтобы когда нужно, была возможность к тому юзеру, который уже подключался, обратиться со стороны сервиса
        public OperationContext operationContext { get; set; }
    
    
    }

}
