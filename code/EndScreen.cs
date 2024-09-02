using System;
using Godot;

public partial class EndScreen : Node2D
{
    [Export]
    public bool victory = false;

    public override void _Ready()
    {
        GetNode<SoundHandler>("/root/SoundHandler").connectButton(GetNode<TextureButton>("./AnimationPositioner/Restart"), false);
        GetNode<SoundHandler>("/root/SoundHandler").connectButton(GetNode<TextureButton>("./AnimationPositioner/Menu"), false);

        if (victory)
        {
            GetNode<AnimationPlayer>("./AnimationPositioner/AnimationPlayer").Play("victory_animation");
        }
        else
        {
            GetNode<AnimationPlayer>("./AnimationPositioner/AnimationPlayer").Play("loss_pop_up");
            GetNode<AnimationPlayer>("./AnimationPositioner/CorrectAnswerDisplay/AnimationPlayer").Play("correct_answer_display_slide");
            Node2D correctAnswerSprites = GetNode<Node2D>("./AnimationPositioner/CorrectAnswerDisplay/Sprites");
            for (int i = 0; i < correctAnswerSprites.GetChildCount() - 1; i++)
            {
                correctAnswerSprites.GetChild<Sprite2D>(i).Texture = GD.Load<Texture2D>(Color_values.colorSprites[(int)GameCoordinator.correctAnswer[i]]);
            }
        }
    }

    public void backToMenu()
    {
        GetNode<SoundHandler>("/root/SoundHandler").restartSoundLevel();
        GetTree().Root.AddChild(ResourceLoader.Load<PackedScene>("res://prefabs/game/Menu.tscn").Instantiate());
        GetParent().GetParent().QueueFree();
    }

    public void restart()
    {
        GetNode<SoundHandler>("/root/SoundHandler").restartSoundLevel();
        GetTree().Root.AddChild(ResourceLoader.Load<PackedScene>("res://prefabs/game/Game.tscn").Instantiate());
        GetParent().GetParent().QueueFree();
    }
}
