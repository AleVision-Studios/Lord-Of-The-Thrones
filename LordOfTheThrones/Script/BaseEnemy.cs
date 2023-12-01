using Godot;
using System;

public partial class BaseEnemy : Resource
{
    [Export]
    public string Name = "Enemy";

    [Export]
    public Texture Texture = null;

    [Export]
    public int Health = 80;

    [Export]
    public int Damage = 5;
}
