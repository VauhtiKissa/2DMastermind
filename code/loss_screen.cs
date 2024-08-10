using Godot;
using System;

public partial class loss_screen : Node2D
{
	public override void _Ready()
	{
        GetNode<button_sound>("/root/ButtonSoundMaker").connect_button(GetNode<TextureButton>("./restart"), false);
        GetNode<button_sound>("/root/ButtonSoundMaker").connect_button(GetNode<TextureButton>("./menu"), false);

		GetNode<AnimationPlayer>("./AnimationPlayer").Play("loss_pop_up");
		GetNode<AnimationPlayer>("./correct_answer_display/AnimationPlayer").Play("correct_answer_display_slide");

		Node2D correct_answer_sprites = GetNode<Node2D>("./correct_answer_display/Sprites");

		for (int i = 0; i < correct_answer_sprites.GetChildCount()-1; i++)
		{
			correct_answer_sprites.GetChild<Sprite2D>(i).Texture = GD.Load<Texture2D>(Color_values.color_sprites[(int)Game_coordinator.correct_answer[i]]);
		}
	}

	public void back_to_menu(){
		GetNode<music_player>("/root/MusicPlayer").restart_sound_level();
		GetTree().Root.AddChild(ResourceLoader.Load<PackedScene>("res://prefabs/game/menu.tscn").Instantiate());
		GetParent().GetParent().QueueFree();
	}

	public void restart(){
		((music_player)GetNode("/root/MusicPlayer")).restart_sound_level();
		GetTree().Root.AddChild(ResourceLoader.Load<PackedScene>("res://prefabs/game/game.tscn").Instantiate());
		GetParent().GetParent().QueueFree();
	}
}
