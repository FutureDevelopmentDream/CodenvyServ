using System;

namespace ClientO1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");


            Console.WriteLine("Iniciando...");

            //Conn Sock = new Conn();
            Declas.Conection = new Conn();
            Declas.Conection.Config_Server();

            Console.WriteLine("Press Enter from initialize server.");
            Console.ReadLine();
            Console.WriteLine("Init...");

            Declas.Conection.Connect();

            Console.WriteLine("Press Enter from Disconnect.");
            Console.ReadLine();

            Declas.Conection.Disconnect();

            Console.WriteLine("Close read.");
            Console.WriteLine("Press Enter from exit.");
            Console.ReadLine();
        }
    }
}