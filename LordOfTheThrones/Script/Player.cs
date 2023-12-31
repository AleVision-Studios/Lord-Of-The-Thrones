using Godot;
using System;

public partial class Player : Area2D
{
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}


	//We use this method to make sure the player always returns to an idle animation after they've done an action
	private void SetIdleAnimation()
	{
		var animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		animatedSprite2D.Play("idle");
	}

}
