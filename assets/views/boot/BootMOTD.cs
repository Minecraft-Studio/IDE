using Godot;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

[Tool]
public class BootMOTD : Label
{
	public class LanguageStrings
	{
		public List<string> motd { get; set; }
	}
	public override void _Ready()
	{
		var lang = new File();
		lang.Open("res://assets/lang/motd.txt", File.ModeFlags.Read);
		var messages = new List<string>(lang.GetAsText().Split("\n"));
		var randomizer = new Random();
		Text = messages[randomizer.Next(messages.Count)];
		lang.Close();
	}
}
