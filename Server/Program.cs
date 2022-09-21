using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Server;
using System.Collections.Generic;

namespace Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var server = new ServerScript();
            server.StartServer();
           while (true)
            {
                Console.Read();
            }
        }
        
    }
}