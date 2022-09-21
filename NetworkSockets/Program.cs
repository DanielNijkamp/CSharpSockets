using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Client
{
    public class Program
    {
        bool GettingIP = true;
        bool AppRunning = false;
        public static void Main(string[] args)
        {
            ClientScript client = new ClientScript();
            client.ConnectToServer();
            
            while (true)
            {
                if (client.isConnected)
                {
                    string input = Console.ReadLine();
                    if (input != null)
                    {
                        client.Send(input);
                    }
                }
            }
        }
    }
}