using Godot;
using System;

public partial class loss_screen : Node2D
{
	public override void _Ready()
	{
        ((button_sound)GetNode("/root/ButtonSoundMaker")).connect_button((TextureButton)GetChild(2), false);
        	((button_sound)GetNode("/root/ButtonSoundMaker")).connect_button((TextureButton)GetChild(3), false);

		((AnimationPlayer)GetChild(4)).Active = true;
		((AnimationPlayer)GetChild(4)).Play("loss_pop_up");
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
