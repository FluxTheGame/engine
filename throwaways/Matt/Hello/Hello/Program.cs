using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hello
{
	class Program
	{
		static void Main(string[] args)
		{

			TestSocketIOClient tClient = new TestSocketIOClient();
			tClient.Execute();

            while (true) { }
		}
	}
}
