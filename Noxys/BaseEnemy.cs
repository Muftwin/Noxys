using Godot;
using System;

public class BaseEnemy : KinematicBody2D
{
	internal bool playerseen = false;

	public override void _Ready()
	{
		
	}

	private void PlayerEnteredHitbox(object body)
	{
		((Player)body).Kill();
	}
	private void PlayerEnteredLineOfSight(object body)
	{
		playerseen = true;
		GD.Print("player seen");
	}
}




