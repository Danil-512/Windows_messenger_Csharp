using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wcf_chat
{
    internal class ErrorMy
    {
        public string registerAlreadyExistingAccount()
        {
            return "Аккаунт с таким логином уже существует";
        }
        public string anotherError()
        {
            return "Неопознанная ошибка";
        }
        public string wrongPassword()
        {
            return "Неправильный пароль";
        }
        public string wrongTwoCode()
        {
            return "Неверный код двухфакторной аутентификации";
        }

    }
}
