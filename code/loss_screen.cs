using Godot;
using System;

public partial class loss_screen : Node2D
{
	public override void _Ready()
	{
        GetNode<SoundHandler>("/root/SoundHandler").connectButton(GetNode<TextureButton>("./Restart"), false);
        GetNode<SoundHandler>("/root/SoundHandler").connectButton(GetNode<TextureButton>("./Menu"), false);

		GetNode<AnimationPlayer>("./AnimationPlayer").Play("loss_pop_up");
		GetNode<AnimationPlayer>("./CorrectAnswerDisplay/AnimationPlayer").Play("correct_answer_display_slide");

		Node2D correct_answer_sprites = GetNode<Node2D>("./CorrectAnswerDisplay/Sprites");

		for (int i = 0; i < correct_answer_sprites.GetChildCount()-1; i++)
		{
			correct_answer_sprites.GetChild<Sprite2D>(i).Texture = GD.Load<Texture2D>(Color_values.color_sprites[(int)Game_coordinator.correct_answer[i]]);
		}
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
