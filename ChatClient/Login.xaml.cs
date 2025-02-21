using ChatClient.ServiceChat;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;


namespace ChatClient
{
    public partial class Login : Page, ServiceChat.IServiceChatCallback
    {
        // Объект типа хоста в клиенте, для взаимодействия с его методами
        ServiceChatClient client;
        
        public Login()
        {
            InitializeComponent();
        }

        private void enterButton_Click(object sender, RoutedEventArgs e)
        {
            client = MainWindow.client;

            string log = loginTBox.Text;
            string pas = registerTBox.Text;

            (string, string, string) gg = client.Login(log, pas);

            if (gg.Item1 == "Ошибка1")
            {
                MessageBox.Show($"Авторизация не прошла.");
            }
            else if (gg.Item2 != "123")
            {
                MainWindow.active_user_login = gg.Item1;
                MainWindow.active_user_email = gg.Item2;

                MessageBox.Show($"Авторизация прошла успешно. Пользователь {gg.Item1}. Почта {gg.Item2}");

                if (gg.Item3 == "OK")
                {
                    MessageBox.Show("Письмо отправлено на почту");
                    NavigationService.Navigate(MainWindow.listPage[3]);
                }
                else
                {
                    MessageBox.Show("Письмо не отправлено на почту");
                    NavigationService.Navigate(MainWindow.listPage[4]);
                }
            }
            else
            {
                MainWindow.active_user_login = gg.Item1;
                MessageBox.Show($"Авторизация прошла успешно. Пользователь {gg.Item1}");
                NavigationService.Navigate(MainWindow.listPage[8]);
            }
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(MainWindow.listPage[0]);
        }

        public void MsgCallback(string msg)
        {
            throw new NotImplementedException();
        }
    }
}
