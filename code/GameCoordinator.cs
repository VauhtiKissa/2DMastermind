using Godot;
using System;
using System.Linq;

public partial class GameCoordinator : Node2D
{
	private int round_number = 0;
	public static GameColors[] correct_answer;
	private GuessCube[] guess_cubes;
	private double time = 0;
	private SoundHandler sound_handler;

	public override void _Ready()
	{
		sound_handler = GetNode<SoundHandler>("/root/SoundHandler");
		guess_cubes = GetNode<Node2D>("./GuessCubes").GetChildren().Cast<GuessCube>().ToArray();
		generate_answer();
		start_round();
		sound_handler.connectButton(GetNode<TextureButton>("./TextureButton"), true);
	}

	public override void _Process(double delta)
	{
		time += delta;
	}

	public static void generate_answer(){
		correct_answer = new GameColors[16];
		for (int i = 0 ; i < 16 ; i++){
			correct_answer[i] = (GameColors)GD.RandRange(0,7);
		}
	}

	public void submitAnswer(bool correct){
		if(correct){
			victory();
		}else{
			start_round();
		}
	}
	public void start_round(){
		if(round_number < 8){

			sound_handler.roundUp(round_number);

			guess_cubes[round_number].activate();
			round_number += 1;
		}else{
			AddChild(ResourceLoader.Load<PackedScene>("res://prefabs/game/LossScreen.tscn").Instantiate());
		}
	}

	public void victory(){
		AddChild(ResourceLoader.Load<PackedScene>("res://prefabs/game/ConfettiCannon.tscn").Instantiate());
		AddChild(ResourceLoader.Load<PackedScene>("res://prefabs/game/victory_screen.tscn").Instantiate());
		int milliseconds = (int)(time*1000);
		int minutes = milliseconds / 60000;
		milliseconds -= minutes * 60000;
		int seconds = milliseconds / 1000;
		milliseconds -= seconds * 1000;

		GetNode<Label>("./VictoryScreen/AnimationPositioner/TimerBack/Label").Text = 
		"Time: " + (minutes < 10 ? "0" : "" ) + minutes
		+ ":" + (seconds < 10 ? "0" : "" ) + seconds
		+ "." + (milliseconds < 100 ? "0" : "" ) 
		+ (milliseconds < 10 ? "0" : "" ) + milliseconds;
	}

	public void back_to_menu(){
		sound_handler.restartSoundLevel();
		GetTree().Root.AddChild(ResourceLoader.Load<PackedScene>("res://prefabs/game/Menu.tscn").Instantiate());
		GetParent().QueueFree();
	}
}
