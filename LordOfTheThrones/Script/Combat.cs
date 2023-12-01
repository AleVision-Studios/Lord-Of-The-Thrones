using Godot;
using System;
using System.Threading.Tasks;

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
		var playerSprite2D = GetNode<AnimatedSprite2D>("Player/AnimatedSprite2D");
		var enemySprite2D = GetNode<AnimatedSprite2D>("Necromancer/AnimatedSprite2D");
		playerSprite2D.Play("attack");
		enemySprite2D.Play("getHit");

		//We use Math.Max so that it always returns the biggest number, if enemy hp goes below 0 it always gives us the 0.
		currentEnemyHealth = Math.Max(0, currentEnemyHealth - _playerState.Damage);
		SetHealth(GetNode<ProgressBar>("EnemyContainer/EnemyHealthBar"), currentEnemyHealth, Enemy.Health);

		if (currentEnemyHealth == 0)
		{
			enemySprite2D.Play("death");
		}

		EnemyTurn();
	}

	private void EnemyTurn()
	{
		var playerSprite2D = GetNode<AnimatedSprite2D>("Player/AnimatedSprite2D");
		var enemySprite2D = GetNode<AnimatedSprite2D>("Necromancer/AnimatedSprite2D");
		playerSprite2D.Play("getHit");
		enemySprite2D.Play("attack");

		currentPlayerHealth = Math.Max(0, currentPlayerHealth - Enemy.Damage);
		SetHealth(GetNode<ProgressBar>("PlayerStats/PlayerContainer/PlayerHealthBar"), currentPlayerHealth, _playerState.MaxHealth);

		if (currentPlayerHealth == 0) 
		{
			playerSprite2D.Play("death");
		}
	}

	//private async void RunMethodWithDelay()
	//{
	//	GD.Print("Before delay");
	//	await Task.Delay(1000);
	//	GD.Print("After delay");
	//	await Task.Run(() => EnemyTurn());
	//}
}

