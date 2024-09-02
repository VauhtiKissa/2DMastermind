using System;
using Godot;

public partial class Menu : Node2D
{
    public Node normalGame;
    public Node tutorialGame;
    public Node options;

    public override void _Ready()
    {
        normalGame = ResourceLoader.Load<PackedScene>("res://prefabs/game/Game.tscn").Instantiate();
        tutorialGame = ResourceLoader.Load<PackedScene>("res://prefabs/tutorial/Tutorial.tscn").Instantiate();

        GetNode<SoundHandler>("/root/SoundHandler").connectButton(GetNode<TextureButton>("./Start"), true);
        GetNode<SoundHandler>("/root/SoundHandler").connectButton(GetNode<TextureButton>("./Tutorial"), true);
        GetNode<SoundHandler>("/root/SoundHandler").connectButton(GetNode<TextureButton>("./Quit"), true);

        if (ConfigManager.config.didTutorial == false)
        {
            GetNode<TextureButton>("./Start").Position = new Vector2(0, 1000);
            GetNode<TextureButton>("./Tutorial").Position = new Vector2(360, 160);
            GetNode<TextureButton>("./Quit").Position = new Vector2(360, 248);
        }
    }

    public void play_pressed()
    {
        GetTree().Root.AddChild(normalGame);
        GetParent().QueueFree();
    }

    public void tutorial_pressed()
    {
        GetTree().Root.AddChild(tutorialGame);
        GetParent().QueueFree();
    }

    public void quit_pressed()
    {
        GetTree().Quit();
    }
}