using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Collections;
using System.Collections.Concurrent;
using System.Net.Sockets;
using System.Threading;
using System.Net;
using System.Diagnostics;


namespace Flux
{
    class Server
    {
        private string[] gameEventNames = new string[] {"collector:burst", "collector:heartbeat", "collector:merge"};
        private TcpClient tcpClient;

        private const int TIMEOUT = 1; // seconds

        public Server()
        {
            tcpClient = new TcpClient();
            try
            {
                IAsyncResult ar = tcpClient.BeginConnect("localhost", 8100, null, null);
                System.Threading.WaitHandle wh = ar.AsyncWaitHandle;
                try
                {
                    if (!ar.AsyncWaitHandle.WaitOne(TimeSpan.FromSeconds(TIMEOUT), false))
                    {
                        tcpClient.Close();
                        throw new TimeoutException();
                    }

                    tcpClient.EndConnect(ar);
                }
                finally
                {
                    wh.Close();
                }


                // spin off listener to new thread
                Thread clientThread = new Thread(new ParameterizedThreadStart(HandleClientComm));
                clientThread.Start(tcpClient);

                Send("Hello from XNA!");
                SubscribeToGameEvents();
            }
            catch
            {
                Console.WriteLine("Could not connect to TCP server!");
            }

        }

        public void SendEventToServer(string eventName, OrderedDictionary o)
        {
            string toSend = "/name=" + eventName;

            foreach (DictionaryEntry entry in o)
            {
                toSend += "/" + entry.Key + "=" + entry.Value;
            }

            toSend += "$";

            Send(toSend);
        }

        private void SubscribeToGameEvents()
        {
            foreach (string eventName in gameEventNames)
            {
                SubscribeToGameEvent(eventName);
            }
        }

        private void SubscribeToGameEvent(string eventName)
        {
            EventManager.On(eventName, (o) =>
            {
                SendEventToServer(eventName, o);
            });
        }

        private void Send(String str)
        {
            //Console.WriteLine("Sending: " + str);
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

                //Console.WriteLine(message);

                string[] items = message.Trim().Split("/".ToCharArray());
                string eventName = items[1].Split("=".ToCharArray())[1];

                bool idIsOkay = false;
                
                //Create the event data object
                OrderedDictionary o = new OrderedDictionary();

                foreach (string item in items)
                {
                    if (item.Contains("="))
                    {
                        string[] keyValue = item.Split("=".ToCharArray());

                        try
                        {
                            if (keyValue[0] == "username")
                            {
                                throw new Exception("Username must be a string!");
                            }

                            int number = Convert.ToInt32(keyValue[1]);

                            if (!idIsOkay && keyValue[0] == "id" && number < 0)
                            {
                                Console.WriteLine("Invalid User ID: " + number);
                                return;
                            }

                            idIsOkay = true;

                            o.Add(keyValue[0], number);
                        }
                        catch
                        {
                            if (keyValue[0] == "id")
                            {
                                Console.WriteLine("Invalid User ID: " + keyValue[1]);
                                return;
                            }

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
