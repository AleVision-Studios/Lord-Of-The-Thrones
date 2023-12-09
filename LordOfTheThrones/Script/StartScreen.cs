using Godot;
using LordOfTheThrones;
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
        string inputName = GetNode<LineEdit>("Panel/HBoxContainer/LineEdit").Text;
        var saveButton = GetNode<Button>("Panel/HBoxContainer/Save");
        
        if (Helpers.NameChecker(inputName))
        {
            playerName = inputName.ToUpper();
            saveButton.Text = "OK!";
		}
        else
        {
            playerName = "PLAYER";
            saveButton.Text = "Please change name.";
        }

        
    }
}
