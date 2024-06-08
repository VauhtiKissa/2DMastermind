using Godot;
using System;

public partial class button_sound : Node
{

	private AudioStreamPlayer hover_sound_maker;
	private AudioStreamPlayer click_sound_maker;

	private TextureButton mute_button;

	public override void _Ready()
	{
		hover_sound_maker = (AudioStreamPlayer)GetChild(0);
		click_sound_maker = (AudioStreamPlayer)GetChild(1);
		mute_button = (TextureButton)GetChild(2);
		if(config_manager.config.button_mute){
			mute_pressed();
		}
	}

	public void connect_button(TextureButton button, bool make_hover_noise ){
		if(make_hover_noise == true){
			button.MouseEntered += play_hover_noise;
			button.Pressed += play_click_noise;
		}else{
			button.Pressed += play_click_noise;
		}
	}

	public void play_hover_noise(){
		if(config_manager.config.button_mute == false){
		hover_sound_maker.Play();
		}
	}

	public void play_click_noise(){
		if (config_manager.config.button_mute == false)
		{
		click_sound_maker.Play();
		}
	}
	public void mute_pressed(){
		config_manager.config.button_mute = !config_manager.config.button_mute;
		if(config_manager.config.button_mute == true){
			mute_button.TextureNormal = (Texture2D)GD.Load("res://sprites/other_buttons/mute_button_off.png");
		}else{
			mute_button.TextureNormal = (Texture2D)GD.Load("res://sprites/other_buttons/mute_button_on.png");
		}
	config_manager.save();
	}
}
