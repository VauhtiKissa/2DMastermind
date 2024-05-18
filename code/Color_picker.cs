using Godot;
using System;

public partial class Color_picker : Node2D
{
	private Guess_cube parent;
	private Node buttons;
	public override void _Ready()
	{
		buttons = GetChild(0).GetChild(1);
		for (int i = 0 ; i < buttons.GetChildCount()-1; i++){

        	((button_sound)GetTree().Root.GetChild(1)).connect_button((TextureButton)buttons.GetChild(i), false);

			((TextureButton)buttons.GetChild(i)).TextureNormal = (Texture2D)GD.Load(Color_values.Color_sprites[i]);
			((TextureButton)buttons.GetChild(i)).TexturePressed = (Texture2D)GD.Load(Color_values.Color_sprites_pressed[i]);
			((TextureButton)buttons.GetChild(i)).TextureHover = (Texture2D)GD.Load(Color_values.Color_sprites_pressed[i]);
		}

        ((button_sound)GetTree().Root.GetChild(1)).connect_button((TextureButton)buttons.GetChild(8), false);

		parent = (Guess_cube)GetParent();
	}

	public void pick_color(int given_color){
		parent.current_color = (GameColors)given_color;
	}
	
	public void round_end_button_pressed(){
		for (int i = 0; i < 9; i++)
		{
			((TextureButton)buttons.GetChild(i)).Disabled = true;
		}
		((AnimationPlayer)GetChild(1)).Active = true;
		((AnimationPlayer)GetChild(1)).PlayBackwards("color_picker");
		parent.round_end();
	}
	
	public void activate(){
		((AnimationPlayer)GetChild(1)).Active = true;
		((AnimationPlayer)GetChild(1)).Play("color_picker");
		for (int i = 0; i < 9; i++)
		{
			((TextureButton)buttons.GetChild(i)).Disabled = false;
		}
	}
}