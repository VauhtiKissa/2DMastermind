using Godot;
using System;
using System.Linq;

public partial class GameCoordinator : Node2D
{
	private int roundNumber = 0;
	public static GameColors[] correctAnswer;
	private GuessCube[] guessCubes;
	private double time = 0;
	private SoundHandler soundHandler;

	public override void _Ready()
	{
		soundHandler = GetNode<SoundHandler>("/root/SoundHandler");
		guessCubes = GetNode<Node2D>("./GuessCubes").GetChildren().Cast<GuessCube>().ToArray();
		generate_answer();
		start_round();
		soundHandler.connectButton(GetNode<TextureButton>("./TextureButton"), true);
	}

	public override void _Process(double delta)
	{
		time += delta;
	}

	public static void generate_answer(){
		correctAnswer = new GameColors[16];
		for (int i = 0 ; i < 16 ; i++){
			correctAnswer[i] = (GameColors)GD.RandRange(0,7);
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
		if(roundNumber < 8){

			soundHandler.roundUp(roundNumber);

			guessCubes[roundNumber].activate();
			roundNumber += 1;
		}else{
			AddChild(ResourceLoader.Load<PackedScene>("res://prefabs/game/LossScreen.tscn").Instantiate());
		}
	}

	public void victory(){
		AddChild(ResourceLoader.Load<PackedScene>("res://prefabs/game/ConfettiCannon.tscn").Instantiate());
		AddChild(ResourceLoader.Load<PackedScene>("res://prefabs/game/VictoryScreen.tscn").Instantiate());
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
		soundHandler.restartSoundLevel();
		GetTree().Root.AddChild(ResourceLoader.Load<PackedScene>("res://prefabs/game/Menu.tscn").Instantiate());
		GetParent().QueueFree();
	}
}
