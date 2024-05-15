using Godot;
using System;

public partial class Color_picker : Node2D
{
	private Guess_cube parent;
	private Node buttons;
	public override void _Ready()
	{
		buttons = GetChild(1);
		for (int i = 0 ; i < buttons.GetChildCount()-1; i++){
			((TextureButton)buttons.GetChild(i)).Modulate = Color_values.Colors[i];
		}
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
		this.Position = new Vector2(304,68);
		parent.round_end();
	}
	
	public void activate(){
		this.Position = new Vector2(504,64);
	}

}
