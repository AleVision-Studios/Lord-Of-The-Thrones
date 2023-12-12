using Godot;
using LordOfTheThrones;
using System;

public partial class StartScreen : Node
{
    public static string playerName = "PLAYER";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        GetNode<Button>("CenterContainer/VBoxContainer/Play").Pressed +=
            () => GetNode<SceneLoader>("/root/SceneLoader").ChangeToScene("Scenes/game.tscn");
    }

    public void ExitGame()
    {
        GetTree().Quit();
    }

    //This is how we Save the player name so it displays in the combat hud.
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
