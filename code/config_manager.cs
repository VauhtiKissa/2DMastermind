using Godot;
using System;
using System.IO;
using System.Text.Json;

public class Config{
	public bool did_tutorial { get; set;} = false;

	public bool music_mute { get; set;} = false;

	public bool button_mute { get; set;} = false;
	public bool Fullscreen { get; set;} = false;
}

public partial class config_manager : Node
{

	private TextureButton mute_button;

	public override void _Ready()
	{
		if(Godot.FileAccess.FileExists("user://2DMastermind.json")){
    	Godot.FileAccess file = Godot.FileAccess.Open("user://2DMastermind.json", Godot.FileAccess.ModeFlags.Read);
		config = JsonSerializer.Deserialize<Config>(file.GetAsText());
    	file.Close();
		mute_button = (TextureButton)GetChild(0);
		}else{
			config = new Config();
		}
	}

	public static void save(){
		DirAccess.RemoveAbsolute("user://2DMastermind.json");
		Godot.FileAccess file = Godot.FileAccess.Open("user://2DMastermind.json", Godot.FileAccess.ModeFlags.Write);
		file.StoreString(JsonSerializer.Serialize(config));
		file.Close();
	}
	public static Config config;

	public void change_full_screen(){
		config.Fullscreen = !config.Fullscreen;
		if(config.Fullscreen){
			GetWindow().Mode = Window.ModeEnum.Fullscreen;
			mute_button.TextureNormal = (Texture2D)GD.Load("res://sprites/other_buttons/fullscreen_off.png");
		}else{
			GetWindow().Mode = Window.ModeEnum.Maximized;
			mute_button.TextureNormal = (Texture2D)GD.Load("res://sprites/other_buttons/fullscreen_on.png");
		}

	}
}
