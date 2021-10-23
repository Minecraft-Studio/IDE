using Godot;
using System;
using System.Collections.Generic;
public class Boot : Control
{
	[Export] int port = 35565;
	[Export] WebSocketClient webSocketClient = new WebSocketClient();
	public override void _Ready()
	{
		var fm = new File();
		string nodePath;
		if (fm.FileExists("res://node/node.exe"))
		{
			nodePath = "node/node.exe";
		}
		else if (fm.FileExists("res://node/node"))
		{
			nodePath = "./node/node";
		}
		else
		{
			nodePath = "node";
		}
		fm.Close();

		// Avoid running if node file is not present (aka in debug mode)
		// instead run "npm run dev" to open nodejs server manually for testing
		if (nodePath != "node")
		{
			var args = new Stack<string>();
			args.Push("server/dist/index.js");
			OS.Execute(nodePath, args.ToArray(), false);
		}

		webSocketClient.Connect("connection_closed", this, "_closed");
		webSocketClient.Connect("connection_error", this, "_closed");

		webSocketClient.Connect("connection_established", this, "_connected");
		webSocketClient.Connect("data_received", this, "_on_data");

		var error = webSocketClient.ConnectToUrl($"ws://localhost:{port}");
		if (error != Error.Ok) GD.PrintErr(error);

	}

	private void _closed(bool wasClean = false)
	{
		GD.Print($"Server connection closed. Close is clean: {wasClean}");
	}

	private void _connected(string proto = "")
	{
		// This is called on connection, "proto" will be the selected WebSocket
		// sub-protocol (which is optional)
		GD.Print($"Connected with protocol: {proto}");
		// You MUST always use get_peer(1).put_packet to send data to server,
		// and not put_packet directly when not using the MultiplayerAPI.
		webSocketClient.GetPeer(1).PutPacket("Test packet".ToUTF8());
	}

	private void _on_data()
	{
		GD.Print($"Got data from server: {webSocketClient.GetPeer(1).GetPacket().GetStringFromUTF8()}");
	}

	public override void _Process(float delta)
	{
		webSocketClient.Poll();
	}
}