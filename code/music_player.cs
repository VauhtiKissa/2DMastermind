using Godot;
using System;

public partial class music_player : Node
{
	private Node[] sources;

	public override void _Ready()
	{
		sources = new Node[8];
		for (int i = 0; i < GetChildCount(); i++)
		{
			sources[i] = GetChild(i);	
		}
	}

	public void round_changed(int number){
		((Godot.AudioStreamPlayer)sources[number]).VolumeDb = 0;
	}
}