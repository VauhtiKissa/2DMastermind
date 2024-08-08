using Godot;
using System;
using System.Data;

public partial class grid_maker : Node2D
{
	/*
		makes the background for the main menu
	*/
	private double circle_poss;
	[Export]
	private float circle_distance = 5;
	[Export]
	private float circle_speed = 1;

	private Vector2I[] tile_positions = 
	{ 
		new Vector2I(0,0), new Vector2I(1,0), new Vector2I(2,0),
		new Vector2I(0,1), new Vector2I(1,1), new Vector2I(2,1),
		new Vector2I(0,2), new Vector2I(1,2)
	};
	
	private TileMap tile_map;
	public override void _Ready()
	{
		tile_map = GetNode<TileMap>("./TileMap");

		for (int i = 0; i < 50; i++)
		{
			for (int e = 0; e < 50; e++)
			{
				tile_map.SetCell(0,new Vector2I(i-20,e-20),0,tile_positions[GD.RandRange(0,7)]);
			}
		}
	}

	public override void _Process(double delta)
	{
		circle_poss = (circle_poss + delta * circle_speed)%(Math.PI*2);
		tile_map.Position = new Vector2((float)Math.Sin(circle_poss),(float)Math.Cos(circle_poss)) * circle_distance;
	}
}
