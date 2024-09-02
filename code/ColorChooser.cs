using System;
using System.Linq;
using Godot;

public partial class ColorChooser : Node2D
{
    private GuessCube parent;
    private TextureButton[] buttons;
    private bool[] crossedToggle = new bool[8];
    private AnimationPlayer animator;

    public override void _Ready()
    {
        parent = (GuessCube)GetParent().GetParent();

        animator = GetNodeOrNull<AnimationPlayer>("./AnimationPlayer");

        buttons = GetNode<Node2D>("./AnimationPositioner/Buttons").GetChildren().Cast<TextureButton>().ToArray();

        for (int i = 0; i < buttons.Length - 1; i++)
        {
            buttons[i].TextureNormal = GD.Load<Texture2D>(Color_values.colorSprites[i]);
            buttons[i].TexturePressed = GD.Load<Texture2D>(Color_values.colorSpritesPressed[i]);
            buttons[i].TextureHover = GD.Load<Texture2D>(Color_values.colorSpritesPressed[i]);
            GetNode<SoundHandler>("/root/SoundHandler").connectButton(buttons[i], false);
        }

        GetNode<SoundHandler>("/root/SoundHandler").connectButton(buttons[8], true);
    }

    public void pickColor(int given_color)
    {
        if (Input.IsActionPressed("left_click"))
        {
            parent.chosenColor = (GameColors)given_color;
            crossedToggle[given_color] = false;
            buttons[given_color].TextureNormal = GD.Load<Texture2D>(Color_values.colorSprites[given_color]);
            buttons[given_color].TexturePressed = GD.Load<Texture2D>(Color_values.colorSpritesPressed[given_color]);
            buttons[given_color].TextureHover = GD.Load<Texture2D>(Color_values.colorSpritesPressed[given_color]);
        }
        else if (crossedToggle[given_color])
        {
            crossedToggle[given_color] = !crossedToggle[given_color];
            buttons[given_color].TextureNormal = GD.Load<Texture2D>(Color_values.colorSprites[given_color]);
            buttons[given_color].TexturePressed = GD.Load<Texture2D>(Color_values.colorSpritesPressed[given_color]);
            buttons[given_color].TextureHover = GD.Load<Texture2D>(Color_values.colorSpritesPressed[given_color]);
        }
        else
        {
            crossedToggle[given_color] = !crossedToggle[given_color];
            buttons[given_color].TextureNormal = GD.Load<Texture2D>(Color_values.colorSpritesCrossed[given_color]);
            buttons[given_color].TexturePressed = GD.Load<Texture2D>(Color_values.colorSpritesCrossedPressed[given_color]);
            buttons[given_color].TextureHover = GD.Load<Texture2D>(Color_values.colorSpritesCrossedPressed[given_color]);
        }
    }

    public void roundEndButtonPressed()
    {
        if (!parent.tutorial)
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].Disabled = true;
            }
            animator?.PlayBackwards("ColorChooserSlide");
        }
        parent.round_end();
    }

    public void activate()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].Disabled = false;
        }
        animator?.Play("ColorChooserSlide");
    }
}
