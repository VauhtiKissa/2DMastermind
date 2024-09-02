using Godot;
using System;
using System.Linq;

public partial class SoundHandler : Node
{
	public AudioStreamPlayer hoverSoundMaker;
	public AudioStreamPlayer clickSoundMaker;
	private TextureButton soundButton;
	private Slider soundSlider;
	private TextureButton musicButton;
	private AudioStreamPlayer[] sources;
	private Slider musicSlider;
	private int musicLevel = 1;

	public override void _Ready()
	{

		// music
		musicButton = GetNode<TextureButton>("./Buttons/MusicButton");
		sources = GetNode<Node>("./Music").GetChildren().Cast<AudioStreamPlayer>().ToArray();

		musicSlider = GetNode<Slider>("./Buttons/MusicButton/VSlider");
		musicSlider.Value = ConfigManager.config.musicVolume;
		musicSlider.Visible = false;
		musicSliderMoved(ConfigManager.config.musicVolume);

		// sound
		soundButton = GetNode<TextureButton>("./Buttons/SoundButton");
		hoverSoundMaker = GetNode<AudioStreamPlayer>("./ButtonSounds/HoverSound");
		clickSoundMaker = GetNode<AudioStreamPlayer>("./ButtonSounds/ClickSound");

		soundSlider = GetNode<Slider>("./Buttons/SoundButton/VSlider");
		soundSlider.Value = ConfigManager.config.buttonVolume;
		soundSlider.Visible = false;
		noiseSliderMoved(ConfigManager.config.buttonVolume);
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
		hoverSoundMaker.Play();
	}

	public void playClickNoise(){
		clickSoundMaker.Play();
	}
	public void soundButtonPressed(){
		soundSlider.Visible = !soundSlider.Visible;
	}

	public void noiseSliderMoved(float new_number){
	
		ConfigManager.config.buttonVolume = new_number;
		ConfigManager.save();

		if(new_number == 0 ){
			soundButton.TextureNormal = GD.Load<Texture2D>("res://sprites/other_buttons/mute_button_off.png");
			hoverSoundMaker.VolumeDb = -80;
			clickSoundMaker.VolumeDb = -80;
		}else{
			soundButton.TextureNormal = GD.Load<Texture2D>("res://sprites/other_buttons/mute_button_on.png");
			hoverSoundMaker.VolumeDb = ConfigManager.config.buttonVolume / 10 -5;
			clickSoundMaker.VolumeDb = ConfigManager.config.buttonVolume / 10 -5;
		}
	}

	public void roundUp(int number){
		if(ConfigManager.config.musicVolume > 0){
			sources[number].VolumeDb = ConfigManager.config.musicVolume /10 -5;
		}
		musicLevel += 1;
	}

	public void restartSoundLevel(){
		for (int i = 1; i < sources.Length; i++)
		{
			sources[i].VolumeDb = -80;
		}
		musicLevel = 1;
	}

	public void musicButtonPessed(){
		musicSlider.Visible = !musicSlider.Visible;
	}

	public void musicSliderMoved(float new_number){
	
		ConfigManager.config.musicVolume = new_number;
		ConfigManager.save();

		if(new_number == 0 ){
			musicButton.TextureNormal = GD.Load<Texture2D>("res://sprites/other_buttons/music_button_off.png");
			for (int i = 0; i < musicLevel; i++)
			{
				sources[i].VolumeDb = -80;
			}
		}else{
			musicButton.TextureNormal = GD.Load<Texture2D>("res://sprites/other_buttons/music_button_on.png");
			for (int i = 0; i < musicLevel; i++)
			{
				sources[i].VolumeDb = ConfigManager.config.musicVolume /10 -5;
			}
		}
	}
}
