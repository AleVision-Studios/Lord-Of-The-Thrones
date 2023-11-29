using Godot;
using System;

public partial class Player : Area2D
{

	private int maxHP = 100;
	private int currentHP = 100;
	private int attackPower = 10;

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
			GD.Print("u ded m8");
		}

		GD.Print("Player HP :" +  currentHP);
	}

	public void Attack(Enemy enemy) 
	{
		int damage = attackPower;
		enemy.TakeDamage(damage);
		GD.Print("Player attacks for " + damage + " damage!");
	}
}
