using Godot;
using System;

public partial class menu : Node2D
{

	public Node normal_game;
	public Node tutorial_game;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		normal_game = ResourceLoader.Load<PackedScene>("res://prefabs/game/game.tscn").Instantiate();
		tutorial_game = ResourceLoader.Load<PackedScene>("res://prefabs/tutorial/tutorial.tscn").Instantiate();
	}

	public void play_pressed(){
		GetTree().Root.AddChild(normal_game);
		QueueFree();
	}

	public void tutorial_pressed(){
		GetTree().Root.AddChild(tutorial_game);
		QueueFree();
	}

}
