using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.IO;

var hostName = Dns.GetHostName();

Console.WriteLine(hostName);

var hostEntry = Dns.GetHostEntry(hostName);
foreach(var ip in hostEntry.AddressList)
    Console.WriteLine(ip);

IPAddress ipAddress = hostEntry.AddressList[0];
IPEndPoint ipEndPoint = new(ipAddress, 8080);

using Socket listener = new(
        ipEndPoint.AddressFamily,
        SocketType.Stream,
        ProtocolType.Tcp);

listener.Bind(ipEndPoint);
listener.Listen(5);

var handler = await listener.AcceptAsync();

while (true)
{
    var input = Console.ReadLine();
    var buffer = new byte[1024];
    var receivedBytes = await handler.ReceiveAsync(buffer, SocketFlags.None);
    var message = Encoding.UTF8.GetString(buffer, 0, receivedBytes);

    const string eom = "<|EOM|>";
    const string ack = "<|ACK|>";

    if(string.IsNullOrWhiteSpace(input) is false)
    {
        handler.SendAsync(Encoding.UTF8.GetBytes(input),0);
    }

    var eomWasSend = message.IndexOf(eom) > -1;
    if (eomWasSend)
    {
        Console.Write($"Server received message: {message}");
        
        var echoBytes = Encoding.UTF8.GetBytes(ack);
        await handler.SendAsync(echoBytes, 0);
        Console.WriteLine("Server send acknowledge messge\n");

    }
}
