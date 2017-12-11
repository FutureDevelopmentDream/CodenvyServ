using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Net.Sockets;

namespace ClientO1
{
    public class HandleData
    {
        public static char Sep = ';';
        public static void Handle_Archive(String Data, Byte[] Bytes)
        {

        }
        public static void Handle_PAK(String Data)
        {
            switch (Data)
            {
                /*case "ULOGGIN":
                    Visible_F();
                    MessageBoxEx.Show("Te has desconectado");
                    break;
                case "NOPAS":
                    Visible_F();
                    MessageBoxEx.Show("Contraseña incorrecta...");
                    break;
                case "NOMSN":
                    Visible_F();
                    MessageBoxEx.Show("Cuenta incorrecta o inexistente...");
                    break;
                case "NOACC":
                    Visible_F();
                    MessageBoxEx.Show("Error al iniciar sesión...");
                    break;*/
            }
        }
        public static void Handle_Texto(String Data)
        {
            String Datos = ReadField(1, Data, Sep);

            switch (Datos)
            {
                case "MSG":
                    Console.WriteLine(Data);
                    break;
            }
        }
        public static void Handle_Data(String data, Byte[] Bytes)
        {
            int Count_D = 0;

            if (data.IndexOf(Sep) == -1)
            {
                Count_D = -1;
            }
            else
                Count_D = ReadField(1, data, Sep).Length;
            //MessageBoxEx.Show(ReadField(1, data, Sep));
            switch (Count_D)
            {
                case -1:
                    Handle_PAK(data);
                    break;
                case 4:
                    Handle_Archive(data, Bytes);
                    break;
                case 5:
                    Handle_Texto(data);
                    break;
                default:
                    Handle_Texto(data);
                    break;
            }
        }
        public static String ReadEnd(String Paquete, int Inicio)
        {
            try
            {
                return Paquete.Substring(Inicio);
            }
            catch
            {
                return String.Empty;
            }
        }
        public static String ReadField(int Pos, String data, char SepASCII)
        {
            int pos = Pos - 1;
            try
            {
                return data.Split(SepASCII)[pos];
            }
            catch
            {
                //clsFer.Fer_C.Add_Consola("Error ReadField... [" + data + "," + pos + "]");
                return String.Empty;
            }

        }
    }


public class Conn
    {
        public Boolean Logeado = false;
        public Socket Sock_ = null;

        public const short AF_INET = 2;
        public const short AF_INET6 = 23;
        public String response = String.Empty;
        public Encoding Enco = Encoding.UTF32;
        private string ip = "127.0.0.1", serverPort = "7666";
        private Thread Th_Rec;
        public void Config_Server()
        {
            Sock_ = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);


            Th_Rec = new Thread(new ThreadStart(Th_Receiv));
            //Th_Rec.SetApartmentState(ApartmentState.STA);
            Th_Rec.IsBackground = true;
        }
        public void Add_Consola(String texto)
        {
            Console.WriteLine(texto + "\n");
        }
        private void Th_Receiv()
        {
            Byte[] bytes = new Byte[1024 * 2];
            while (this.Sock_.Connected && Logeado)
            {
                try
                {
                    bytes = new Byte[1024 * 2];
                    int bytesRec = Sock_.Receive(bytes, SocketFlags.None);
                    String Resp = Enco.GetString(bytes, 0, bytesRec);
                    //MessageBoxEx.Show(Resp);
                    if (bytesRec > 0)
                        HandleData.Handle_Data(Resp, bytes);
                }
                catch
                {

                }
            }
        }

        public void Connect()
        {

            if (!State_Connection())
            {
                try
                {
                    Sock_.Connect(ip, Convert.ToInt32(serverPort));
                    Logeado = true;
                    Th_Rec.Start();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else
            {
                Finality_Connection();
                try
                {
                    Sock_.Connect(ip, Convert.ToInt32(serverPort));
                    Th_Rec.Start();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
        public void Finality_Connection()
        {
            try
            {
                //Sock_.Disconnect(true);
                //Sock_.Close();
                Sock_.Shutdown(SocketShutdown.Both);
                
            }
            catch
            {

            }
            try
            {
                Th_Rec.Join();
            }
            catch
            {

            }
        }

        internal void Disconnect()
        {
            //throw new NotImplementedException();
            if (State_Connection())
            {
                //Sock_.Disconnect(true);
                Sock_.Shutdown(SocketShutdown.Both);
            }

        }

        public Boolean State_Connection()
        {
            try
            {
                return Sock_.Connected;
            }
            catch
            {
                return false;
            }
        }
    }


}
