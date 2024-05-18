using Godot;
using System;

public partial class AudioStreamPlayer : Godot.AudioStreamPlayer
{

	string[] music_parts ={
		"res://sounds/bitchin 1.wav",
		"res://sounds/bitchin 2.wav",
		"res://sounds/bitchin 3.wav",
		"res://sounds/bitchin 4.wav",
		"res://sounds/bitchin 5.wav",
		"res://sounds/bitchin 6.wav",
		"res://sounds/bitchin 7.wav",
		"res://sounds/bitchin 8.wav"
	};

	AudioStreamPolyphonic audio_stream;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		audio_stream = (AudioStreamPolyphonic)Stream;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
