using Godot;
using System;

public partial class button_sound : Node
{

	private AudioStreamPlayer hover_sound_maker;
	private AudioStreamPlayer click_sound_maker;

	public override void _Ready()
	{
		hover_sound_maker = (AudioStreamPlayer)GetChild(0);
		click_sound_maker = (AudioStreamPlayer)GetChild(1);
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
}
