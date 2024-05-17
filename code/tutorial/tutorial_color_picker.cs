using Godot;
using System;

public partial class tutorial_color_picker : Node2D
{
	private tutorial_guess_cube parent;
	private Node buttons;
	public override void _Ready()
	{
		buttons = GetChild(1);
		for (int i = 0 ; i < buttons.GetChildCount()-1; i++){
			((TextureButton)buttons.GetChild(i)).TextureNormal = (Texture2D)GD.Load(Color_values.Color_sprites[i]);
			((TextureButton)buttons.GetChild(i)).TexturePressed = (Texture2D)GD.Load(Color_values.Color_sprites_pressed[i]);
		}
		parent = (tutorial_guess_cube)GetParent();
	}

	public void pick_color(int given_color){
		parent.current_color = (GameColors)given_color;
	}
	
	public void chech_button_pressed(){
		parent.round_end();
	}
}