using Godot;
using System;

public partial class tutorial : Node2D
{
	private bool page = false;
	private Vector2 hidden_position = new Vector2(-1000,0);
	private Vector2 visible_position = new Vector2(320,150);

	private tutorial_guess_row page1;
	private tutorial_guess_cube page2;

	private TextureButton[] buttons;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		((button_sound)GetTree().Root.GetChild(1)).connect_button((TextureButton)GetChild(3), true);
		((button_sound)GetTree().Root.GetChild(1)).connect_button((TextureButton)GetChild(4), true);

		page1 = (tutorial_guess_row)GetChild(1);
		page2 = (tutorial_guess_cube)GetChild(2);
	}


	public void change_page(){
		if(page == true){
			page1.Position = visible_position;
			page2.Position = hidden_position;
		}else{
			page1.Position = hidden_position;
			page2.Position = visible_position;
		}
		page = !page;
	}

	public void back_to_menu(){
		GetTree().Root.AddChild(ResourceLoader.Load<PackedScene>("res://prefabs/game/menu.tscn").Instantiate());
		QueueFree();
	}
}
