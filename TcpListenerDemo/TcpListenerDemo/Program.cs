using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace TcpListenerDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpListener server = null;
            try
            {
                Int32 port = 13000;                /* Set the TcpListener (Server) on port 13000. */
                server = new TcpListener(port);
                server.Start(); /* Start listening for client requests. */
                Byte[] bytes = new Byte[256];/* Buffer for reading data */
                String data = null;
                while (true)
                {
                    Console.Write("I'm Server,Waiting for a connection... ");
                    TcpClient client = server.AcceptTcpClient();
                    Console.WriteLine("Connected!");
                    data = null;
                    NetworkStream stream = client.GetStream(); /* 获取一个stream对象来做reading和writing操作 */
                    int i;
                    while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)/* Loop to receive all the data sent by the client. */
                    {
                        data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);/* Translate data bytes to a ASCII string. */
                        Console.WriteLine("Received From Client: {0}\n", data);
                        data = data.ToUpper();/* Process the data sent by the client. */
                        byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);
                        /* Send back a response. */
                        stream.Write(msg, 0, msg.Length);
                        Console.WriteLine("Server Sent Back: {0} Server\n", data);
                    }

                    /* Shutdown and end connection */
                    client.Close();
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            finally
            {
                /*  Stop listening for new clients. */
                server.Stop();
            }

            Console.WriteLine("\nHit enter to continue...");
            Console.Read();
        }
    }
}
