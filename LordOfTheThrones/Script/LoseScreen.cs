using Godot;
using System;

public partial class LoseScreen : CanvasLayer
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	//This resets the game and you begin from level 1 with all stats reset.
	public void OnTryAgainPressed()
	{
		GetNode<SceneLoader>("/root/SceneLoader").ChangeToScene("Scenes/game.tscn");
	}

	//This method turns off the whole game
	public void OnGiveUpPressed()
	{
		GetTree().Quit();
	}
}
