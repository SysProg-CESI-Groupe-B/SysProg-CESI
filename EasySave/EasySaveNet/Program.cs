﻿using System.Text;
using System.Runtime.InteropServices;

using System.Net;
using System.Net.Sockets;

namespace EasySaveNet;

internal static class Program
{
    private static readonly bool IsWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
    private const string ServerPort = "9000";
    
    /// <summary>
    /// Main entry point of the program.
    /// </summary>
    /// <param name="args"></param>
    private static void Main(string[] args)
    {
        Console.InputEncoding = Console.OutputEncoding = Encoding.UTF8;
        
        if (args.Length != 1)
        {
            Console.WriteLine("Error: Wrong arguments");
            Console.WriteLine("Usage: EasySaveNet <server IP address> <server port>");
            
            Environment.Exit(IsWindows ? 160 : 126);
        }
        
        // Parse command line arguments to get server IP address and port
        var serverIpAddress = IPAddress.Parse(args[0]);
        var serverPort = int.Parse(ServerPort);

        try
        {
            var serverSocket = EstablishConnection(serverIpAddress, serverPort);
            NetworkListener(serverSocket); 
            
            TerminateConnection(serverSocket);
        }
        catch (SocketException e)
        {
            Console.WriteLine("Error: {0}", e.Message);
            Environment.Exit(IsWindows ? 59 : 1);
        }
    }
    
    /// <summary>
    /// Establish a connection with the server.
    /// </summary>
    /// <param name="serverIpAddress"></param>
    /// <param name="serverPort"></param>
    /// <returns>Socket</returns>
    private static Socket EstablishConnection(IPAddress serverIpAddress, int serverPort)
    {
        var newServerSocket = new Socket(
            AddressFamily.InterNetwork, 
            SocketType.Stream, 
            ProtocolType.Tcp
        );
        
        var remoteEndPoint = new IPEndPoint(
            serverIpAddress, 
            serverPort
        );
        
        Console.WriteLine("\nWaiting for connection...");
        newServerSocket.Connect(remoteEndPoint);
            
        Console.WriteLine(
            "Connection established with IP: {0}", 
            serverIpAddress
            );
        
        Console.WriteLine("Type \"exit\" to terminate the connection\n");

        return newServerSocket;
    }
    
    /// <summary>
    /// Send and receive messages from the server.
    /// </summary>
    /// <param name="serverSocket"></param>
    private static void NetworkListener(Socket serverSocket)
    {
        var input = string.Empty;
        
        while (input != "exit")
        {
            /* Wait for user input and send it to the server */
            
            Console.Write("Input: ");
            
            input = Console.ReadLine() ?? string.Empty;
            var byteInput = Encoding.UTF8.GetBytes(input);
            
            // If the user input is empty, skip the rest of the loop
            if (string.IsNullOrEmpty(input)) continue;
        
            serverSocket.Send(byteInput);
            
            /* Wait for server response and display it */
            
            var byteResponse = new byte[1024];
            
            // Receive response from server and store it in byteResponse.
            // Also store the length of the response in responseLength.
            var responseLength = serverSocket.Receive(byteResponse);
            
            var response = Encoding.UTF8.GetString(byteResponse, 0, responseLength);
            Console.WriteLine(response);
        }
    }
    
    /// <summary>
    /// Shutdown the connection with the server.
    /// </summary>
    /// <param name="serverSocket"></param>
    private static void TerminateConnection(Socket serverSocket)
    {
        serverSocket.Shutdown(SocketShutdown.Both);
        serverSocket.Close();
        
        Console.WriteLine("Connection terminated");
        
        Environment.Exit(0);
    }
}
