using Godot;
using System;
using System.Collections.Generic;
using System.Linq;


public partial class Guess_cube : Node2D
{
	public GameColors current_color;
	private GameColors[] current_values;
	private Node color_picker;
	private Node buttons;
	// Ordered rows then colums
	private Node correct_answer_numbers;
	private Node wrong_place_numbers;


 
	public override void _Ready()
	{
		current_values = new GameColors[16];

		color_picker = GetChild(0);
		buttons = GetChild(2);
		correct_answer_numbers = GetChild(3);
		wrong_place_numbers = GetChild(4);

		for (int i = 0 ; i < buttons.GetChildCount()-1; i++){
			((TextureButton)buttons.GetChild(i)).Modulate = Color_values.Colors[0];
		}
	}

	public void button_pressed(int number){
		current_values[number] = current_color;
		// Colors the button
		((TextureButton)buttons.GetChild(number)).Modulate = Color_values.Colors[(int)current_color];
	}

	public void round_end(){

		// fort checking victory condition
		int corrects = 0;

		for (int x = 0; x < 8; x++)
		{
			

			int correct = 0; 
			int wrong_position = 0;

			List<GameColors> correct_colors = new List<GameColors>();
			List<GameColors> input_colors = new List<GameColors>();
		
			for (int i = 0; i < 4; i++)
			{
				correct_colors.Add(Game_coordinator.correct_answer[correct_answer_indexer(x,i)]);
				input_colors.Add(current_values[correct_answer_indexer(x,i)]);
			}

			for (int i = 3; i >= 0; i--)
			{
				if(correct_colors[i] == input_colors[i]){
					correct += 1;
					correct_colors.RemoveAt(i);
					input_colors.RemoveAt(i);
				}
			}

			((Label)correct_answer_numbers.GetChild(x)).Text = correct.ToString();

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
			((Label)wrong_place_numbers.GetChild(x)).Text = wrong_position.ToString();
			
			for (int i = 0; i < 16; i++)
			{
				((TextureButton)buttons.GetChild(i)).Disabled = true;
			}

			corrects += correct;
			if(corrects == 32){
				((Game_coordinator)GetNode("../..")).victory();
			}
		}
	if(corrects != 32){
		((Game_coordinator)GetNode("../..")).start_round();
	}
	}

	private int correct_answer_indexer(int index, int internal_index){
		if(index < 4){
			return index * 4 + internal_index;
		}else{
			return index + internal_index * 4 - 4;
		}
	}

	public void activate(){
		for (int i = 0; i < 16; i++)
		{
			((TextureButton)buttons.GetChild(i)).Disabled = false;
		}
		((Color_picker)color_picker).activate();
	}
}