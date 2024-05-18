using Godot;
using System;

public partial class loss_screen : Node2D
{
	public void back_to_menu(){
		GetTree().Root.AddChild(ResourceLoader.Load<PackedScene>("res://prefabs/game/menu.tscn").Instantiate());
		GetParent().GetParent().QueueFree();
	}

	public void restart(){
		GetTree().Root.AddChild(ResourceLoader.Load<PackedScene>("res://prefabs/game/game.tscn").Instantiate());
		GetParent().QueueFree();
	}
	
	public void quit(){
		GetTree().Quit();
	}
}
