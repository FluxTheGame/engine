using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Concurrent;
using System.Collections.Specialized;
using System.Net.Sockets;
using System.Threading;
using System.Net;
using System.Diagnostics;


namespace Flux
{
    class Server
    {
        private TcpClient tcpClient;

        public Server()
        {
            tcpClient = new TcpClient();
            try
            {
                tcpClient.Connect("127.0.0.1", 8100);

                Thread clientThread = new Thread(new ParameterizedThreadStart(HandleClientComm));
                clientThread.Start(tcpClient);

                this.send("Hello from XNA!");
            }
            catch
            {
                Console.WriteLine("Could not connect to TCP server!");
            }
        }


        public void send(String str)
        {
            NetworkStream clientStream = tcpClient.GetStream();
            byte[] data = Encoding.ASCII.GetBytes(str);
            clientStream.Write(data, 0, data.Length);
            clientStream.Flush();
        }


        private void HandleClientComm(object client)
        {
            TcpClient tcpClient = (TcpClient)client;
            NetworkStream clientStream = tcpClient.GetStream();

            byte[] message = new byte[4096];
            int bytesRead;

            while (true)
            {
                bytesRead = 0;

                try
                {
                    bytesRead = clientStream.Read(message, 0, 4096);
                }
                catch
                {
                    break;
                }

                if (bytesRead == 0)
                {
                    break;
                }

                string data = Encoding.ASCII.GetString(message, 0, bytesRead);
                HandleMessage(data);
                
            }

            tcpClient.Close();
        }


        private void HandleMessage(string data)
        {
            string[] items = data.Split("/".ToCharArray());
            string eventName = items[0];
            OrderedDictionary o = new OrderedDictionary();

            foreach (string item in items)
            {
                if (item.Contains("="))
                {
                    string[] keyValue = item.Split("=".ToCharArray());

                    try
                    {
                        int number = Convert.ToInt32(keyValue[1]);
                        o.Add(keyValue[0], number);
                    }
                    catch
                    {
                        o.Add(keyValue[0], keyValue[1]);
                    }
                }
            }
            
            EventManager.Emit(eventName, o);
        }

    }
}
