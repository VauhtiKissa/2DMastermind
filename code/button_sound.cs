using Godot;
using System;

public partial class button_sound : Node
{
	public AudioStreamPlayer hover_sound_maker;
	public AudioStreamPlayer click_sound_maker;
	private TextureButton mute_button;
	private Slider slider;

	public override void _Ready()
	{
		hover_sound_maker = GetNode<AudioStreamPlayer>("./hover_sound");
		click_sound_maker = GetNode<AudioStreamPlayer>("./click_sound");
		mute_button = GetNode<TextureButton>("./TextureButton");

		slider = GetNode<Slider>("TextureButton/VSlider");
		slider.Value = config_manager.config.button_volume;
		slider.Visible = false;
		Noise_slider_moved(config_manager.config.button_volume);
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
		hover_sound_maker.Play();
	}

	public void play_click_noise(){
		click_sound_maker.Play();
	}
	public void mute_pressed(){
		slider.Visible = !slider.Visible;
	}

	public void Noise_slider_moved(float new_number){
	
		config_manager.config.button_volume = new_number;
		config_manager.save();

		if(new_number == 0 ){
			mute_button.TextureNormal = (Texture2D)GD.Load("res://sprites/other_buttons/mute_button_off.png");
			hover_sound_maker.VolumeDb = -80;
			click_sound_maker.VolumeDb = -80;
		}else{
			mute_button.TextureNormal = (Texture2D)GD.Load("res://sprites/other_buttons/mute_button_on.png");
			hover_sound_maker.VolumeDb = config_manager.config.button_volume / 10 -5;
			click_sound_maker.VolumeDb = config_manager.config.button_volume / 10 -5;
		}
	}
}
