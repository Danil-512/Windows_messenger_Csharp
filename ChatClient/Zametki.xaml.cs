using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using ChatClient.ServiceChat;

namespace ChatClient
{
    public partial class Zametki : Page
    {
        ServiceChatClient client;

        public Zametki()
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

        // Событие кнопки добавления заметки
        private void zametkiButton_Click(object sender, RoutedEventArgs e)
        {
            string note = noteTB.Text;

            client.AddUserNote(note);

            listBox.Items.Clear();

            List<string> list = new List<string>();
            list = client.GetUserNotes().ToList();

            foreach (string note1 in list)
            {
                listBox.Items.Add(note1);
            }
        }

        // Событие загрузки страницы - вывод актуального списка заметок
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            activeUserTextBlock.Text = $"Аккаунт: _{client.get_active_login()}_";

            listBox.Items.Clear();

            List<string> list = new List<string>();
            list = client.GetUserNotes().ToList();

            foreach (string note in list)
            {
                listBox.Items.Add(note);
            }

        }

        // Событие нажатия кнопки очистки списка заметок
        private void dellButton_Click(object sender, RoutedEventArgs e)
        {
            client.DellUserNote();

            listBox.Items.Clear();

            List<string> list = new List<string>();
            list = client.GetUserNotes().ToList();

            foreach (string note1 in list)
            {
                listBox.Items.Add(note1);
            }
        }

        // Событие нажатия кнопки перехода к меню
        private void menuButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(MainWindow.listPage[4]);
        }
    }
}
