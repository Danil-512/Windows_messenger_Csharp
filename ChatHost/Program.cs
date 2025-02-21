using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

// Консольное приложение хост
namespace ChatHost
{
    internal class Program
    {
        // Хост для сервиса wcf_chat
        // Задача хоста - запуститься и висеть в процессах. Обрабатывать логику сервиса
        // 

        static void Main(string[] args)
        {
            using (var host = new ServiceHost(typeof(wcf_chat.ServiceChat)))
            {
                host.Open();
                Console.WriteLine("Хост стартовал");
                Console.ReadLine();
            }
        }
    }
}
