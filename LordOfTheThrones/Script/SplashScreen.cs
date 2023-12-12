using Godot;
using System;

public partial class SplashScreen : ColorRect
{
	//This our first loading screen when booting up the game.
	//It shows logo and names for 4 (Currently) seconds and then switches to the main screen.
	public override void _Ready()
	{
		GetNode<Timer>("Timer").Timeout +=
			() => GetNode<SceneLoader>("/root/SceneLoader").ChangeToScene("Scenes/start_screen.tscn");
	}	
}
