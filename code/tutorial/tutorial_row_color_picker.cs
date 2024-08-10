using Godot;
using System;

public partial class tutorial_row_color_picker : Node2D
{
	private tutorial_guess_row parent;
	private Node buttons;
	public override void _Ready()
	{
		buttons = GetNode<Node2D>("./buttons");
		for (int i = 0 ; i < buttons.GetChildCount(); i++){
        	GetNode<button_sound>("/root/ButtonSoundMaker").connect_button((TextureButton)buttons.GetChild(i), false);
			buttons.GetChild<TextureButton>(i).TextureNormal = GD.Load<Texture2D>(Color_values.color_sprites[i]);
			buttons.GetChild<TextureButton>(i).TexturePressed = GD.Load<Texture2D>(Color_values.color_sprites_pressed[i]);
			buttons.GetChild<TextureButton>(i).TextureHover = GD.Load<Texture2D>(Color_values.color_sprites_pressed[i]);
		}
		
		parent = GetParent<tutorial_guess_row>();
	}

	public void pick_color(int given_color){
		parent.current_color = (GameColors)given_color;
	}
	
	public void chech_button_pressed(){
		parent.round_end();
	}
}