using System;
using System.Net.Mail;
using System.Net;

namespace wcf_chat
{
    // Класс для реализации двухфакторной аутентификации
    internal class TwoFactClass
    {
        public string email;
        public string cod;
        public string status = "-";
        private string codeNew = null;

        // Метод для отправки сообщения на почту
        private void SendMessage(string text, string toUser)
        {
            // отправитель - устанавливаем адрес и отображаемое в письме имя
            MailAddress from = new MailAddress("testpost7788@gmail.com", "TEST");
            // кому отправляем
            MailAddress to = new MailAddress(toUser);
            // создаем объект сообщения
            MailMessage m = new MailMessage(from, to);
            // тема письма
            m.Subject = $"Код безопасности<";
            // текст письма
            m.Body = $"{text}";
            // письмо представляет код html
            m.IsBodyHtml = true;
            // адрес smtp-сервера и порт, с которого будем отправлять письмо
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            // логин и пароль
            smtp.Credentials = new NetworkCredential("testpost7788@gmail.com", "agno sbkk crup iiei");
            smtp.EnableSsl = true;
            smtp.Send(m);
            Console.WriteLine("Сообщение отправлено на почту1");
        }

        // Метод для получения отправленного кода
        public string GetCode()
        {
            return codeNew;
        }

        // Конструктор класса
        public TwoFactClass(string em)
        {
            email = em;

            SendCode();
        }

        // Метод для создания кода и отправки его на почту
        private (string, string) SendCode()
        {
            try
            {
                Random random = new Random();
                int setn_int = random.Next(10000, 99999);
                cod = Convert.ToString(setn_int);

                codeNew = cod;

                SendMessage(cod, email);
                Console.WriteLine("Сообщение отправлено на почту");

                status = "OK";
                return ("OK", cod);
            }
            catch
            {
                status = "NOTOK";
                return ("NOTOK", "NULL");
            }
        }
    }
}
