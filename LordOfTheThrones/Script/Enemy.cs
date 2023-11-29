using Godot;
using System;

public partial class Enemy : Area2D
{

	private int maxHP = 50;
	private int currentHP = 50;
	private int attackPower = 5;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	public int GetCurrentHP()
	{
		return currentHP;
	}

	public void TakeDamage(int damage)
	{
		currentHP -= damage;

		if (currentHP <= 0)
		{
			currentHP = 0;
			GD.Print("u win m8");
		}

		GD.Print("Enemy HP :" + currentHP);
	}

	public void Attack(Player player) 
	{
		int damage = attackPower;
		player.TakeDamage(damage);
		GD.Print("Enemy attacks for " + damage + " damage!");
	}
}
