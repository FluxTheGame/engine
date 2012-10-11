using System;
using System.Diagnostics;
using System.Threading;
using SocketIOClient;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Text.RegularExpressions;
using SocketIOClient.Messages;

namespace Flux
{

	public class NodeClient
	{
		Client socket;

		public NodeClient()
		{
			Console.WriteLine("Connecting...");

			socket = new Client("http://127.0.0.1:8000/"); // url to the nodejs / socket.io instance
  

			// register for 'connect' event with io server
			socket.On("connect", (fn) =>
			{
				Trace.WriteLine("\r\nConnected.\r\n");
                //socket.Emit("gameapp", new { secret = "abc123" });
			});

            socket.On("node:move", (fn) =>
            {
                
            });


			// make the socket.io connection
			socket.Connect();
		}

        public void Send()
        {
            //socket.Emit("touch", new { x = 100, y= -38 });
        }


	}

}
