using Godot;
using System;

public partial class music_player : Node
{
	private TextureButton mute_button;

	private Node[] sources;

	private int music_level = 1;

	public override void _Ready()
	{
		sources = new Node[8];
		for (int i = 0; i < GetChildCount()-1; i++)
		{
			sources[i] = GetChild(i);	
		}
		mute_button = (TextureButton)GetChild(8);
		if(config_manager.config.music_mute){
			mute_pressed();
		}
	}

	public void round_up(int number){
		if(config_manager.config.music_mute == false){
		((AudioStreamPlayer)sources[number]).VolumeDb = 0;
		}
		music_level += 1;
	}

	public void restart_sound_level(){
		for (int i = 1; i < sources.Length; i++)
		{
			((AudioStreamPlayer)sources[i]).VolumeDb = -80;
		}
		music_level = 0;
	}

	public void mute_pressed(){
		config_manager.config.music_mute = !config_manager.config.music_mute;

		if(config_manager.config.music_mute == true){

			mute_button.TextureNormal = (Texture2D)GD.Load("res://sprites/other_buttons/music_button_off.png");

			for (int i = 0; i < music_level; i++)
			{
				((AudioStreamPlayer)sources[i]).VolumeDb = -80;
			}
		}else{

			mute_button.TextureNormal = (Texture2D)GD.Load("res://sprites/other_buttons/music_button_on.png");
			
			for (int i = 0; i < music_level; i++)
			{
				((AudioStreamPlayer)sources[i]).VolumeDb = 0;
			}
		}
		config_manager.save();
	}
}