using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

public partial class tutorial_guess_row : Node2D
{
	public GameColors current_color;
	private GameColors[] current_values;
	private GameColors[] correct_values;
	private Node color_picker;
	private Node buttons;
	// Ordered rows then colums
	private Node correct_answer_number;
	private Node wrong_place_number;
 
	public override void _Ready()
	{
		current_values = new GameColors[4];

		color_picker = GetChild(0);
		buttons = GetChild(1);
		correct_answer_number = GetChild(2);
		wrong_place_number = GetChild(3);

		for (int i = 0 ; i < buttons.GetChildCount(); i++){
        	((button_sound)GetTree().Root.GetChild(1)).connect_button((TextureButton)buttons.GetChild(i), false);
			((TextureButton)buttons.GetChild(i)).TextureNormal = (Texture2D)GD.Load(Color_values.Color_sprites[0]);
			((TextureButton)buttons.GetChild(i)).TexturePressed = (Texture2D)GD.Load(Color_values.Color_sprites_pressed[0]);
			((TextureButton)buttons.GetChild(i)).TextureHover = (Texture2D)GD.Load(Color_values.Color_sprites_pressed[0]);
		}

        ((button_sound)GetTree().Root.GetChild(1)).connect_button((TextureButton)GetChild(4), true);

		correct_values = new GameColors[4];
		for (int i = 0 ; i < correct_values.Length ; i++){
			correct_values[i] = (GameColors)GD.RandRange(0,7);
		}
	}

	public void button_pressed(int number){
   	 	if (!Input.IsActionPressed("right_click")){
			current_values[number] = current_color;
			((TextureButton)buttons.GetChild(number)).TextureNormal = (Texture2D)GD.Load(Color_values.Color_sprites[(int)current_color]);
			((TextureButton)buttons.GetChild(number)).TexturePressed = (Texture2D)GD.Load(Color_values.Color_sprites_pressed[(int)current_color]);
			((TextureButton)buttons.GetChild(number)).TextureHover = (Texture2D)GD.Load(Color_values.Color_sprites_pressed[(int)current_color]);
	 	}else{
			current_values[number] = GameColors.blank;
			((TextureButton)buttons.GetChild(number)).TextureNormal = (Texture2D)GD.Load(Color_values.Color_sprites[8]);
			((TextureButton)buttons.GetChild(number)).TexturePressed = (Texture2D)GD.Load(Color_values.Color_sprites_pressed[8]);
			((TextureButton)buttons.GetChild(number)).TextureHover = (Texture2D)GD.Load(Color_values.Color_sprites_pressed[8]);
		}
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

		((Label)correct_answer_number).Text = correct.ToString();

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