using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using ChatClient.ServiceChat;


namespace ChatClient
{
    /// <summary>
    /// Логика взаимодействия для Two_factor.xaml
    /// </summary>
    public partial class Two_factor : Page
    {
        // Объект типа хоста в клиенте, для взаимодействия с его методами
        ServiceChatClient client;
        public Two_factor()
        {
            InitializeComponent();
            client = MainWindow.client;
        }

        // Событие нажатия кнопки проверки кода двухфакторной аутентификации
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            string cd = textBox1.Text;
            string status = client.TwoFact(cd);

            if (status == "OK")
            {
                MessageBox.Show("Проверка пройдена");
                NavigationService.Navigate(MainWindow.listPage[8]);
            }
            else
            {
                MessageBox.Show("Проверка не пройдена");
            }
        }

        // Событие нажатия кнопки выхода из аккаунта
        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            // Переход к Main
            NavigationService.Navigate(MainWindow.listPage[0]);

            MainWindow.active_user_email = null;
            MainWindow.active_user_login = null;
        }
    }
}
