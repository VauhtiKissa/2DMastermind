using Godot;
using System;
using System.Linq;

public partial class Color_picker : Node2D
{
	private Guess_cube parent;
	private TextureButton[] buttons;
	private AnimationPlayer animator;
	public override void _Ready()
	{

		parent = (Guess_cube)GetParent().GetParent();
		animator = GetNode<AnimationPlayer>("./AnimationPlayer");

		buttons = new TextureButton[9];

		for (int i = 0 ; i < buttons.Count()-1; i++){

			buttons[i] = (TextureButton)GetNode("./Node2D/buttons").GetChildren()[i];
        	((button_sound)GetNode("/root/ButtonSoundMaker")).connect_button(buttons[i], false);
			buttons[i].TextureNormal = (Texture2D)GD.Load(Color_values.Color_sprites[i]);
			buttons[i].TexturePressed = (Texture2D)GD.Load(Color_values.Color_sprites_pressed[i]);
			buttons[i].TextureHover = (Texture2D)GD.Load(Color_values.Color_sprites_pressed[i]);
		}

		buttons[8] = (TextureButton)GetNode("./Node2D/buttons").GetChildren()[8];

	}

	public void pick_color(int given_color){

		parent.current_color = (GameColors)given_color;
	}
	
	public void round_end_button_pressed(){

		for (int i = 0; i < buttons.Count(); i++)
		{
			buttons[i].Disabled = true;
		}
		animator.PlayBackwards("color_picker");
		parent.round_end();
	}
	
	public void activate(){

		animator.Play("color_picker");

		for (int i = 0; i < buttons.Count(); i++)
		{
			buttons[i].Disabled = false;
		}

	}
}