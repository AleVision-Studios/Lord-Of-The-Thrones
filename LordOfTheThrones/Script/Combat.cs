using Godot;
using System;

public partial class Combat : Node
{
	private Player _player;
	private Enemy _enemy;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_player = GetNode<Player>("Player");
		_enemy = GetNode<Enemy>("Necromancer");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void PlayerTurn()
	{
		_player.Attack(_enemy);
	}

	public void EnemyTurn()
	{
		_enemy.Attack(_player);
	}

	public bool IsBattleOver()
	{
		return _player.GetCurrentHP() <= 0 || _enemy.GetCurrentHP() <= 0;
	}

	public void EndBattle()
	{
        if (_player.GetCurrentHP() <= 0)
        {
			GD.Print("Game over!");
        }
		else
		{
			GD.Print("Victory!");
		}
    }
}
