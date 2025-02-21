using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using ChatClient.ServiceChat;


namespace ChatClient
{
    public partial class Contacts : Page
    {
        ServiceChatClient client;

        public Contacts()
        {
            InitializeComponent();
            client = MainWindow.client;
        }
        // Список контактов
        private List<string> contList;
        // Черный список
        private List<string> contBlackList;

        // Событие нажатия кнопки добавления контакта в черный список
        private void newBlackButton_Click(object sender, RoutedEventArgs e)
        {
            contBlackList = new List<string>();
            contBlackList = new List<string>();

            MessageBox.Show(client.NewBlackCont(comboBoxCont.SelectedValue.ToString()));

            listBlack.Items.Clear();

            contBlackList = client.GetContList().Item2.ToList();

            for (int i = 0; i < contBlackList.Count; i++)
            {
                listBlack.Items.Add(contBlackList[i]);
            }
        }

        // Событие нажатия кнопки добавления контакта 
        private void newContactButton_Click(object sender, RoutedEventArgs e)
        {
            contList = new List<string>();

            MessageBox.Show(client.NewCont(newContBox.Text));

            listContacts.Items.Clear();
            comboBoxCont.Items.Clear();

            contList = client.GetContList().Item1.ToList();

            for (int i = 0; i < contList.Count; i++)
            {
                listContacts.Items.Add(contList[i]);
                comboBoxCont.Items.Add(contList[i]);
            }
        }

        // Функция обновления чата
        public void updateChat()
        {    
            // Проверка на то, не пуст ли выбор контакта
            if (comboBoxCont.SelectedValue != null )
            {
                // Проверка на то, находится ли контакт в черном списке
                int j = 0;
                for (int i = 0; i < contBlackList.Count; i++)
                {
                    if (comboBoxCont.SelectedValue.ToString() == contBlackList[i]) j = 1;
                }

                if (j == 0)
                {
                    try
                    {
                        List<string> messList = new List<string>();
                        messListBox.ItemsSource = messList;

                        string comb = comboBoxCont.SelectedValue.ToString();

                        int k1 = 0;

                        contBlackList.Clear();
                        contBlackList = client.GetContList().Item2.ToList();

                        for (int i = 0; i < contBlackList.Count; i++)
                        {
                            if (contBlackList[i] == comb)
                            {
                                k1 = 1;
                            }
                        }

                        if (k1 == 1)
                        {
                            MessageBox.Show("Контакт в черном списке. Вы не можете открыть этот чат");
                            return;
                        }
                        else
                        {
                            messList.Clear();
                            messList = client.GetChatMesList(comb).ToList();
                        }
                        messListBox.ItemsSource = messList;
                    }
                    catch { }
                }
            }
        }

        // Событие нажатия кнопки открытия чата
        private void openChatButton_Click(object sender, RoutedEventArgs e)
        {
            updateChat();
        }

        // Событие загрузки страницы - вывод актуальных данных
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            activeUserTextBlock.Text = $"Аккаунт: _{client.get_active_login()}_";

            listContacts.Items.Clear();
            listBlack.Items.Clear();
            comboBoxCont.Items.Clear();

            contList = new List<string>();
            contBlackList = new List<string>();

            contList = client.GetContList().Item1.ToList();
            contBlackList = client.GetContList().Item2.ToList();

            for (int i = 0; i < contBlackList.Count; i++)
            {
                listBlack.Items.Add(contBlackList[i]);
            }
            for (int i = 0; i < contList.Count; i++)
            {
                listContacts.Items.Add(contList[i]);
                comboBoxCont.Items.Add(contList[i]);
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

        // Событие нажатяи кнопки перехода в меню
        private void menuButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(MainWindow.listPage[4]);
        }

        // Событие нажатия кнопки удаление контакта из черного списка
        private void DelBlackButton_Click(object sender, RoutedEventArgs e)
        {
            string comb = comboBoxCont.SelectedValue.ToString();

            MessageBox.Show(client.DelBlackCont(comb));

            listBlack.Items.Clear();

            contBlackList = client.GetContList().Item2.ToList();

            for (int i = 0; i < contBlackList.Count; i++)
            {
                listBlack.Items.Add(contBlackList[i]);
            }
        }

        // Событие нажатия кнопки удаления аккаунта
        private void DelButton_Click(object sender, RoutedEventArgs e)
        {
            string comb = comboBoxCont.SelectedValue.ToString();

            MessageBox.Show(client.DelCont(comb));

            listContacts.Items.Clear();
            comboBoxCont.Items.Clear();

            contList = client.GetContList().Item1.ToList();

            for (int i = 0; i < contList.Count; i++)
            {
                listContacts.Items.Add(contList[i]);
                comboBoxCont.Items.Add(contList[i]);
            }
        }

        // Событие нажатия кнопки отправки сообщения
        private void newMessChatButton_Click(object sender, RoutedEventArgs e)
        {
            string newMessage = $" {newMessageBlock.Text}";

            string comb1 = comboBoxCont.SelectedValue.ToString();

            string status = client.NewMessChat(comb1, newMessage);

            List<string> listStr = new List<string>();

            messListBox.ItemsSource = listStr;

            string comb = comboBoxCont.SelectedValue.ToString();

            int k1 = 0;

            contBlackList.Clear();
            contBlackList = client.GetContList().Item2.ToList();

            for (int i = 0; i < contBlackList.Count; i++)
            {
                if (contBlackList[i] == comb)
                {
                    k1 = 1;
                }
            }

            if (k1 == 1)
            {
                MessageBox.Show("Контакт в черном списке. Вы не можете открыть этот чат");
            }
            else
            {
                List<string> messList = new List<string>();
                messList = client.GetChatMesList(comb).ToList();

                messListBox.ItemsSource = messList;
            }
        }

        // Событие движения мыши - для обновления данных в списке сообщений чата
        private void Grid_MouseMove(object sender, MouseEventArgs e)
        {
            listContacts.Items.Clear();

            updateChat();

            contList = new List<string>();

            contList = client.GetContList().Item1.ToList();
            
            for (int i = 0; i < contList.Count; i++)
            {
                listContacts.Items.Add(contList[i]);
            }
        }
    }
}
