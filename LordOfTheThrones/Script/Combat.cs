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


	public int enemyDmgBonus = 50;
	public int playerDmgBonus = 5;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		SetHealth(GetNode<ProgressBar>("EnemyStats/EnemyContainer/EnemyHealthBar"), Enemy.Health, Enemy.Health);
		SetHealth(GetNode<ProgressBar>("PlayerStats/PlayerContainer/PlayerHealthBar"), _playerState.CurrentHealth, _playerState.MaxHealth);

		currentEnemyHealth = Enemy.Health;
		currentPlayerHealth = _playerState.CurrentHealth;
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

	private async void OnAttackPressed()
	{
		GetNode<Button>("Panel/HBoxContainer/Attack").Disabled = true;
		var playerSprite2D = GetNode<AnimatedSprite2D>("Player/AnimatedSprite2D");
		var enemySprite2D = GetNode<AnimatedSprite2D>("Necromancer/AnimatedSprite2D");
		GetNode<AudioStreamPlayer>("Player_Attack_Sound").Play();
		playerSprite2D.Play("attack");

		await RunMethodWithDelay(500, "RunEnemyGetsHitAnimation");

		//We use Math.Max so that it always returns the biggest number, if enemy hp goes below 0 it always gives us the 0.
		currentEnemyHealth = Math.Max(0, currentEnemyHealth - (_playerState.Damage + RandomNumber(playerDmgBonus)));
		SetHealth(GetNode<ProgressBar>("EnemyStats/EnemyContainer/EnemyHealthBar"), currentEnemyHealth, Enemy.Health);

		if (currentEnemyHealth == 0)
		{
			await RunMethodWithDelay(0, "RunEnemyDeathAnimation");
			GetNode<Button>("Panel/HBoxContainer/Attack").Disabled = true;
		}
		else
		{
			await RunMethodWithDelay(1000, "EnemyTurn");
			GetNode<Button>("Panel/HBoxContainer/Attack").Disabled = true;
		}
	}

	private async void EnemyTurn()
	{
		var playerSprite2D = GetNode<AnimatedSprite2D>("Player/AnimatedSprite2D");
		var enemySprite2D = GetNode<AnimatedSprite2D>("Necromancer/AnimatedSprite2D");
		GetNode<AudioStreamPlayer>("Enemy_Attack_Sound").Play();
		enemySprite2D.Play("attack");

		await RunMethodWithDelay(1500, "RunPlayerGetsHitAnimation");

		currentPlayerHealth = Math.Max(0, currentPlayerHealth - (Enemy.Damage + RandomNumber(enemyDmgBonus)));
		SetHealth(GetNode<ProgressBar>("PlayerStats/PlayerContainer/PlayerHealthBar"), currentPlayerHealth, _playerState.MaxHealth);

		if (currentPlayerHealth == 0)
		{
			await RunMethodWithDelay(0, "RunPlayerDeathAnimation");
			GetNode<Button>("Panel/HBoxContainer/Attack").Disabled = true;
		}
		else
		{
			GetNode<Button>("Panel/HBoxContainer/Attack").Disabled = false;
		}
	}

	private async void RunPlayerGetsHitAnimation()
	{
		var playerSprite2D = GetNode<AnimatedSprite2D>("Player/AnimatedSprite2D");
		playerSprite2D.Play("getHit");
	}

	private async void RunEnemyGetsHitAnimation()
	{
		var enemySprite2D = GetNode<AnimatedSprite2D>("Necromancer/AnimatedSprite2D");
		enemySprite2D.Play("getHit");
	}

	private async void RunEnemyDeathAnimation()
	{
		var enemySprite2D = GetNode<AnimatedSprite2D>("Necromancer/AnimatedSprite2D");
		enemySprite2D.Play("death");
		GetNode<AudioStreamPlayer>("Death_Sound").Play();
	}

	private async void RunPlayerDeathAnimation()
	{
		var playerSprite2D = GetNode<AnimatedSprite2D>("Player/AnimatedSprite2D");
		playerSprite2D.Play("death");
		GetNode<AudioStreamPlayer>("Death_Sound").Play();
	}

	private async Task RunMethodWithDelay(int delayTime, string methodName)
	{
		await Task.Delay(delayTime);
		CallDeferred(methodName);
	}

	private int RandomNumber(int maxNumber)
	{
		var random = new Random();
		int result = random.Next(1, maxNumber);

		return result;
	}
}

