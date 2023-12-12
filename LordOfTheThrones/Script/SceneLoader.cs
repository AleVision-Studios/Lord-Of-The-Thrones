using Godot;
using System;

public partial class SceneLoader : Node
{
    [Export] private string _sceneFolder;

    //Method to change to a different scene.
    public void ChangeToScene(string sceneName)
    {
        string f = _sceneFolder == "" ? "" : $"{_sceneFolder}/";
        GetTree().ChangeSceneToFile($"res://{f}{sceneName}");
    }
}
