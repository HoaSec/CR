
using System;
using System.Diagnostics;
using System.Management.Automation;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        int port = 17040;
        TcpListener listener = new TcpListener(IPAddress.Parse("127.0.0.1"), port);

        listener.Start();
        Console.WriteLine($"Listening on port {port}...");

        while (true)
        {
            TcpClient client = await listener.AcceptTcpClientAsync();
            _ = HandleClientAsync(client);
        }
    }

    //$tcpConnection = New-Object System.Net.Sockets.TcpClient("127.0.0.1", 17040);$tcpStream = $tcpConnection.GetStream();$reader = New-Object System.IO.StreamReader($tcpStream);$writer = New-Object System.IO.StreamWriter($tcpStream);$writer.AutoFlush = $true;$buffer = new-object System.Byte[] 1024;$encoding = new-object System.Text.AsciiEncoding; while ($tcpConnection.Connected){while ($tcpStream.DataAvailable){$rawresponse = $reader.Read($buffer, 0, 1024);$response = $encoding.GetString($buffer, 0, $rawresponse)}if ($tcpConnection.Connected){Write-Host -NoNewline "prompt> ";$command = Read-Host; if ($command -eq "escape"){break;}$writer.WriteLine($command) | Out-Null;}start-sleep -Milliseconds 500;}$reader.Close();$writer.Close();$tcpConnection.Close()

    static async Task HandleClientAsync(TcpClient client)
    {
        Console.WriteLine($"Client connected: {client.Client.RemoteEndPoint}");

        using (client)
        using (NetworkStream stream = client.GetStream())
        {
            byte[] buffer = new byte[4096];

            int bytesRead;
            while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length)) > 0)
            {

                string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                Console.WriteLine(message);

                var scriptArguments = "-Command \"" + message + "\"";
                var processStartInfo = new ProcessStartInfo("powershell.exe", scriptArguments);
                processStartInfo.RedirectStandardOutput = true;
                processStartInfo.RedirectStandardError = true;

                using var process = new Process();
                process.StartInfo = processStartInfo;
                process.Start();
                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();
                byte[] dataToSend = Encoding.UTF8.GetBytes(output);
                //await stream.WriteAsync(Encoding.UTF8.GetBytes(output), 0, dataToSend.Length);
                //Console.WriteLine(output);
                Console.WriteLine(error);

            }
        }

        Console.WriteLine("Client disconnected.");
    }
}