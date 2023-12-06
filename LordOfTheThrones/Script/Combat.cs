using Godot;
using System;
using System.Threading.Tasks;

public partial class Combat : Node, ICombat
{
	[Export]
	public BaseEnemy Enemy = null;

	//public BaseEnemy enemy;

	PlayerState _playerState = new PlayerState();

	public int currentEnemyHealth = 0;
	public int currentPlayerHealth = 0;

	public int enemyDmgBonus = 20;
	public int playerDmgBonus = 5;



	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		SetHealth(GetNode<ProgressBar>("EnemyStats/EnemyContainer/EnemyHealthBar"), Enemy.Health, Enemy.Health);
		SetHealth(GetNode<ProgressBar>("PlayerStats/PlayerContainer/PlayerHealthBar"), _playerState.CurrentHealth, _playerState.MaxHealth);

		currentEnemyHealth = Enemy.Health;
		currentEnemyHealth = Enemy.Health;
		currentPlayerHealth = _playerState.CurrentHealth;
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void SetHealth(ProgressBar progressBar, int health, int maxHealth)
	{
		progressBar.Value = health;
		progressBar.MaxValue = maxHealth;
		progressBar.GetNode<Label>("Label").Text = $"HP: {health}/{maxHealth}";
	}

	private async void OnAttackPressed()
	{
		try
		{
			DisableAttackButton();
			RunPlayerAttackAnimation();
			await RunMethodWithDelay(500, "RunEnemyGetsHitAnimation");

			ApplyDamageToEnemy();


			if (currentEnemyHealth == 0)
			{
				await RunMethodWithDelay(0, "RunEnemyDeathAnimation");
				DisableAttackButton();
			}
			else
			{
				await RunMethodWithDelay(1000, "EnemyTurn");
				DisableAttackButton();
			}

		}
		catch (Exception ex)
		{
			GD.PrintErr($"Exception in EnemyTurn: {ex}");
		}
	}

	private void DisableAttackButton()
	{
		GetNode<Button>("Panel/HBoxContainer/Attack").Disabled = true;
	}

	private void EnableAttackButton()
	{
		GetNode<Button>("Panel/HBoxContainer/Attack").Disabled = false;
	}

	public void ApplyDamageToEnemy()
	{
		var enemyHealthBar = GetNode<ProgressBar>("EnemyStats/EnemyContainer/EnemyHealthBar");

		currentEnemyHealth = Math.Max(0, currentEnemyHealth - (_playerState.Damage + RandomNumber(playerDmgBonus)));
		SetHealth(enemyHealthBar, currentEnemyHealth, Enemy.Health);
	}

	private async void EnemyTurn()
	{
		try
		{
			PlayEnemyAttackAnimation();
			await RunMethodWithDelay(1500, "RunPlayerGetsHitAnimation");

			ApplyDamageToPlayer();

			if (currentPlayerHealth == 0)
			{
				await RunMethodWithDelay(0, "RunPlayerDeathAnimation");
				DisableAttackButton();
			}
			else
			{
				EnableAttackButton();
			}

		}
		catch (Exception ex)
		{
			GD.PrintErr($"Exception in EnemyTurn: {ex}");
		}
	}

	private void PlayEnemyAttackAnimation()
	{
		var enemySprite2D = GetNode<AnimatedSprite2D>("Necromancer/AnimatedSprite2D");
		var enemyAttackSound = GetNode<AudioStreamPlayer>("Enemy_Attack_Sound");

		enemyAttackSound.Play();
		enemySprite2D.Play("attack");
	}

	public void ApplyDamageToPlayer()
	{
		var playerHealthBar = GetNode<ProgressBar>("PlayerStats/PlayerContainer/PlayerHealthBar");

		currentPlayerHealth = Math.Max(0, currentPlayerHealth - (Enemy.Damage + RandomNumber(enemyDmgBonus)));
		SetHealth(playerHealthBar, currentPlayerHealth, _playerState.MaxHealth);
	}

	private void RunPlayerAttackAnimation()
	{
		var playerSprite2D = GetNode<AnimatedSprite2D>("Player/AnimatedSprite2D");
		var attackSound = GetNode<AudioStreamPlayer>("Player_Attack_Sound");

		attackSound.Play();
		playerSprite2D.Play("attack");
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

	public async Task RunMethodWithDelay(int delayTime, string methodName)
	{
		await Task.Delay(delayTime);
		CallDeferred(methodName);
	}

	public int RandomNumber(int maxNumber)
	{
		Random random = new Random();

		return random.Next(1, maxNumber);
	}
}

