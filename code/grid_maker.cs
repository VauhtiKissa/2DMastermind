using Godot;
using System;

public partial class grid_maker : Node2D
{

	private TileMap tile_map;

	// Called when the node enters the scene tree for the first time.
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

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
