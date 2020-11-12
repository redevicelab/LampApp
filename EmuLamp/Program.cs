using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace EmuLamp
{
    class Program
    {
        static void Main()
        {
            //#region UDP Server
            //const string ip = "127.0.0.1";
            //const int port = 8889;

            //var endPoint = new IPEndPoint(IPAddress.Parse(ip), port);
            //var udpsocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            //udpsocket.Bind(endPoint);
            //while (true)
            //{
            //    var buffer = new byte[256];
            //    var size = 0;
            //    var data = new StringBuilder();
            //    EndPoint senderPoint = new IPEndPoint(IPAddress.Any, 0);
            //    do
            //    {
            //        size = udpsocket.ReceiveFrom(buffer, ref senderPoint);
            //        data.Append(Encoding.UTF8.GetString(buffer));
            //    } while (udpsocket.Available > 0);

            //    Console.WriteLine(data);
            //    udpsocket.SendTo(Encoding.UTF8.GetBytes("Recevied"), senderPoint);
            //}
            //#endregion
           
            #region UDP Client
            const string ip = "127.0.0.1";
            const int port = 8889; //порт клиента

            var endPoint = new IPEndPoint(IPAddress.Parse(ip), port);
            var udpsocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            udpsocket.Bind(endPoint);

            while (true)
            {
                var buffer = new byte[256];
                var size = 0;
                var data = new StringBuilder();

                Console.Write("Ввод: ");
                var mes = Console.ReadLine();
                var serverEndPoint = new IPEndPoint(IPAddress.Parse(ip),8888); // Порт и адресс сервера
                udpsocket.SendTo(Encoding.UTF8.GetBytes(mes), serverEndPoint);

                while (udpsocket.Available > 0)
                {
                    EndPoint senderPoint = new IPEndPoint(IPAddress.Any, 0);
                    size = udpsocket.ReceiveFrom(buffer, ref senderPoint);
                    data.Append(Encoding.UTF8.GetString(buffer));
                }

                Console.WriteLine(data);
            }
            #endregion


        }

        private static void ReceiveMessage(int port)
        {
            UdpClient receiver = new UdpClient(port); // UdpClient для получения данных
            IPEndPoint remoteIp = null; // адрес входящего подключения
            try
            {
                while (true)
                {
                    byte[] data = receiver.Receive(ref remoteIp); // получаем данные
                    string message = Encoding.ASCII.GetString(data);
                    Console.WriteLine("Собеседник: {0}", message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                receiver.Close();
            }
        }

    }
}
