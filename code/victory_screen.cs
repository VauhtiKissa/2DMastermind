using Godot;
using System;

public partial class victory_screen : Node2D
{
	public void back_to_menu(){

		music_player music_box = (music_player)GetTree().Root.GetChild(0);
		music_box.restart_sound_level();

		GetTree().Root.AddChild(ResourceLoader.Load<PackedScene>("res://prefabs/game/menu.tscn").Instantiate());
		GetParent().GetParent().QueueFree();
	}

	public void restart(){

		music_player music_box = (music_player)GetTree().Root.GetChild(0);
		music_box.restart_sound_level();

		GetTree().Root.AddChild(ResourceLoader.Load<PackedScene>("res://prefabs/game/game.tscn").Instantiate());
		GetParent().GetParent().QueueFree();
	}
}