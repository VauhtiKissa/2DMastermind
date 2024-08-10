using Godot;
using System;
using System.Linq;
using System.Collections.Generic;

public partial class GuessCube : Node
{
	public GameColors chosen_color;
	private GameColors[] current_colors;
	private TextureButton[] buttons;
	private Label[] correct_answer_numbers;
	private Label[] wrong_place_numbers;
	private ColorChooser color_chooser;
	[Export]
	public bool tutorial = false;
	[Export]
	public bool sliding = false;

	public override void _Ready()
	{

		color_chooser = GetNode<ColorChooser>("./AnimationPositioner/ColorChooser");
		buttons = GetNode<Node2D>("./AnimationPositioner/Buttons").GetChildren().Cast<TextureButton>().ToArray();
		correct_answer_numbers = GetNode<Node2D>("./AnimationPositioner/CorrectAnswerNumbers").GetChildren().Cast<Label>().ToArray();
		wrong_place_numbers = GetNode<Node2D>("./AnimationPositioner/WrongPlaceNumbers").GetChildren().Cast<Label>().ToArray();

		current_colors = new GameColors[16];

		for (int i = 0; i < buttons.Length; i++)
		{
			buttons[i].TextureNormal = GD.Load<Texture2D>(Color_values.color_sprites[0]);
			buttons[i].TexturePressed = GD.Load<Texture2D>(Color_values.color_sprites_pressed[0]);
			buttons[i].TextureHover = GD.Load<Texture2D>(Color_values.color_sprites_pressed[0]);
        	GetNode<button_sound>("/root/ButtonSoundMaker").connect_button(buttons[i], false);

			current_colors[i] = GameColors.blank;
		}

		if(tutorial){
			Game_coordinator.generate_answer();
			activate();
		}

	}

	public void button_pressed(int button_index){

		current_colors[button_index] = Input.IsActionPressed("left_click") ? chosen_color : GameColors.blank;

		int color_index = (int)current_colors[button_index];

		buttons[button_index].TextureNormal = GD.Load<Texture2D>(Color_values.color_sprites[color_index]);
		buttons[button_index].TexturePressed = GD.Load<Texture2D>(Color_values.color_sprites_pressed[color_index]);
		buttons[button_index].TextureHover = GD.Load<Texture2D>(Color_values.color_sprites_pressed[color_index]);
	}

	public void round_end(){
		if(!tutorial){
			GetNode<Game_coordinator>("../../").submitAnswer(check_answer());
			disable();
		}else{
			check_answer();
		}
	}

	public bool check_answer(){

		int correct_answers = 0;

		for (int i = 0; i < 4; i++)
		{

			int row_correct_answers = 0;
			int row_wrong_position_answers = 0;
			int column_correct_answers = 0;
			int column_wrong_position_answers = 0;

			List<GameColors>row_incorrectly_guessed_correct_colors = new List<GameColors>();
			List<GameColors>row_incorrectly_guessed_colors = new List<GameColors>();
			List<GameColors>column_incorrectly_guessed_correct_colors = new List<GameColors>();
			List<GameColors>column_incorrectly_guessed_colors = new List<GameColors>();

			for (int e = 0; e < 4; e++)
			{
				// check row
				if(current_colors[i*4+e] == Game_coordinator.correct_answer[i*4+e]){
					correct_answers += 1;
					row_correct_answers += 1;
				}else{
					row_incorrectly_guessed_correct_colors.Add(Game_coordinator.correct_answer[i*4+e]);
					row_incorrectly_guessed_colors.Add(current_colors[i*4+e]);
				}
				// check column
				if(current_colors[i+e*4] == Game_coordinator.correct_answer[i+e*4]){
					column_correct_answers += 1;
				}else{
					column_incorrectly_guessed_correct_colors.Add(Game_coordinator.correct_answer[i+e*4]);
					column_incorrectly_guessed_colors.Add(current_colors[i+e*4]);
				}
			}
			foreach (GameColors color in row_incorrectly_guessed_colors)
			{
				if(row_incorrectly_guessed_correct_colors.Contains(color)){
					row_wrong_position_answers += 1;
					row_incorrectly_guessed_correct_colors.Remove(color);
				}

			}
			foreach (GameColors color in column_incorrectly_guessed_colors)
			{
				if(column_incorrectly_guessed_correct_colors.Contains(color)){
					column_wrong_position_answers += 1;
					column_incorrectly_guessed_correct_colors.Remove(color);
				}

			}
			correct_answer_numbers[i].Text = row_correct_answers.ToString();
			correct_answer_numbers[i+4].Text = column_correct_answers.ToString();
			wrong_place_numbers[i].Text = row_wrong_position_answers.ToString();
			wrong_place_numbers[i+4].Text = column_wrong_position_answers.ToString();
		}
		return correct_answers == 16;
	}

	public void activate(){
		if(sliding){
			GetNode<AnimationPlayer>("./AnimationPlayer").Play("GuessCubeSlide");
		}

		for (int i = 0; i < 16; i++)
		{
			buttons[i].Disabled = false;
			buttons[i].TextureNormal = GD.Load<Texture2D>(Color_values.color_sprites[(int)GameColors.blank]);
			buttons[i].TexturePressed = GD.Load<Texture2D>(Color_values.color_sprites_pressed[(int)GameColors.blank]);
			buttons[i].TextureHover = GD.Load<Texture2D>(Color_values.color_sprites_pressed[(int)GameColors.blank]);
		}
		color_chooser.activate();
	}

	public void disable(){
		if(sliding){
			GetNode<AnimationPlayer>("./AnimationPlayer").PlayBackwards("GuessCubeSlide");
		}
		for (int i = 0; i < 16; i++)
		{
			buttons[i].Disabled = true;
		}
	}

}
