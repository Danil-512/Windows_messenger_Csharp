using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using ChatClient.ServiceChat;


namespace ChatClient
{    
    public partial class Menu : Page
    {
        ServiceChatClient client;

        public Menu()
        {
            InitializeComponent();
            client = MainWindow.client;   
        }

        // Событие нажатия кнопки выхода из аккаунта
        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            // Переход к Main
            NavigationService.Navigate(MainWindow.listPage[0]);

            MainWindow.active_user_email = null;
            MainWindow.active_user_login = null;
        }

        // Событие нажатия кнопки перехода к странице с заметками
        private void zametkiButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(MainWindow.listPage[6]);

        }

        // Событие загрузки страницы - обновление подписи логина активного пользователя
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                activeUserTextBlock.Text = $"Аккаунт: _{client.get_active_login()}_";
            }
            catch
            {
                MessageBox.Show("??");
            }
        }

        // Событие нажатия кнопки перехода к странице с контактами
        private void contactsButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(MainWindow.listPage[7]);
        }

        // Событие нажатия кнопки перехода к странице с профилем
        private void personalButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(MainWindow.listPage[8]);
        }
    }
}
