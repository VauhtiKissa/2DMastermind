using Godot;
using System;
using System.Linq;
using System.Reflection.Emit;

public partial class Game_coordinator : Node2D
{
	private int round_number = 0;
	public static GameColors[] correct_answer;
	public static GameColors[] guessed_colors;

	private Guess_cube[] cubes;

	private double time = 0;

	public override void _Ready()
	{
		cubes = new Guess_cube[8];
		for (int i = 0; i < GetChild(0).GetChildren().Count; i++)
		{
			cubes[i] = (Guess_cube)GetChild(0).GetChildren()[i];
		}
		generate_answer();
		start_round();
		((button_sound)GetNode("/root/ButtonSoundMaker")).connect_button((TextureButton)GetChild(1), true);
	}

	public override void _Process(double delta)
	{
		time += delta;
	}

	private void generate_answer(){
		correct_answer = new GameColors[16];
		for (int i = 0 ; i < 16 ; i++){
			correct_answer[i] = (GameColors)GD.RandRange(0,7);
		}
	}

	public void start_round(){
		if(round_number < 8){

			music_player music_box = (music_player)GetNode("/root/MusicPlayer");
			music_box.round_up(round_number);

			cubes[round_number].activate();
			round_number += 1;
		}else{
			AddChild(ResourceLoader.Load<PackedScene>("res://prefabs/game/loss_screen.tscn").Instantiate());
		}
	}

	public void victory(){
		AddChild(ResourceLoader.Load<PackedScene>("res://prefabs/game/confetti_cannon.tscn").Instantiate());
		AddChild(ResourceLoader.Load<PackedScene>("res://prefabs/game/victory_screen.tscn").Instantiate());
		int milliseconds = (int)(time*1000);
		int minutes = milliseconds / 60000;
		milliseconds -= minutes * 60000;
		int seconds = milliseconds / 1000;
		milliseconds -= seconds * 1000;

		GetNode("victory_screen_holder").GetNode("victory_screen").GetNode("Timer_back").GetNode<Godot.Label>("Label").Text = 
		"Time: " + (minutes < 10 ? "0" : "" ) + minutes
		+ ":" + (seconds < 10 ? "0" : "" ) + seconds
		+ "." + (milliseconds < 100 ? "0" : "" ) + (milliseconds < 10 ? "0" : "" ) + milliseconds;
	}

	public void back_to_menu(){
		((music_player)GetNode("/root/MusicPlayer")).restart_sound_level();
		GetTree().Root.AddChild(ResourceLoader.Load<PackedScene>("res://prefabs/game/menu.tscn").Instantiate());
		GetParent().QueueFree();
	}
}
