using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using ChatClient.ServiceChat;


namespace ChatClient
{
    public partial class Two_factor_reg : Page
    {
        // Объект типа хоста в клиенте, для взаимодействия с его методами
        ServiceChatClient client;
        public Two_factor_reg()
        {
            InitializeComponent();
            client = MainWindow.client;
        }

        // Событие нажатия кнопки проверки кода
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            string cd = textBox1.Text;
            string status = Register.otvetStatus;
            string codeReg = Register.otvetCode;

            if (status == "OK")
            {
                MessageBox.Show("Проверка пройдена");
                client.two_access_izm("YES");

                if (Register.otvetReg != "Ошибка добавления пользователя. Возможно логин занят ERROR4")
                {
                    if (Register.emailReg != "")
                    {
                        MessageBox.Show("Почта есть");
                        MessageBox.Show($"Статус регистрации: {Register.otvetReg}");
                    }
                    else
                    {
                        MessageBox.Show("Почты нет");
                        MessageBox.Show($"Статус регистрации: {Register.otvetReg}");
                    }
                    MessageBox.Show("Пользователь успешно зарегистрирован");
                    NavigationService.Navigate(MainWindow.listPage[0]);
                }
                else
                {
                    MessageBox.Show("Пользователь не зарегистрирован. Возможно логин уже занят.");
                }
            }
            else
            {
                client.two_access_izm("NO");
                MessageBox.Show("Проверка не пройдена");
            }
        }

        // Событие нажатия кнопки возврата на главную страницу
        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            // Переход к Main
            NavigationService.Navigate(MainWindow.listPage[0]);

            MainWindow.active_user_email = null;
            MainWindow.active_user_login = null;
        }               
    }
}
