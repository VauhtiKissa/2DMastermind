using System;
using System.Text.Json;
using Godot;

public class Config
{
    public bool didTutorial { get; set; } = false;
    public float musicVolume { get; set; } = 25;
    public float buttonVolume { get; set; } = 5;
    public bool Fullscreen { get; set; } = true;
}

public partial class ConfigManager : Node
{
    public static Config config;
    private TextureButton fullscreen_button;

    public override void _Ready()
    {
        if (FileAccess.FileExists("user://2DMastermind.json"))
        {
            FileAccess file = FileAccess.Open("user://2DMastermind.json", FileAccess.ModeFlags.Read);
            config = JsonSerializer.Deserialize<Config>(file.GetAsText());
            file.Close();
        }
        else
        {
            config = new Config();
        }
        fullscreen_button = GetNode<TextureButton>("./TextureButton");
        if (config.Fullscreen)
        {
            GetWindow().Mode = Window.ModeEnum.Fullscreen;
            fullscreen_button.TextureNormal = GD.Load<Texture2D>("res://sprites/other_buttons/fullscreen_off.png");
        }
    }

    public static void save()
    {
        DirAccess.RemoveAbsolute("user://2DMastermind.json");
        FileAccess file = FileAccess.Open("user://2DMastermind.json", FileAccess.ModeFlags.Write);
        file.StoreString(JsonSerializer.Serialize(config));
        file.Close();
    }

    public void change_full_screen()
    {
        config.Fullscreen = !config.Fullscreen;
        if (config.Fullscreen)
        {
            GetWindow().Mode = Window.ModeEnum.Fullscreen;
            fullscreen_button.TextureNormal = GD.Load<Texture2D>("res://sprites/other_buttons/fullscreen_off.png");
        }
        else
        {
            GetWindow().Mode = Window.ModeEnum.Maximized;
            fullscreen_button.TextureNormal = GD.Load<Texture2D>("res://sprites/other_buttons/fullscreen_on.png");
        }
        save();
    }
}
