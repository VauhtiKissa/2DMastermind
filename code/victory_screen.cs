using Godot;
using System;

public partial class victory_screen : Node2D
{

	public override void _Ready()
	{
        ((button_sound)GetNode("/root/ButtonSoundMaker")).connect_button((TextureButton)GetChild(3), false);
        ((button_sound)GetNode("/root/ButtonSoundMaker")).connect_button((TextureButton)GetChild(4), false);

		((AnimationPlayer)GetChild(5)).Active = true;
		((AnimationPlayer)GetChild(5)).Play("victory_animation");
	}

	public void back_to_menu(){
		((music_player)GetNode("/root/MusicPlayer")).restart_sound_level();
		GetTree().Root.AddChild(ResourceLoader.Load<PackedScene>("res://prefabs/game/menu.tscn").Instantiate());
		GetParent().GetParent().QueueFree();
	}

	public void restart(){
		((music_player)GetNode("/root/MusicPlayer")).restart_sound_level();
		GetTree().Root.AddChild(ResourceLoader.Load<PackedScene>("res://prefabs/game/game.tscn").Instantiate());
		GetParent().GetParent().QueueFree();
	}
}
