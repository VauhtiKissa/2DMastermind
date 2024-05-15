using Godot;
using System;
using System.Linq;
using System.Threading;

public partial class Game_coordinator : Node2D
{
	private int round_number = 0;
	public static GameColors[] correct_answer;
	public static GameColors[] guessed_colors;
	private Guess_cube[] cubes;

	public override void _Ready()
	{
		cubes = new Guess_cube[8];
		for (int i = 0; i < GetChild(0).GetChildren().Count(); i++)
		{
			cubes[i] = (Guess_cube)GetChild(0).GetChildren()[i];
		}
		generate_answer();
		start_round();
    }

	private void generate_answer(){
		correct_answer = new GameColors[16];
		for (int i = 0 ; i < 16 ; i++){
			correct_answer[i] = (GameColors)GD.RandRange(0,7);
		}
	}

	public void start_round(){
		if(round_number < 8){
			cubes[round_number].activate();
			round_number += 1;
		}else{
			GD.Print("you lost womp womp");
		}
	}

	public void victory(){
		GD.Print("Victory");
	}
}
