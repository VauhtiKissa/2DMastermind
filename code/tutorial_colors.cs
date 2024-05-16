using Godot;
using System;

public partial class tutorial_colors : Node2D
{
	private tutorial_guess_row parent;
	private Node buttons;
	public override void _Ready()
	{
		parent = (tutorial_guess_row)GetParent();
		buttons = GetChild(0);
		for (int i = 0 ; i < buttons.GetChildCount()-1; i++){
			((TextureButton)buttons.GetChild(i)).Modulate = Color_values.Colors[i];
		}
	}

	public void pick_color(int given_color){
		parent.current_color = (GameColors)given_color;
	}
}
