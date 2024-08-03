using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.Write("ingresa puerto:");
            if (int.TryParse(Console.ReadLine(), out int port))
            {
                Chat chat = new Chat(port);
                await chat.StartAsync();
            }
            else
            {
                Console.WriteLine("invalid port");
            }

        }
    }
}

