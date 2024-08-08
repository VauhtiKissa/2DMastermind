using Godot;
using System;

public partial class menu : Node2D
{

	public Node normal_game;
	public Node tutorial_game;
	public Node options;

	public override void _Ready()
	{
		normal_game = ResourceLoader.Load<PackedScene>("res://prefabs/game/game.tscn").Instantiate();
		tutorial_game = ResourceLoader.Load<PackedScene>("res://prefabs/tutorial/tutorial.tscn").Instantiate();

		((button_sound)GetNode("/root/ButtonSoundMaker")).connect_button(GetNode<TextureButton>("./start"), true);
		((button_sound)GetNode("/root/ButtonSoundMaker")).connect_button(GetNode<TextureButton>("./tutorial"), true);
		((button_sound)GetNode("/root/ButtonSoundMaker")).connect_button(GetNode<TextureButton>("./quit"), true);

		if(config_manager.config.did_tutorial == false){
			((TextureButton)GetNode("./start")).Position = new Vector2(0,1000);
			((TextureButton)GetNode("./tutorial")).Position = new Vector2(360,160);
			((TextureButton)GetNode("./quit")).Position = new Vector2(360,248);
		}

	}

	public void play_pressed(){
		GetTree().Root.AddChild(normal_game);
		GetParent().QueueFree();
	}

	public void tutorial_pressed(){
		GetTree().Root.AddChild(tutorial_game);
		GetParent().QueueFree();
	}

	public void quit_pressed(){
		GetTree().Quit();
	}

}
