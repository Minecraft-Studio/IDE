using Godot;
using System;
using System.Collections.Generic;
public class Boot : Control
{
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
		var args = new Stack<string>();
		args.Push("server/dist/index.js");
		OS.Execute(nodePath, args.ToArray(), false);
	}

}
