// WebSocketClient.cs
using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        using var client = new ClientWebSocket();
        await client.ConnectAsync(new Uri("ws://localhost:5000/ws/"), CancellationToken.None);
        Console.WriteLine("Connected to server.");

        while (true)
        {
            Console.Write("Enter message: ");
            string? input = Console.ReadLine();

            if (string.IsNullOrEmpty(input)) continue;
            if (input.Equals("exit", StringComparison.OrdinalIgnoreCase)) break;

            // Send
            var bytes = Encoding.UTF8.GetBytes(input);
            await client.SendAsync(new ArraySegment<byte>(bytes), WebSocketMessageType.Text, true, CancellationToken.None);

            // Receive
            var buffer = new byte[1024];
            var result = await client.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            Console.WriteLine("Server: " + Encoding.UTF8.GetString(buffer, 0, result.Count));
        }

        await client.CloseAsync(WebSocketCloseStatus.NormalClosure, "Client closing", CancellationToken.None);
    }
}
