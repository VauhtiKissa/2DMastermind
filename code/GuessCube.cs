using System;
using System.Collections.Generic;
using System.Linq;
using Godot;

public partial class GuessCube : Node
{
    public GameColors chosenColor;
    private GameColors[] currentColors;
    private TextureButton[] buttons;
    private Label[] correctAnswerNumbers;
    private Label[] wrongPlaceNumbers;
    private ColorChooser colorChooser;

    [Export]
    public bool tutorial = false;

    [Export]
    public bool sliding = false;

    public override void _Ready()
    {
        colorChooser = GetNode<ColorChooser>("./AnimationPositioner/ColorChooser");

        buttons = GetNode<Node2D>("./AnimationPositioner/Buttons").GetChildren().Cast<TextureButton>().ToArray();
        correctAnswerNumbers = GetNode<Node2D>("./AnimationPositioner/CorrectAnswerNumbers").GetChildren().Cast<Label>().ToArray();
        wrongPlaceNumbers = GetNode<Node2D>("./AnimationPositioner/WrongPlaceNumbers").GetChildren().Cast<Label>().ToArray();

        currentColors = new GameColors[16];

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].TextureNormal = GD.Load<Texture2D>(Color_values.colorSprites[0]);
            buttons[i].TexturePressed = GD.Load<Texture2D>(Color_values.colorSpritesPressed[0]);
            buttons[i].TextureHover = GD.Load<Texture2D>(Color_values.colorSpritesPressed[0]);
            GetNode<SoundHandler>("/root/SoundHandler").connectButton(buttons[i], false);

            currentColors[i] = GameColors.blank;
        }

        if (tutorial)
        {
            GameCoordinator.generate_answer();
            activate();
        }
    }

    public void button_pressed(int button_index)
    {
        currentColors[button_index] = Input.IsActionPressed("left_click")
            ? chosenColor
            : GameColors.blank;

        int color_index = (int)currentColors[button_index];

        buttons[button_index].TextureNormal = GD.Load<Texture2D>(Color_values.colorSprites[color_index]);
        buttons[button_index].TexturePressed = GD.Load<Texture2D>(Color_values.colorSpritesPressed[color_index]);
        buttons[button_index].TextureHover = GD.Load<Texture2D>(Color_values.colorSpritesPressed[color_index]);
    }

    public void round_end()
    {
        if (!tutorial)
        {
            GetNode<GameCoordinator>("../../").submitAnswer(check_answer());
            disable();
        }
        else
        {
            check_answer();
        }
    }

    public bool check_answer()
    {
        int correctAnswers = 0;

        for (int i = 0; i < 4; i++)
        {
            int rowCorrectAnswers = 0;
            int rowWrongPositionAnswers = 0;
            int columnCorrectAnswers = 0;
            int columnWrongPositionAnswers = 0;

            List<GameColors> rowIncorrectlyGuessedCorrectColors = new List<GameColors>();
            List<GameColors> rowIncorrectlyGuessedColors = new List<GameColors>();
            List<GameColors> columnIncorrectlyGuessedCorrectColors = new List<GameColors>();
            List<GameColors> columnIncorrectlyGuessedColors = new List<GameColors>();

            for (int e = 0; e < 4; e++)
            {
                // check row
                if (currentColors[i * 4 + e] == GameCoordinator.correctAnswer[i * 4 + e])
                {
                    correctAnswers += 1;
                    rowCorrectAnswers += 1;
                }
                else
                {
                    rowIncorrectlyGuessedCorrectColors.Add(
                        GameCoordinator.correctAnswer[i * 4 + e]
                    );
                    rowIncorrectlyGuessedColors.Add(currentColors[i * 4 + e]);
                }
                // check column
                if (currentColors[i + e * 4] == GameCoordinator.correctAnswer[i + e * 4])
                {
                    columnCorrectAnswers += 1;
                }
                else
                {
                    columnIncorrectlyGuessedCorrectColors.Add(GameCoordinator.correctAnswer[i + e * 4]);
                    columnIncorrectlyGuessedColors.Add(currentColors[i + e * 4]);
                }
            }
            foreach (GameColors color in rowIncorrectlyGuessedColors)
            {
                if (rowIncorrectlyGuessedCorrectColors.Contains(color))
                {
                    rowWrongPositionAnswers += 1;
                    rowIncorrectlyGuessedCorrectColors.Remove(color);
                }
            }
            foreach (GameColors color in columnIncorrectlyGuessedColors)
            {
                if (columnIncorrectlyGuessedCorrectColors.Contains(color))
                {
                    columnWrongPositionAnswers += 1;
                    columnIncorrectlyGuessedCorrectColors.Remove(color);
                }
            }
            correctAnswerNumbers[i].Text = rowCorrectAnswers.ToString();
            correctAnswerNumbers[i + 4].Text = columnCorrectAnswers.ToString();
            wrongPlaceNumbers[i].Text = rowWrongPositionAnswers.ToString();
            wrongPlaceNumbers[i + 4].Text = columnWrongPositionAnswers.ToString();
        }
        return correctAnswers == 16;
    }

    public void activate()
    {
        if (sliding)
        {
            GetNode<AnimationPlayer>("./AnimationPlayer").Play("GuessCubeSlide");
        }

        for (int i = 0; i < 16; i++)
        {
            buttons[i].Disabled = false;
            buttons[i].TextureNormal = GD.Load<Texture2D>(Color_values.colorSprites[(int)GameColors.blank]);
            buttons[i].TexturePressed = GD.Load<Texture2D>(Color_values.colorSpritesPressed[(int)GameColors.blank]);
            buttons[i].TextureHover = GD.Load<Texture2D>(Color_values.colorSpritesPressed[(int)GameColors.blank]);
        }
        colorChooser.activate();
    }

    public void disable()
    {
        if (sliding)
        {
            GetNode<AnimationPlayer>("./AnimationPlayer").PlayBackwards("GuessCubeSlide");
        }
        for (int i = 0; i < 16; i++)
        {
            buttons[i].Disabled = true;
        }
    }
}
