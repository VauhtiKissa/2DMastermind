using Godot;
using System;

public partial class menu : Node2D
{

	public Node normal_game;
	public Node tutorial_game;
	public Node options;

	public override void _Ready()
	{
		normal_game = ResourceLoader.Load<PackedScene>("res://prefabs/game/game.tscn").Instantiate();
		tutorial_game = ResourceLoader.Load<PackedScene>("res://prefabs/tutorial/tutorial.tscn").Instantiate();

		GetNode<SoundHandler>("/root/SoundHandler").connectButton(GetNode<TextureButton>("./start"), true);
		GetNode<SoundHandler>("/root/SoundHandler").connectButton(GetNode<TextureButton>("./tutorial"), true);
		GetNode<SoundHandler>("/root/SoundHandler").connectButton(GetNode<TextureButton>("./quit"), true);

		if(config_manager.config.did_tutorial == false){
			GetNode<TextureButton>("./start").Position = new Vector2(0,1000);
			GetNode<TextureButton>("./tutorial").Position = new Vector2(360,160);
			GetNode<TextureButton>("./quit").Position = new Vector2(360,248);
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
