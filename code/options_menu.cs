using Godot;
using System;

public partial class options_menu : Node2D
{
	private Node2D Sprites;

	private music_player music_box;
	private button_sound button_Sound;

	public override void _Ready()
	{
		music_box = GetNode<music_player>("/root/MusicPlayer");
		button_Sound = GetNode<button_sound>("/root/ButtonSoundMaker");
		Sprites = GetNode<Node2D>("Sprites");

		((button_sound)GetNode("/root/ButtonSoundMaker")).connect_button((TextureButton)GetChild(3), true);
		((button_sound)GetNode("/root/ButtonSoundMaker")).connect_button((TextureButton)GetChild(4), true);

	}

	public void SFX_slider_moved(int new_number){
		button_Sound.hover_sound_maker.VolumeDb = new_number; 
		button_Sound.click_sound_maker.VolumeDb = new_number; 
	}

	public void Music_slider_moved(int new_number){
		config_manager.config.music_volume = new_number;
		music_box.sources[0].VolumeDb = config_manager.config.music_volume;
	}

	public void Timer_button_pressed(){

	}

	public void Exit_and_save_pressed(){
		config_manager.save();
		GetTree().Root.AddChild(ResourceLoader.Load<PackedScene>("res://prefabs/game/menu.tscn").Instantiate());
		QueueFree();
	}

}
