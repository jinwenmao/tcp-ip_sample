using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace TcpClientDemo
{
    class Program
    {
        static void Connect(String server, String message)
        {
            try
            {
                Int32 port = 13000;
                TcpClient client = new TcpClient(server, port);

                /* Translate the passed message into ASCII and store it as a Byte array. */
                Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);
                NetworkStream stream = client.GetStream();                
                stream.Write(data, 0, data.Length);/* Send the message to the connected TcpServer.  */

                Console.WriteLine("Sent: {0}", message);
                data = new Byte[256];

                /* String to store the response ASCII representation. */
                String responseData = String.Empty;

                Int32 bytes = stream.Read(data, 0, data.Length); /* Read the first batch of the TcpServer response bytes. */
                responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                Console.WriteLine("Received from Server: {0}", responseData);

                stream.Close();/* Close everything. */
                client.Close();
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("ArgumentNullException: {0}", e);
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            Console.WriteLine("\n Press Enter to continue...");
            Console.Read();
        }

        static void Main(string[] args)
        {
            while (true)
            {
                //Connect("192.168.0.1", "This is send from client");   //地址为服务器地址  127.0.0.1
                Connect("127.0.0.1", "This is send from client");
            }
        }
    }
}
