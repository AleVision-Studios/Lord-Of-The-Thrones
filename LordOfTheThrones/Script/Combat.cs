using Godot;
using System;
using System.Threading.Tasks;

public partial class Combat : Node
{
	[Export]
	public BaseEnemy Enemy = null;

	private PlayerState _playerState = new PlayerState();

	private int _currentEnemyHealth = 0;
	private int _currentPlayerHealth = 0;
	
	private static int _level = 1;

	private int _enemyDamageBonus = 18 + _level;
	private int _playerDamageBonus = 5;


	public override void _Ready()
	{
		InitializeUI();
		InitializeHealthValues();
	}

	private void InitializeUI()
	{
		var enemyHealthBar = GetNode<ProgressBar>("EnemyStats/EnemyContainer/EnemyHealthBar");
		var playerHealthBar = GetNode<ProgressBar>("PlayerStats/PlayerContainer/PlayerHealthBar");
		GetNode<Label>("PlayerStats/PlayerContainer/PlayerName").Text = StartScreen.playerName;
		GetNode<Label>("Panel/Gold").Text = "X " + PlayerState.TotalGold.ToString();
		GetNode<Label>("Panel/Level").Text = "LEVEL " + _level.ToString();

		SetHealth(enemyHealthBar, Enemy.Health, Enemy.Health);
		SetHealth(playerHealthBar, _playerState.CurrentHealth, _playerState.MaxHealth);
	}

	public static void SetHealth(ProgressBar progressBar, int health, int maxHealth)
	{
		progressBar.Value = health;
		progressBar.MaxValue = maxHealth;
		progressBar.GetNode<Label>("Label").Text = $"HP: {health}/{maxHealth}";
	}

	private void InitializeHealthValues()
	{
		_currentEnemyHealth = Enemy.Health;
		_currentPlayerHealth = _playerState.CurrentHealth;
	}

	public async void OnAttackPressed()
	{
		DisableAttackButton();
		RunPlayerAttackAnimation();
		await RunMethodWithDelay(500, RunEnemyGetsHitAnimation);

		ApplyDamageToEnemy();

		if (_currentEnemyHealth == 0)
		{
			await RunMethodWithDelay(0, RunEnemyDeathAnimation);
			DisableAttackButton();
			await RunMethodWithDelay(3000, ShowWinScreen);
		}
		else
		{
			await RunMethodWithDelay(1000, EnemyTurn);
			DisableAttackButton();
		}
	}

	private void ShowWinScreen()
	{
		GetNode<AudioStreamPlayer>("Winning_Sound").Play();
		GetNode<CanvasLayer>("WinScreen").Visible = true;
		_level++;
	}

	private void ShowLoseScreen()
	{
		GetNode<AudioStreamPlayer>("Losing_Sound").Play();
		GetNode<CanvasLayer>("LoseScreen").Visible = true;
		PlayerState.TotalGold = 0;
	}

	//Disables the AttackButton, use this during the enemies Turns,
	private void DisableAttackButton()
	{
		GetNode<Button>("Panel/HBoxContainer/Attack").Disabled = true;
	}

	//Enables the button again after the enemies turn is completed.
	private void EnableAttackButton()
	{
		GetNode<Button>("Panel/HBoxContainer/Attack").Disabled = false;
	}

	public void ApplyDamageToEnemy()
	{
		var enemyHealthBar = GetNode<ProgressBar>("EnemyStats/EnemyContainer/EnemyHealthBar");

		_currentEnemyHealth = Math.Max(0, _currentEnemyHealth - (_playerState.Damage + RandomNumber(_playerDamageBonus)));
		SetHealth(enemyHealthBar, _currentEnemyHealth, Enemy.Health);
	}

	private async void EnemyTurn()
	{
		PlayEnemyAttackAnimation();
		await RunMethodWithDelay(1500, RunPlayerGetsHitAnimation);

		ApplyDamageToPlayer();

		if (_currentPlayerHealth == 0)
		{
			await RunMethodWithDelay(0, RunPlayerDeathAnimation);
			DisableAttackButton();
			await RunMethodWithDelay(1000, ShowLoseScreen);

		}
		else
		{
			EnableAttackButton();
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

		_currentPlayerHealth = Math.Max(0, _currentPlayerHealth - (Enemy.Damage + RandomNumber(_enemyDamageBonus)));
		SetHealth(playerHealthBar, _currentPlayerHealth, _playerState.MaxHealth);
	}

	private void RunPlayerAttackAnimation()
	{
		var playerSprite2D = GetNode<AnimatedSprite2D>("Player/AnimatedSprite2D");
		var attackSound = GetNode<AudioStreamPlayer>("Player_Attack_Sound");

		attackSound.Play();
		playerSprite2D.Play("attack");
	}

	private void RunPlayerGetsHitAnimation()
	{
		var playerSprite2D = GetNode<AnimatedSprite2D>("Player/AnimatedSprite2D");
		playerSprite2D.Play("getHit");
	}

	private void RunEnemyGetsHitAnimation()
	{
		var enemySprite2D = GetNode<AnimatedSprite2D>("Necromancer/AnimatedSprite2D");
		enemySprite2D.Play("getHit");
	}

	private void RunEnemyDeathAnimation()
	{
		var enemySprite2D = GetNode<AnimatedSprite2D>("Necromancer/AnimatedSprite2D");
		enemySprite2D.Play("death");
		GetNode<AudioStreamPlayer>("Death_Sound").Play();
	}

	private void RunPlayerDeathAnimation()
	{
		var playerSprite2D = GetNode<AnimatedSprite2D>("Player/AnimatedSprite2D");
		playerSprite2D.Play("death");
		GetNode<AudioStreamPlayer>("Death_Sound").Play();
	}

	public static async Task RunMethodWithDelay(int delayTime, Action method)
	{
		await Task.Delay(delayTime);
		method.Invoke();
	}
	public static int RandomNumber(int maxNumber)
	{
		Random random = new Random();
		return random.Next(1, maxNumber);
	}
}
