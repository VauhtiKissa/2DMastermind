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


	private TileMap tile_map;
	public override void _Ready()
	{
		tile_map = (TileMap)GetChild(0);

		for (int i = 0; i < 40; i++)
		{
			for (int e = 0; e < 30; e++)
			{
				tile_map.SetCell(0,new Vector2I(i-15,e-11),0,new Vector2I(GD.RandRange(0,1),GD.RandRange(0,3)),0);
			}
		}
	}

	public override void _Process(double delta)
	{
		circle_poss = (circle_poss + delta * circle_speed)%(Math.PI*2);
		tile_map.Position = new Vector2((float)Math.Sin(circle_poss),(float)Math.Cos(circle_poss)) * circle_distance;
	}
}
