using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Client
{
    public class ClientScript
    {
        byte[] buffer = new byte[512];
        private Socket clientsocket;
        public bool isConnected = false;
        public void ConnectToServer()
        {
            try
            {
                clientsocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                clientsocket.BeginConnect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 80), new AsyncCallback(ConnectCallBack), null);
                isConnected = true;
                Console.WriteLine($"Connected to server:  {Environment.NewLine} ClientEndpoint: {clientsocket.LocalEndPoint} {Environment.NewLine} ServerEndpoint: {clientsocket.RemoteEndPoint}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e.GetType().Name} | {e.Message}");
            }
        }
        private void ConnectCallBack(IAsyncResult AR)
        {
            try
            {
                clientsocket.EndConnect(AR);
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e.GetType().Name} | {e.Message}");
            }
        }
        public void Send(string aMessage)
        {
            if(string.IsNullOrEmpty(aMessage))
            {
                Console.WriteLine("Enter a valid input!");
                return;
            }
            try
            {
                buffer = Encoding.UTF8.GetBytes(aMessage);
                clientsocket.BeginSend(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(SendCallback), null);
                Console.WriteLine($"Client Sending: {aMessage}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e.GetType().Name} | {e.Message}");
            }

        }

        private void SendCallback(IAsyncResult AR)
        {
            try
            {
                clientsocket.EndSend(AR);

            }
            catch (Exception e)
            {
                Console.WriteLine($"{e.GetType().Name} | {e.Message}");
            }
        }
    }
}
