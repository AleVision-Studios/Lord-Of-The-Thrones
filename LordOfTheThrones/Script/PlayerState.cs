using Godot;
using System;

public partial class PlayerState : Node
{
	//This is the players stats, they are used as a global variable in the project
	public int CurrentHealth = 100;
	public int MaxHealth = 100;
	public int Damage = 10;
	public static int TotalGold = 0;
	public string PlayerName = "Player";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}



}
