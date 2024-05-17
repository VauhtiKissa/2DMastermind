using Godot;
public enum GameColors {white, black, red, green, blue, yellow, turquoise, purple}

public static class Color_values{
    // same order as GameColors
    public static Color[] Colors = {
        Color.Color8(230, 230, 230),
        Color.Color8(35, 35, 35),
        Color.Color8(217, 26, 61),
        Color.Color8(25, 191, 53),
        Color.Color8(28, 126, 217),
        Color.Color8(214, 179, 0),
        Color.Color8(28, 217, 173),
        Color.Color8(102, 51, 153),
    };

    // paths to button sprites
    
    public static string[] Color_sprites = {
        "res://sprites/button_sprites/0white.png",
        "res://sprites/button_sprites/1black.png",
        "res://sprites/button_sprites/2red.png",
        "res://sprites/button_sprites/3green.png",
        "res://sprites/button_sprites/4blue.png",
        "res://sprites/button_sprites/5yellow.png",
        "res://sprites/button_sprites/6turquoise.png",
        "res://sprites/button_sprites/7purple.png"
    } ;

    // paths to pressed button sprites
    public static string[] Color_sprites_pressed = {
        "res://sprites/button_sprites/0white_pressed.png",
        "res://sprites/button_sprites/1black_pressed.png",
        "res://sprites/button_sprites/2red_pressed.png",
        "res://sprites/button_sprites/3green_pressed.png",
        "res://sprites/button_sprites/4blue_pressed.png",
        "res://sprites/button_sprites/5yellow_pressed.png",
        "res://sprites/button_sprites/6turquoise_pressed.png",
        "res://sprites/button_sprites/7purple_pressed.png"
    } ;
}