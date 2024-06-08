using Godot;
using System;
using System.Runtime.CompilerServices;

public partial class menu : Node2D
{

	public Node normal_game;
	public Node tutorial_game;
	public Node options;

	public override void _Ready()
	{
		normal_game = ResourceLoader.Load<PackedScene>("res://prefabs/game/game.tscn").Instantiate();
		tutorial_game = ResourceLoader.Load<PackedScene>("res://prefabs/tutorial/tutorial.tscn").Instantiate();
		options = ResourceLoader.Load<PackedScene>("res://prefabs/game/options_menu.tscn").Instantiate();

		((button_sound)GetNode("/root/ButtonSoundMaker")).connect_button((TextureButton)GetChild(0), true);
		((button_sound)GetNode("/root/ButtonSoundMaker")).connect_button((TextureButton)GetChild(1), true);
		((button_sound)GetNode("/root/ButtonSoundMaker")).connect_button((TextureButton)GetChild(2), true);

		if(config_manager.config.did_tutorial == false){
			((TextureButton)GetNode(GetPath()+"/start")).Position = new Vector2(0,1000);
			((TextureButton)GetNode(GetPath()+"/tutorial")).Position = new Vector2(360,160);
			((TextureButton)GetNode(GetPath()+"/quit")).Position = new Vector2(360,248);
		}

	}

	public void play_pressed(){
		GetTree().Root.AddChild(normal_game);
		GetParent().QueueFree();
	}

	public void tutorial_pressed(){
		GetTree().Root.AddChild(tutorial_game);
		GetParent().QueueFree();
	}

		public void options_pressed(){
		GetTree().Root.AddChild(options);
		GetParent().QueueFree();
	}

	public void quit_pressed(){
		GetTree().Quit();
	}

}
