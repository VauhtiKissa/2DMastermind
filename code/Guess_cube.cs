using Godot;
using System;
using System.Collections.Generic;

public partial class Guess_cube : Node2D
{
	public GameColors current_color;
	private GameColors[] current_values;
	private Node color_picker;
	private Node buttons;
	// Ordered rows then colums
	private Node correct_answer_numbers;
	private Node wrong_place_numbers;
	[Export]
	public bool slider = false;
 	private AnimationPlayer animator;

	public override void _Ready()
	{
		current_values = new GameColors[16];

		animator = GetNode<AnimationPlayer>("./AnimationPlayer");
		color_picker = GetNode("./Animation_positioner/color_picker");
		buttons = GetNode("./Animation_positioner/buttons");
		correct_answer_numbers = GetNode("./Animation_positioner/correct_answer_numbers");
		wrong_place_numbers = GetNode("./Animation_positioner/wrong_place_numbers");

		for (int i = 0 ; i < buttons.GetChildCount(); i++){
			
			((TextureButton)buttons.GetChild(i)).TextureNormal = (Texture2D)GD.Load(Color_values.Color_sprites[0]);
        	((button_sound)GetNode("/root/ButtonSoundMaker")).connect_button((TextureButton)buttons.GetChild(i), false);

			((TextureButton)buttons.GetChild(i)).TexturePressed = (Texture2D)GD.Load(Color_values.Color_sprites_pressed[0]);
			((TextureButton)buttons.GetChild(i)).TextureHover = (Texture2D)GD.Load(Color_values.Color_sprites_pressed[0]);
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
			((TextureButton)buttons.GetChild(number)).TexturePressed = (Texture2D)GD.Load(Color_values.Color_sprites[8]);
			((TextureButton)buttons.GetChild(number)).TextureHover = (Texture2D)GD.Load(Color_values.Color_sprites_pressed[8]);
		}
	}


	public void round_end(){

		// for checking victory condition
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

		if(slider == true){
			animator.PlayBackwards("guess_cube_slide");
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

		if(slider == true){
			animator.Play("guess_cube_slide");
		}

		for (int i = 0; i < 16; i++)
		{
			current_values[i] = GameColors.blank;
			((TextureButton)buttons.GetChild(i)).Disabled = false;
			((TextureButton)buttons.GetChild(i)).TextureNormal = (Texture2D)GD.Load(Color_values.Color_sprites[8]);
			((TextureButton)buttons.GetChild(i)).TexturePressed = (Texture2D)GD.Load(Color_values.Color_sprites_pressed[8]);
			((TextureButton)buttons.GetChild(i)).TextureHover = (Texture2D)GD.Load(Color_values.Color_sprites_pressed[8]);
		}
		((Color_picker)color_picker).activate();
	}
}