using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Chat chat = new Chat();

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

