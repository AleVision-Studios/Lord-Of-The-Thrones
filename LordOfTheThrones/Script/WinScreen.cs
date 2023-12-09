using Godot;
using System;
using System.Reflection.PortableExecutable;

public partial class WinScreen : CanvasLayer
{
	Random rnd = new Random();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GetNode<Label>("Panel/VBoxContainer/Loot").Text = RandomizeLoot().ToString();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void OnGamblePressed()
	{
		GetNode<AnimatedSprite2D>("Chest/AnimatedSprite2D").Play("default");
		int droppedLoot = GetNode<Label>("Panel/VBoxContainer/Loot").Text.ToInt();
		int gambledLoot = GambleManager.DoubleOrNothing(droppedLoot);
		GetNode<Label>("Panel/VBoxContainer/Loot").Text = gambledLoot.ToString();
		
		GetNode<Button>("Panel/VBoxContainer/Gamble").Disabled = true;

		if (gambledLoot == 0)
		{
			GetNode<AudioStreamPlayer>("GambleFail").Play();
		}
		else
		{
			GetNode<AudioStreamPlayer>("GambleSuccess").Play();
		}
	}

	public int RandomizeLoot()
	{
		return rnd.Next(20, 200);
	}

	public void OnTakePressed()
	{
		int loot = GetNode<Label>("Panel/VBoxContainer/Loot").Text.ToInt();
		PlayerState.TotalGold += loot;
		if (loot > 0)
		{
			GetNode<AudioStreamPlayer>("Kaching").Play();
		}

		OS.DelayMsec(1000);
		GetNode<SceneLoader>("/root/SceneLoader").ChangeToScene("Scenes/game.tscn");
	}
}
