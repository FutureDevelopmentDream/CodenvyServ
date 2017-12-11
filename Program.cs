using System;

namespace ClientO1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");


            Console.WriteLine("Iniciando...");

            Conn Sock = new Conn();

            Sock.Config_Server();

            Console.WriteLine("Press Enter from initialize server.");
            Console.ReadLine();
            Console.WriteLine("Init...");

            Sock.Connect();

            Console.WriteLine("Press Enter from Disconnect.");
            Console.ReadLine();

            Sock.Disconnect();

            Console.WriteLine("Close read.");
            Console.WriteLine("Press Enter from exit.");
            Console.ReadLine();
        }
    }
}