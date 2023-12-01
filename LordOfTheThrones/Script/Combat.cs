using Godot;
using System;

public partial class Combat : Node
{
	[Export]
	public BaseEnemy Enemy = null;

	//public BaseEnemy enemy;

	PlayerState _playerState = new PlayerState();

	public int currentEnemyHealth = 0;
	public int currentPlayerHealth = 0;



	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        SetHealth(GetNode<ProgressBar>("EnemyContainer/EnemyHealthBar"), Enemy.Health, Enemy.Health);
        SetHealth(GetNode<ProgressBar>("PlayerStats/PlayerContainer/PlayerHealthBar"), _playerState.CurrentHealth, _playerState.MaxHealth);

		currentEnemyHealth = Enemy.Health;
		currentPlayerHealth = _playerState.CurrentHealth;
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
        //var animatedSprite2D = GetNode<AnimatedSprite2D>("Player/AnimatedSprite2D");
        //animatedSprite2D.Play("idle");
    }

	private void SetHealth(ProgressBar progressBar, int health, int maxHealth)
	{
		progressBar.Value = health;
		progressBar.MaxValue = maxHealth;
		progressBar.GetNode<Label>("Label").Text = $"HP: {health}/{maxHealth}";
	}

	private void OnAttackPressed()
	{
        var animatedSprite2D = GetNode<AnimatedSprite2D>("Player/AnimatedSprite2D");		
		animatedSprite2D.Play("attack");	
		
        //We use Math.Max so that it always returns the biggest number, if enemy hp goes below 0 it always gives us the 0.
        currentEnemyHealth = Math.Max(0, currentEnemyHealth - _playerState.Damage);
        SetHealth(GetNode<ProgressBar>("EnemyContainer/EnemyHealthBar"), currentEnemyHealth, Enemy.Health);

		

    }
}

