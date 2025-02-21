using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using ChatClient.ServiceChat;



namespace ChatClient
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, ServiceChat.IServiceChatCallback
    {
        // Создание списка страниц
        public static List<Page> listPage; 

        // Создание переменных для хранения актуальных логина и почты
        public static string active_user_login = null;
        public static string active_user_email = null;

        // Создание объекта клиента
        public static ServiceChatClient client;
        
        public MainWindow()
        {
            InitializeComponent();
            client = new ServiceChatClient(new System.ServiceModel.InstanceContext(this));
            listPage = new List<Page>();

            // Добавление главной страницы
            listPage.Add(new Main());
            // Добавление страницы авторизации
            listPage.Add(new Login());
            // Добавление страницы регистрации
            listPage.Add(new Register());
            // Добавление страницы двухфакторной аутентификации при авторизации
            listPage.Add(new Two_factor());
            // Добавление страницы меню
            listPage.Add(new Menu());
            // Добавление страницы двухфакторной аутентификации при регистрации
            listPage.Add(new Two_factor_reg());
            // Добавление страницы с заметками
            listPage.Add(new Zametki());
            // Добавление страницы с контактами и чатами
            listPage.Add(new Contacts());
            // Добавление страницы с профилем
            listPage.Add(new Personal());

            frame2.NavigationUIVisibility = NavigationUIVisibility.Hidden;
            frame2.Content = listPage[0];
        }
        
        public void MsgCallback(string msg)
        {
            throw new NotImplementedException();
        }
    }
}
