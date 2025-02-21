using ChatClient.ServiceChat;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace ChatClient
{
    public partial class Register : Page
    {
        public Register()
        {
            InitializeComponent();
        }

        ServiceChatClient client;
        public string loginReg;
        public string passwordReg;
        public static string emailReg;
        public static string otvetReg;
        public static string otvetCode;
        public static string otvetStatus;

        // Событие нажатия кнопки регистрации пользователя
        private void enterButton_Click(object sender, RoutedEventArgs e)
        {
            loginReg = loginTBox.Text;
            passwordReg = passwordTBox.Text;
            emailReg = emailTBox.Text;

            client = MainWindow.client;
            otvetStatus = null;

            if (emailReg == "")
            {
                otvetReg = client.Register(loginReg, passwordReg, "").Item1;
                if (otvetReg != "Ошибка добавления пользователя. Возможно логин занят ERROR4")
                {
                    if (emailReg != "")
                    {
                        MessageBox.Show("Почта есть");
                        MessageBox.Show($"Статус регистрации: {otvetReg}");
                    }
                    else
                    {
                        MessageBox.Show("Почты нет");
                        MessageBox.Show($"Статус регистрации: {otvetReg}");
                    }
                    MessageBox.Show("Пользователь успешно зарегистрирован");
                    NavigationService.Navigate(MainWindow.listPage[0]);
                }
                else
                {
                    MessageBox.Show("Пользователь не зарегистрирован. Возможно логин уже занят. 1");
                }
            }
            else if (client.two_access() == "NO")
            {
                (string, string) otvet = client.Register(loginReg, passwordReg, emailReg);
                otvetReg = otvet.Item1;
                otvetCode = otvet.Item2;
                otvetStatus = "OK";
                NavigationService.Navigate(MainWindow.listPage[5]);
            }
        }

        // Событие нажатия кнопки возврата на прошлую страницу
        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(MainWindow.listPage[0]);
        }
    }
}
