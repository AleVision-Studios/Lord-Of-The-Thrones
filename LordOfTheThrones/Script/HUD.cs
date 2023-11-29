using Godot;
using System;

public partial class HUD : CanvasLayer
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void OnAttackPressed()
	{
		Player player = GetPlayerNode();
		Enemy enemy = GetNecromancerNode();

		if (player != null && enemy != null)
		{
			player.Attack(enemy);
		}
		else
		{
			GD.Print("Player or Necromancer not found. Check node names and hierarchy.");
		}
	}

	private Player GetPlayerNode()
	{
		Node playerNode = GetNodeOrNull<Player>("Player");
		if (playerNode is Player player)
		{
			return player;
		}
		else
		{
			GD.Print("Player node not found or is not of type Player.");
			return null;
		}
	}

	private Enemy GetNecromancerNode()
	{
		Node necromancerNode = GetNodeOrNull<Enemy>("Necromancer");
		if (necromancerNode is Enemy necromancer)
		{
			return necromancer;
		}
		else
		{
			GD.Print("Necromancer node not found or is not of type Enemy.");
			return null;
		}
	}
}