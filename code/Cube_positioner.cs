using Godot;
using System;

public partial class Cube_positioner : Node
{

	private Vector2 default_position = new Vector2(5000,0);
	private Vector2 default_scale = new Vector2(1,1);
	private Vector2[][] positions = {
		new[]{new Vector2(224,60)},
		new[]{new Vector2(72,72), new Vector2(500,60)}
	};
	private Vector2[] scales = {
		new Vector2(0.8f,0.8f),
		new Vector2(0.6f,0.6f)
		
	};

	[Export]
	public int round = 0;

	Node2D[] cubes;

	public override void _Ready()
	{

		cubes = new Node2D[8];
		for (int i = 0; i < 8; i++)
		{
			cubes[i] = (Node2D)GetChild(i);
		}
		if(round > 0){
			Put_cubes_in_position(round);
		}

	}

	public void Put_cubes_in_position(int round_number){

		for (int i = 0; i < 8; i++){

			if(i < round_number){
				cubes[i].GlobalPosition = positions[round_number-1][i];
				cubes[i].Scale = scales[round_number-1];
			}else{
				break;
			}

		}
	}

}