using Godot;
using System;

public partial class music_player : Node
{
	private TextureButton mute_button;
	public AudioStreamPlayer[] sources;
	private Slider slider;
	private int music_level = 1;

	public override void _Ready()
	{
		sources = new AudioStreamPlayer[8];

		for (int i = 0; i < GetChildCount()-1; i++)
		{
			sources[i] = GetChild<AudioStreamPlayer>(i);	
		}
		mute_button =  GetNode<TextureButton>("./TextureButton");
		
		slider = GetNode<Slider>("TextureButton/VSlider");
		slider.Value = config_manager.config.music_volume;
		slider.Visible = false;
		Music_slider_moved(config_manager.config.music_volume);

	}

	public void round_up(int number){
		if(config_manager.config.music_volume > 0){
			sources[number].VolumeDb = config_manager.config.music_volume /10 -5;
		}
		GD.Print(config_manager.config.music_volume / 10);
		music_level += 1;
	}

	public void restart_sound_level(){
		for (int i = 1; i < sources.Length; i++)
		{
			sources[i].VolumeDb = -80;
		}
		music_level = 1;
	}

	public void mute_pressed(){
		slider.Visible = !slider.Visible;
	}

	public void Music_slider_moved(float new_number){
	
		config_manager.config.music_volume = new_number;
		config_manager.save();

		if(new_number == 0 ){
			mute_button.TextureNormal = (Texture2D)GD.Load("res://sprites/other_buttons/music_button_off.png");
			for (int i = 0; i < music_level; i++)
			{
				sources[i].VolumeDb = -80;
			}
		}else{
			mute_button.TextureNormal = (Texture2D)GD.Load("res://sprites/other_buttons/music_button_on.png");
			for (int i = 0; i < music_level; i++)
			{
				sources[i].VolumeDb = config_manager.config.music_volume /10 -5;
			}
		}
	}
}