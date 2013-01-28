using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Collections.Concurrent;
using System.Net.Sockets;
using System.Threading;
using System.Net;
using System.Diagnostics;


namespace Flux
{
    class Server
    {
        private string[] gameEventNames = new string[] {"collector:burst"};
        private TcpClient tcpClient;

        public Server()
        {
            /*tcpClient = new TcpClient();
            try
            {
                // fetch IP from hostname
                //IPAddress[] ipList = Dns.GetHostAddresses("jahfer.no-ip.org");
                // connect TCP client to node.js server
                //tcpClient.Connect(ipList[0], 8100);

                tcpClient.Connect("172.17.153.60", 8100);
 
                // spin off listener to new thread
                Thread clientThread = new Thread(new ParameterizedThreadStart(HandleClientComm));
                clientThread.Start(tcpClient);

                Send("Hello from XNA!");
                SubscribeToGameEvents();
            }
            catch
            {
                Console.WriteLine("Could not connect to TCP server!");
            }*/
        }

        public void SendEventToServer(string eventName, OrderedDictionary o)
        {
            /*string toSend = eventName;

            foreach (KeyValuePair<string, string> entry in o)
            {
                toSend += "/" + entry.Key + "=" + entry.Value;
            }

            toSend += "$";
            Console.WriteLine(toSend);
             * */
            //Broken currently... Cast error :(
        }

        private void SubscribeToGameEvents()
        {
            foreach (string eventName in gameEventNames)
            {
                EventManager.On(eventName, (o) =>
                {
                    SendEventToServer(eventName, o);
                });
            }
        }

        private void Send(String str)
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
                HandleMessages(data);
                
            }

            tcpClient.Close();
        }


        private void HandleMessages(string data)
        {
            string[] messages = data.Trim().Split("$".ToCharArray());
            
            foreach (string message in messages) {

                if (message.Length <= 3) continue; //Short message - skip

                string[] items = message.Trim().Split("/".ToCharArray());
                string eventName = items[1].Split("=".ToCharArray())[1];
                
                //Create the event data object
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

                //Send the event
                if (o.Contains("id"))
                    EventManager.Emit(eventName, o);
                else 
                    Console.WriteLine("Missing ID in message: " + message);

            }
        }

    }
}
