using Godot;
using System;
using System.Linq;

public partial class Tutorial : Node2D
{
	[Export]
	private Node2D[] pages;
	private int currentPage = 0;
	public override void _Ready()
	{

		pages = GetNode<Node2D>("./Pages").GetChildren().Cast<Node2D>().ToArray();

		GetNode<SoundHandler>("/root/SoundHandler").connectButton(GetNode<TextureButton>("./LastPage"), true);
		GetNode<SoundHandler>("/root/SoundHandler").connectButton(GetNode<TextureButton>("./NextPage"), true);
		GetNode<SoundHandler>("/root/SoundHandler").connectButton(GetNode<TextureButton>("./BackToMenu"), true);

		ConfigManager.config.didTutorial = true;
		ConfigManager.save();
	}

	public void changePageForward(){
		currentPage += 1;
		if(currentPage <= pages.Length-1){
			pages[currentPage].Position = new Vector2(0,0);
			pages[currentPage-1].Position = new Vector2(0,1000);
		}else{
			currentPage = 0;
			pages[currentPage].Position = new Vector2(0,0);
			pages[pages.Length-1].Position = new Vector2(0,1000);
		}
	}

	public void changePageBackward(){
		currentPage -= 1;
		if(currentPage >= 0){
			pages[currentPage].Position = new Vector2(0,0);
			pages[currentPage+1].Position = new Vector2(0,1000);
		}else{
			currentPage = pages.Length-1;
			pages[currentPage].Position = new Vector2(0,0);
			pages[0].Position = new Vector2(0,1000);
		}
	}


	public void backToMenu(){
		GetTree().Root.AddChild(ResourceLoader.Load<PackedScene>("res://prefabs/game/Menu.tscn").Instantiate());
		QueueFree();
	}
}
