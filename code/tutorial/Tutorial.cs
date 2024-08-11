using Godot;
using System;
using System.Linq;

public partial class Tutorial : Node2D
{
	[Export]
	private Node2D[] Pages;
	private int current_page = 0;
	public override void _Ready()
	{

		Pages = GetNode<Node2D>("./Pages").GetChildren().Cast<Node2D>().ToArray();

		GetNode<SoundHandler>("/root/SoundHandler").connectButton(GetNode<TextureButton>("./LastPage"), true);
		GetNode<SoundHandler>("/root/SoundHandler").connectButton(GetNode<TextureButton>("./NextPage"), true);
		GetNode<SoundHandler>("/root/SoundHandler").connectButton(GetNode<TextureButton>("./BackToMenu"), true);

		ConfigManager.config.did_tutorial = true;
		ConfigManager.save();
	}

	public void changePageForward(){
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

	public void changePageBackward(){
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


	public void backToMenu(){
		GetTree().Root.AddChild(ResourceLoader.Load<PackedScene>("res://prefabs/game/Menu.tscn").Instantiate());
		QueueFree();
	}
}
