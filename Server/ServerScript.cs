using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server
{
    public class ServerScript
    {
        private Socket serversocket, clientsocket;
        private byte[] buffer;
        public void StartServer()
        {
            try
            {
                serversocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                serversocket.Bind(new IPEndPoint(IPAddress.Any, 80));
                serversocket.Listen(0);
                serversocket.BeginAccept(new AsyncCallback(AcceptCallBack), null);
                Console.WriteLine("Connected to client");
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e.GetType().Name} | {e.Message}");
            }

        }
        private void AcceptCallBack(IAsyncResult AR)
        {
            try
            {
                clientsocket = serversocket.EndAccept(AR);
                buffer = new byte[clientsocket.ReceiveBufferSize];
                clientsocket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(RecieveCallBack), null);
                Console.WriteLine("Now recieving callbacks");
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e.GetType().Name} | {e.Message}");
            }
        }
        private void RecieveCallBack(IAsyncResult AR)
        {
            try
            {
                int recieved = clientsocket.EndReceive(AR);
                Array.Resize(ref buffer, recieved);
                string text = Encoding.ASCII.GetString(buffer, 0, recieved);
                Console.WriteLine($"Recieved message [{text}]");
                Array.Resize(ref buffer, clientsocket.ReceiveBufferSize);
                clientsocket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(RecieveCallBack), null);
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e.GetType().Name} | {e.Message}");
            }
        }
    }
}
