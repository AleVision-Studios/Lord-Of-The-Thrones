using Godot;
using System;

public partial class Combat : Node
{
	[Export]
	public BaseEnemy Enemy = null;

	//public BaseEnemy enemy;

	PlayerState _playerState = new PlayerState();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        SetHealth(GetNode<ProgressBar>("EnemyContainer/EnemyHealthBar"), Enemy.Health, Enemy.Health);
        SetHealth(GetNode<ProgressBar>("PlayerStats/PlayerContainer/PlayerHealthBar"), _playerState.currentHealth, _playerState.maxHealth);
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void SetHealth(ProgressBar progressBar, int health, int maxHealth)
	{
		progressBar.Value = health;
		progressBar.MaxValue = maxHealth;
		progressBar.GetNode<Label>("Label").Text = $"HP: {health}/{maxHealth}";
	}
}

