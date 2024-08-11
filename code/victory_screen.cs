using Godot;
using System;

public partial class victory_screen : Node2D
{
	[Export]
	public bool victory = false;
	public override void _Ready()
	{
        GetNode<SoundHandler>("/root/SoundHandler").connectButton(GetNode<TextureButton>("./Restart"), false);
        GetNode<SoundHandler>("/root/SoundHandler").connectButton(GetNode<TextureButton>("./Menu"), false);

		AnimationPlayer animationPlayer = GetNode<AnimationPlayer>("./");

		animationPlayer.Play("victory_animation");
	}

	public void back_to_menu(){
		GetNode<SoundHandler>("/root/SoundHandler").restartSoundLevel();
		GetTree().Root.AddChild(ResourceLoader.Load<PackedScene>("res://prefabs/game/menu.tscn").Instantiate());
		GetNode<Node>("/root/Game").QueueFree();
	}

	public void restart(){
		GetNode<SoundHandler>("/root/SoundHandler").restartSoundLevel();
		GetTree().Root.AddChild(ResourceLoader.Load<PackedScene>("res://prefabs/game/game.tscn").Instantiate());
		GetNode<Node>("/root/Game").QueueFree();
	}
}
