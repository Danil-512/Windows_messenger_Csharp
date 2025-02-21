using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace ChatClient
{
    public partial class Main : Page
    {
        public Main()
        {
            InitializeComponent();
        }

        // Событие нажатия кнопкаи для перехода на страницу авторизации
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(MainWindow.listPage[1]);
        }

        // Событие нажатия кнопки для перехода на страницу регистрации
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(MainWindow.listPage[2]);
        }
    }
}
