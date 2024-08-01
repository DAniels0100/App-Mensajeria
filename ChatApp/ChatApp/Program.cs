using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test
{
    class Program
    {
        static void Main(string[] args)
        {
            Chat chat = new Chat();

            Console.Write("Do you want to host a chat? (y/n): ");
            string choice = Console.ReadLine();

            if (choice.ToLower() == "y")
            {
                chat.StartListening(8000);
            }
            else
            {
                chat.Connect("127.0.0.1", 8000);
            }

            while (true)
            {
                string message = Console.ReadLine();
                chat.SendMessage(message);
            }
        }
    }
}

