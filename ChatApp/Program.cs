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

            //Console.Write("Do you want to host a chat? (y/n): ");
            //string choice = Console.ReadLine();

            try
            {
                chat.Connect("127.0.0.1", 8000);
            }
            catch (Exception ex)
            {

                chat.StartListening(8000);
                Console.WriteLine("you are the host");
            }

            while (true)
            {
                string message = Console.ReadLine();
                chat.SendMessage(message);
            }
        }
    }
}

