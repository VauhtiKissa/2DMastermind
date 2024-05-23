using Godot;
using System;
using System.IO;
using System.Text.Json;

public class Config{
	public bool did_tutorial { get; set;} = false;
}

public partial class config_manager : Node
{

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		if(Godot.FileAccess.FileExists("user://2DMastermind.json")){
    	Godot.FileAccess file = Godot.FileAccess.Open("user://2DMastermind.json", Godot.FileAccess.ModeFlags.Read);
		config = JsonSerializer.Deserialize<Config>(file.GetAsText());
    	file.Close();
		}else{
			config = new Config();
		}

	}

	public static void save(){
		Godot.FileAccess file = Godot.FileAccess.Open("user://2DMastermind.json", Godot.FileAccess.ModeFlags.Write);
		file.StoreString(JsonSerializer.Serialize(config));
	}
	public static Config config;
}
