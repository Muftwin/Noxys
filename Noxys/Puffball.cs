using Godot;
using System;

public class Puffball : RigidBody2D
{
	AnimationPlayer animPlayer;
	Particles2D particles;
	Sprite sprite;
	public override void _Ready()
	{
		animPlayer = (AnimationPlayer)GetNode("AnimationPlayer");
		particles = (Particles2D)GetNode("Particles2D");
		sprite = (Sprite)GetNode("Sprite");

		animPlayer.Play("idle");
	}
	private void _on_Hitbox_PlayerHit()
	{
		PopAndDestroy();

	}
	private void HitTilemap(object body)
	{
		PopAndDestroy();

	}

	private async void PopAndDestroy()
	{
		Sleeping = true;
		sprite.Visible = false;
		particles.Emitting = true;
		Timer timer = new Timer();
		timer.WaitTime = 1;
		AddChild(timer);
		timer.Start();
		await ToSignal(timer, "timeout");
		QueueFree();
	}
}






