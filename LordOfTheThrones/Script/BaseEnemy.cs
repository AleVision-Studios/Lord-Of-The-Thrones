using Godot;
using System;


public partial class BaseEnemy : Resource
{
    //This is our default Enemy, if an enemy doesnt have any their variables are set to null this is what they get.
    //Good if you just want to test an animation or something.
    [Export]
    public string Name = "Enemy";

    [Export]
    public Texture Texture = null;

    [Export]
    public int Health = 80;

    [Export]
    public int Damage = 5;
}
