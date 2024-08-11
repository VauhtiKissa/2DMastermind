using Godot;
using System;

public partial class menu : Node2D
{

	public Node normal_game;
	public Node tutorial_game;
	public Node options;

	public override void _Ready()
	{
		normal_game = ResourceLoader.Load<PackedScene>("res://prefabs/game/Game.tscn").Instantiate();
		tutorial_game = ResourceLoader.Load<PackedScene>("res://prefabs/tutorial/Tutorial.tscn").Instantiate();

		GetNode<SoundHandler>("/root/SoundHandler").connectButton(GetNode<TextureButton>("./Start"), true);
		GetNode<SoundHandler>("/root/SoundHandler").connectButton(GetNode<TextureButton>("./Tutorial"), true);
		GetNode<SoundHandler>("/root/SoundHandler").connectButton(GetNode<TextureButton>("./Quit"), true);

		if(ConfigManager.config.did_tutorial == false){
			GetNode<TextureButton>("./Start").Position = new Vector2(0,1000);
			GetNode<TextureButton>("./Tutorial").Position = new Vector2(360,160);
			GetNode<TextureButton>("./Quit").Position = new Vector2(360,248);
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

	public void quit_pressed(){
		GetTree().Quit();
	}

}
