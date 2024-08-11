using Godot;
using System;
using System.Linq;

public partial class ColorChooser : Node2D
{
	private GuessCube parent;
	private TextureButton[] buttons;
	private AnimationPlayer animator;
	public override void _Ready()
	{

		parent = (GuessCube)GetParent().GetParent();

		try
		{
			animator = GetNode<AnimationPlayer>("./AnimationPlayer");
		}
		catch (Exception)
		{
			
		}

		buttons = GetNode<Node2D>("./AnimationPositioner/Buttons").GetChildren().Cast<TextureButton>().ToArray();
		
		for (int i = 0 ; i < buttons.Length-1; i++){
			buttons[i].TextureNormal = GD.Load<Texture2D>(Color_values.color_sprites[i]);
			buttons[i].TexturePressed = GD.Load<Texture2D>(Color_values.color_sprites_pressed[i]);
			buttons[i].TextureHover = GD.Load<Texture2D>(Color_values.color_sprites_pressed[i]);
			GetNode<SoundHandler>("/root/SoundHandler").connectButton(buttons[i], false);
		}

        GetNode<SoundHandler>("/root/SoundHandler").connectButton(buttons[8], true);
	
	}

	public void pickColor(int given_color){

		parent.chosen_color = (GameColors)given_color;
	}
	
	public void roundEndButtonPressed(){

		if(!parent.tutorial){
			for (int i = 0; i < buttons.Length; i++)
			{
				buttons[i].Disabled = true;
			}
			animator?.PlayBackwards("ColorChooserSlide");
		}
		parent.round_end();
	}
	
	public void activate(){

		for (int i = 0; i < buttons.Length; i++)
		{
			buttons[i].Disabled = false;
		}
		animator?.Play("ColorChooserSlide");

	}
}