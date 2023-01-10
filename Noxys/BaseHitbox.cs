using Godot;
using System;

public class BaseHitbox : Area2D
{
	[Signal] delegate void PlayerHit();
	public override void _Ready()
	{
		CollisionMask = 2;
		Connect("body_entered", this, "PlayerEntered");
	}

	private void PlayerEntered(object body)
	{
		((Player)body).Kill();
		EmitSignal("PlayerHit");
	}
}
