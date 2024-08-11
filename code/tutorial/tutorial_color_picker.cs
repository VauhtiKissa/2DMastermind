using Godot;
using System;
using System.Linq;

public partial class tutorial_color_picker : Node
{
	private GuessCube guess_cube;
	private TextureButton[] buttons;
	public override void _Ready()
	{
		guess_cube = GetNode<GuessCube>("../..");
		buttons = GetNode<Node2D>("./AnimationPositioner/Buttons").GetChildren().Cast<TextureButton>().ToArray();
		
		for (int i = 0 ; i < buttons.Length-1; i++){
			buttons[i].TextureNormal = GD.Load<Texture2D>(Color_values.color_sprites[i]);
			buttons[i].TexturePressed = GD.Load<Texture2D>(Color_values.color_sprites_pressed[i]);
			buttons[i].TextureHover = GD.Load<Texture2D>(Color_values.color_sprites_pressed[i]);
			GetNode<SoundHandler>("/root/ButtonSoundMaker").connectButton(buttons[i], false);
		}

        GetNode<SoundHandler>("/root/ButtonSoundMaker").connectButton(buttons[8], true);
	
	}

	public void pick_color(int given_color){
		guess_cube.chosen_color = (GameColors)given_color;
	}
	
	public void check_button_pressed(){
		guess_cube.round_end();
	}
}