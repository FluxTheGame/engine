using System;
using System.Diagnostics;
using System.Threading;
using SocketIOClient;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Text.RegularExpressions;
using SocketIOClient.Messages;

namespace xna
{

	public class TestSocketIOClient
	{
		Client socket;
		public void Execute()
		{
			Console.WriteLine("Connecting...");

			socket = new Client("http://127.0.0.1:8000/"); // url to the nodejs / socket.io instance
  

			// register for 'connect' event with io server
			socket.On("connect", (fn) =>
			{
				Console.WriteLine("\r\nConnected.\r\n");
                socket.Emit("gameapp", new { secret = "abc123" });
                
			});

			// register for 'hi' events - message is a json 'Part' object
			socket.On("hi", (data) =>
			{
				Console.WriteLine("got hi");

                Payload p = data.Json.GetFirstArgAs<Payload>();
                Console.WriteLine("SomeDataOne:   {0}\r\n", p.SomeDataOne);
			});

			// make the socket.io connection
			socket.Connect();
		}

        public void Send()
        {
            socket.Emit("touch", new { x = 100, y= -38 });
        }


	}

}
