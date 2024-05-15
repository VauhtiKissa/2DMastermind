using Godot;
using System;

public partial class tutorial_cube : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void button_pressed(int number){
		/*
		current_values[number] = current_color;
		// Colors the button
		((TextureButton)buttons.GetChild(number)).Modulate = Color_values.Colors[(int)current_color];
		*/
	}

}
