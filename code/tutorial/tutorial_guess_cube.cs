using Godot;
using System;
using System.Collections.Generic;

public partial class tutorial_guess_cube : Node2D
{
	public GameColors current_color;
	private GameColors[] current_values;
	private GameColors[] correct_values;
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

		for (int i = 0 ; i < buttons.GetChildCount(); i++){
			((SoundHandler)GetNode("/root/ButtonSoundMaker")).connectButton((TextureButton)buttons.GetChild(i), false);
			((TextureButton)buttons.GetChild(i)).TextureNormal = (Texture2D)GD.Load(Color_values.color_sprites[8]);
			((TextureButton)buttons.GetChild(i)).TexturePressed = (Texture2D)GD.Load(Color_values.color_sprites[8]);
			((TextureButton)buttons.GetChild(i)).TextureHover = (Texture2D)GD.Load(Color_values.color_sprites_pressed[8]);
		}

	

		correct_values = new GameColors[16];
		for (int i = 0 ; i < correct_values.Length ; i++){
			correct_values[i] = (GameColors)GD.RandRange(0,7);
			current_values[i] = GameColors.blank;
		}
	}

	public void button_pressed(int number){
   	 	if (!Input.IsActionPressed("right_click")){
			current_values[number] = current_color;
			((TextureButton)buttons.GetChild(number)).TextureNormal = (Texture2D)GD.Load(Color_values.color_sprites[(int)current_color]);
			((TextureButton)buttons.GetChild(number)).TexturePressed = (Texture2D)GD.Load(Color_values.color_sprites_pressed[(int)current_color]);
			((TextureButton)buttons.GetChild(number)).TextureHover = (Texture2D)GD.Load(Color_values.color_sprites_pressed[(int)current_color]);
	 	}else{
			current_values[number] = GameColors.blank;
			((TextureButton)buttons.GetChild(number)).TextureNormal = (Texture2D)GD.Load(Color_values.color_sprites[8]);
			((TextureButton)buttons.GetChild(number)).TexturePressed = (Texture2D)GD.Load(Color_values.color_sprites[8]);
			((TextureButton)buttons.GetChild(number)).TextureHover = (Texture2D)GD.Load(Color_values.color_sprites_pressed[8]);
		}
	}

	public void round_end(){

		for (int x = 0; x < 8; x++)
		{
			

			int correct = 0; 
			int wrong_position = 0;

			List<GameColors> correct_colors = new List<GameColors>();
			List<GameColors> input_colors = new List<GameColors>();

			for (int i = 0; i < 4; i++)
			{
				correct_colors.Add(correct_values[correct_answer_indexer(x,i)]);
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
			
		}
	}

	private int correct_answer_indexer(int index, int internal_index){
		if(index < 4){
			return index * 4 + internal_index;
		}else{
			return index + internal_index * 4 - 4;
		}
	}
}