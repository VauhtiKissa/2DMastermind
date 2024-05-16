using Godot;
using System;
using System.Collections.Generic;


public partial class tutorial_guess_row : Node2D
{
	public GameColors current_color;
	private GameColors[] current_values;

	private GameColors[] correct_values;

	private Node buttons;
	private Node correct_number;
	private Node wrong_place_number;


	public override void _Ready()
	{
		current_values = new GameColors[4];

		buttons = GetChild(2);
		correct_number = GetChild(3);
		wrong_place_number = GetChild(4);

		for (int i = 0 ; i < buttons.GetChildCount(); i++){
			((TextureButton)buttons.GetChild(i)).Modulate = Color_values.Colors[0];
		}

		correct_values = new GameColors[4];
		for (int i = 0 ; i < 4 ; i++){
			correct_values[i] = (GameColors)GD.RandRange(0,7);
		}
	}

	public void button_pressed(int number){

		current_values[number] = current_color;
		((TextureButton)buttons.GetChild(number)).Modulate = Color_values.Colors[(int)current_color];
	}

	public void round_end(){
		int correct = 0; 
		int wrong_position = 0;

		List<GameColors> correct_colors = new List<GameColors>();
		List<GameColors> input_colors = new List<GameColors>();
		
		for (int i = 0; i < 4; i++)
		{
			correct_colors.Add(correct_values[i]);
			input_colors.Add(current_values[i]);
		}

		for (int i = 3; i >= 0; i--)
		{
			if(correct_colors[i] == input_colors[i]){
				correct += 1;
				correct_colors.RemoveAt(i);
				input_colors.RemoveAt(i);
			}
		}

		((Label)GetChild(3)).Text = correct.ToString();

		for (int i = 0; i < input_colors.Count; i++)
		{
			for (int e = 0; e < correct_colors.Count; e++){
				if(input_colors[i] == correct_colors[e]){
					wrong_position += 1;
					correct_colors.RemoveAt(e);
					break;
				}
			}
		}
		((Label)wrong_place_number).Text = wrong_position.ToString();
	}
}