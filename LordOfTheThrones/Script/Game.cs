using Godot;
using System;

public partial class Game : Node
{
	private Combat _combat;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_combat = GetNode<Combat>("Combat");
		GD.Print("Type of _combat:", _combat.GetType().ToString());


		PlayerTurn();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void PlayerTurn() 
	{ 
		_combat.PlayerTurn();

		if (_combat.IsBattleOver())
		{
			_combat.EndBattle();
		}
		else
		{
			EnemyTurn();
		}
	}

	private void EnemyTurn() 
	{
		_combat.EnemyTurn();
		if (_combat.IsBattleOver())
		{
			_combat.EndBattle();
		}
		else
		{
			PlayerTurn();
		}
	}
}
