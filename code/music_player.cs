using Godot;
using System;

public partial class music_player : Node
{
	/*
		a music player that is autoloaded in
	*/
	private Node[] sources;

	public override void _Ready()
	{
		sources = new Node[8];
		for (int i = 0; i < GetChildCount(); i++)
		{
			sources[i] = GetChild(i);	
		}
	}

	public void round_up(int number){
		((Godot.AudioStreamPlayer)sources[number]).VolumeDb = 0;
	}

	public void restart_sound_level(){
		for (int i = 1; i < sources.Length; i++)
		{
			((Godot.AudioStreamPlayer)sources[i]).VolumeDb = -80;
		}
	}
}