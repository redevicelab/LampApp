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
            Console.WriteLine("Start");
            ReceiveMessage();
            Console.ReadLine();


           async Task ReceiveMessage()
            {
                using (var udpClient = new UdpClient(8888))
                {
                    while (true)
                    {
                        var receivedResult = await udpClient.ReceiveAsync();
                        Console.Write(Encoding.ASCII.GetString(receivedResult.Buffer));
                    }
                }
            }
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
