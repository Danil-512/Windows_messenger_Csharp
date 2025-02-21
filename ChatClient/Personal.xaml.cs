using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using ChatClient.ServiceChat;

namespace ChatClient
{
    public partial class Personal : Page
    {
        ServiceChatClient client;

        public Personal()
        {
            InitializeComponent();
            client = MainWindow.client;
        }

        // Событие нажатия на кнопку перехода в мню
        private void menuButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(MainWindow.listPage[4]);
        }

        // Событие нажатия на кнопку выхода из аккаунта
        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(MainWindow.listPage[0]);

            MainWindow.active_user_email = null;
            MainWindow.active_user_login = null;
        }

        // Событие загрузки страницы - загрузка акьуальных данных о пользователе
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            activeUserTextBlock.Text = $"Аккаунт: _{client.get_active_login()}_";

            (string, string, string) userData = client.GetUserData();
            activeUserNameTextBlock.Text = "Имя: " + userData.Item1;
            activeUserLastNameTextBlock.Text = "Фамилия: " + userData.Item2;
            activeUserStatusNameTextBlock.Text = "Статус: " + userData.Item3;
        }

        // Событие нажатия кнопки обновления данных пользователя
        private void newDataButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string name = nameTB.Text;
                string last_name = last_nameTB.Text;
                string status = statusTB.Text;

                MessageBox.Show(client.NewUserData(name, last_name, status));

                (string, string, string) userData = client.GetUserData();
                activeUserNameTextBlock.Text = "Имя: " + userData.Item1;
                activeUserLastNameTextBlock.Text = "Фамилия: " + userData.Item2;
                activeUserStatusNameTextBlock.Text = "Статус: " + userData.Item3;
            }
            catch
            {
                MessageBox.Show("Ошибка обновления данных");
            }
        }
    }
}
