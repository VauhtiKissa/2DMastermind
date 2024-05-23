using Godot;
using System;
using System.Runtime.CompilerServices;

public partial class menu : Node2D
{

	public Node normal_game;
	public Node tutorial_game;

	public override void _Ready()
	{
		normal_game = ResourceLoader.Load<PackedScene>("res://prefabs/game/game.tscn").Instantiate();
		tutorial_game = ResourceLoader.Load<PackedScene>("res://prefabs/tutorial/tutorial.tscn").Instantiate();


		((button_sound)GetNode("/root/ButtonSoundMaker")).connect_button((TextureButton)GetChild(2), true);
		((button_sound)GetNode("/root/ButtonSoundMaker")).connect_button((TextureButton)GetChild(3), true);
		((button_sound)GetNode("/root/ButtonSoundMaker")).connect_button((TextureButton)GetChild(4), true);

	}

	public void play_pressed(){
		GetTree().Root.AddChild(normal_game);
		GetParent().QueueFree();
	}

	public void tutorial_pressed(){
		GetTree().Root.AddChild(tutorial_game);
		GetParent().QueueFree();
	}

	public void quit_pressed(){
		GetTree().Quit();
	}

}
