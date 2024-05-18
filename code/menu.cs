using Godot;
using System;
using System.Runtime.CompilerServices;

public partial class menu : Node2D
{

	public Node normal_game;
	public Node tutorial_game;

	private double circle_poss;
	[Export]
	private float circle_distance = 5;
	[Export]
	private float circle_speed = 1;

	public override void _Ready()
	{
		normal_game = ResourceLoader.Load<PackedScene>("res://prefabs/game/game.tscn").Instantiate();
		tutorial_game = ResourceLoader.Load<PackedScene>("res://prefabs/tutorial/tutorial.tscn").Instantiate();

		((button_sound)GetTree().Root.GetChild(1)).connect_button((TextureButton)GetChild(2), true);
		((button_sound)GetTree().Root.GetChild(1)).connect_button((TextureButton)GetChild(3), true);
		((button_sound)GetTree().Root.GetChild(1)).connect_button((TextureButton)GetChild(4), true);

	}

	public override void _Process(double delta)
	{
		circle_poss = (circle_poss + delta * circle_speed)%(Math.PI*2);
		Position = new Vector2((float)Math.Sin(circle_poss),(float)Math.Cos(circle_poss)) * circle_distance;
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
