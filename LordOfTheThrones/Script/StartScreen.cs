using Godot;
using System;

public partial class StartScreen : Node
{
    public static string playerName;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        GetNode<Button>("CenterContainer/VBoxContainer/Play").Pressed +=
            () => GetNode<SceneLoader>("/root/SceneLoader").ChangeToScene("Scenes/game.tscn");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public void ExitGame()
    {
        GetTree().Quit();
    }

    public void SavePlayerName()
    {
        GD.Print("SavePlayerName is running");
        playerName = GetNode<LineEdit>("Panel/HBoxContainer/LineEdit").Text;
    }
}
