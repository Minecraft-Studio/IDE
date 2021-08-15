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
		lang.Open("res://assets/lang/en-us.json", File.ModeFlags.Read);
		var langStrings = JsonConvert.DeserializeObject<LanguageStrings>(lang.GetAsText());
		var messages = langStrings.motd;
		var randomizer = new Random();
		Text = messages[randomizer.Next(messages.Count)];
		lang.Close();
	}
}
