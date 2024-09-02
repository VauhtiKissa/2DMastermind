using Godot;
using System;
using System.Data;

public partial class GridMaker : Node2D
{
	/*
		makes the background for the main menu
	*/
	private double circlePoss;
	[Export]
	private float circleDistance = 750;
	[Export]
	private float circleSpeed = 0.25f;

	private Vector2I[] tilePositions = 
	{ 
		new Vector2I(0,0), new Vector2I(1,0), new Vector2I(2,0),
		new Vector2I(0,1), new Vector2I(1,1), new Vector2I(2,1),
		new Vector2I(0,2), new Vector2I(1,2)
	};
	
	private TileMap tileMap;
	public override void _Ready()
	{
		tileMap = GetNode<TileMap>("./TileMap");

		for (int i = 0; i < 50; i++)
		{
			for (int e = 0; e < 50; e++)
			{
				tileMap.SetCell(0,new Vector2I(i-20,e-20),0,tilePositions[GD.RandRange(0,7)]);
			}
		}
	}

	public override void _Process(double delta)
	{
		circlePoss = (circlePoss + delta * circleSpeed)%(Math.PI*2);
		tileMap.Position = new Vector2((float)Math.Sin(circlePoss),(float)Math.Cos(circlePoss)) * circleDistance;
	}
}
