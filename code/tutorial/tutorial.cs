 using Godot;
using System;
using System.Linq;

public partial class tutorial : Node2D
{
	[Export]
	private Node2D[] Pages;
	private int current_page = 0;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

		((button_sound)GetNode("/root/ButtonSoundMaker")).connect_button((TextureButton)GetChild(0), true);
		((button_sound)GetNode("/root/ButtonSoundMaker")).connect_button((TextureButton)GetChild(1), true);
		config_manager.config.did_tutorial = true;
		config_manager.save();
	}

	public void change_page_forward(){
		current_page += 1;
		if(current_page <= Pages.Length-1){
			Pages[current_page].Position = new Vector2(0,0);
			Pages[current_page-1].Position = new Vector2(0,1000);
		}else{
			current_page = 0;
			Pages[current_page].Position = new Vector2(0,0);
			Pages[Pages.Length-1].Position = new Vector2(0,1000);
		}
	}

	public void change_page_backward(){
		current_page -= 1;
		if(current_page >= 0){
			Pages[current_page].Position = new Vector2(0,0);
			Pages[current_page+1].Position = new Vector2(0,1000);
		}else{
			current_page = Pages.Length-1;
			Pages[current_page].Position = new Vector2(0,0);
			Pages[0].Position = new Vector2(0,1000);
		}
	}


	public void back_to_menu(){
		GetTree().Root.AddChild(ResourceLoader.Load<PackedScene>("res://prefabs/game/menu.tscn").Instantiate());
		QueueFree();
	}
}
