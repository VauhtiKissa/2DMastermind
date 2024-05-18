using Godot;
using System;

public partial class loss_screen : Node2D
{
	public override void _Ready()
	{
        ((button_sound)GetTree().Root.GetChild(1)).connect_button((TextureButton)GetChild(2), true);
        ((button_sound)GetTree().Root.GetChild(1)).connect_button((TextureButton)GetChild(3), true);
	}

	public void back_to_menu(){

		((music_player)GetTree().Root.GetChild(0)).restart_sound_level();

		GetTree().Root.AddChild(ResourceLoader.Load<PackedScene>("res://prefabs/game/menu.tscn").Instantiate());
		GetParent().GetParent().QueueFree();
	}

	public void restart(){
		
		((music_player)GetTree().Root.GetChild(0)).restart_sound_level();

		GetTree().Root.AddChild(ResourceLoader.Load<PackedScene>("res://prefabs/game/game.tscn").Instantiate());
		GetParent().GetParent().QueueFree();
	}
}
