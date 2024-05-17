using Godot;
using System;

public partial class tutorial : Node2D
{
	private bool page = false;
	private Vector2 hidden_position = new Vector2(-1000,0);
	private Vector2 visible_position = new Vector2(320,150);

	private tutorial_guess_row page1;
	private tutorial_guess_cube page2;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		page1 = (tutorial_guess_row)GetChild(0);
		page2 = (tutorial_guess_cube)GetChild(1);
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
