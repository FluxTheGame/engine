using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using System.Collections.Concurrent;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Net;
using System.Diagnostics;

namespace SocketsThrowaway
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class Server
    {
        private TcpClient tcpClient;

        public Server()
        {
            tcpClient = new TcpClient();
            tcpClient.Connect("127.0.0.1", 8000);

            Thread clientThread = new Thread(new ParameterizedThreadStart(HandleClientComm));
            clientThread.Start(tcpClient);

            this.send("Connected!\n");


            /*TimeSpan maxDuration = TimeSpan.FromMinutes(10);
            Stopwatch sw = Stopwatch.StartNew();
            bool DoneWithWork = false;

            while (sw.Elapsed < maxDuration && !DoneWithWork)
            {
                this.send("XNA Heartbeat");
                Thread.Sleep(100);
            }*/
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

                Console.WriteLine(Encoding.ASCII.GetString(message, 0, bytesRead));
            }

            tcpClient.Close();
        }
    }
}
