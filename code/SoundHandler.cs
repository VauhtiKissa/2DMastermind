using Godot;
using System;
using System.Linq;

public partial class SoundHandler : Node
{
	public AudioStreamPlayer hover_sound_maker;
	public AudioStreamPlayer click_sound_maker;
	private TextureButton sound_button;
	private Slider sound_slider;
	private TextureButton music_button;
	private AudioStreamPlayer[] sources;
	private Slider music_slider;
	private int music_level = 1;

	public override void _Ready()
	{

		// music
		music_button = GetNode<TextureButton>("./Buttons/MusicButton");
		sources = GetNode<Node>("./Music").GetChildren().Cast<AudioStreamPlayer>().ToArray();

		music_slider = GetNode<Slider>("./Buttons/MusicButton/VSlider");
		music_slider.Value = config_manager.config.music_volume;
		music_slider.Visible = false;
		musicSliderMoved(config_manager.config.music_volume);

		// sound
		sound_button = GetNode<TextureButton>("./Buttons/SoundButton");
		hover_sound_maker = GetNode<AudioStreamPlayer>("./ButtonSounds/HoverSound");
		click_sound_maker = GetNode<AudioStreamPlayer>("./ButtonSounds/ClickSound");

		sound_slider = GetNode<Slider>("./Buttons/SoundButton/VSlider");
		sound_slider.Value = config_manager.config.button_volume;
		sound_slider.Visible = false;
		noiseSliderMoved(config_manager.config.button_volume);
	}

	public void connectButton(TextureButton button, bool make_hover_noise ){
		if(make_hover_noise == true){
			button.MouseEntered += playHoverNoise;
			button.Pressed += playClickNoise;
		}else{
			button.Pressed += playClickNoise;
		}
	}

	public void playHoverNoise(){
		hover_sound_maker.Play();
	}

	public void playClickNoise(){
		click_sound_maker.Play();
	}
	public void soundButtonPressed(){
		sound_slider.Visible = !sound_slider.Visible;
	}

	public void noiseSliderMoved(float new_number){
	
		config_manager.config.button_volume = new_number;
		config_manager.save();

		if(new_number == 0 ){
			sound_button.TextureNormal = GD.Load<Texture2D>("res://sprites/other_buttons/mute_button_off.png");
			hover_sound_maker.VolumeDb = -80;
			click_sound_maker.VolumeDb = -80;
		}else{
			sound_button.TextureNormal = GD.Load<Texture2D>("res://sprites/other_buttons/mute_button_on.png");
			hover_sound_maker.VolumeDb = config_manager.config.button_volume / 10 -5;
			click_sound_maker.VolumeDb = config_manager.config.button_volume / 10 -5;
		}
	}

/*




*/
	// Music handling ----------------------------------------------------
	public void roundUp(int number){
		if(config_manager.config.music_volume > 0){
			sources[number].VolumeDb = config_manager.config.music_volume /10 -5;
		}
		music_level += 1;
	}

	public void restartSoundLevel(){
		for (int i = 1; i < sources.Length; i++)
		{
			sources[i].VolumeDb = -80;
		}
		music_level = 1;
	}

	public void musicButtonPessed(){
		music_slider.Visible = !music_slider.Visible;
	}

	public void musicSliderMoved(float new_number){
	
		config_manager.config.music_volume = new_number;
		config_manager.save();

		if(new_number == 0 ){
			music_button.TextureNormal = GD.Load<Texture2D>("res://sprites/other_buttons/music_button_off.png");
			for (int i = 0; i < music_level; i++)
			{
				sources[i].VolumeDb = -80;
			}
		}else{
			music_button.TextureNormal = GD.Load<Texture2D>("res://sprites/other_buttons/music_button_on.png");
			for (int i = 0; i < music_level; i++)
			{
				sources[i].VolumeDb = config_manager.config.music_volume /10 -5;
			}
		}
	}
}
