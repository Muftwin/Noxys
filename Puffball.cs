using Godot;
using System;

public partial class Puffball : Area2D
{
	AnimatedSprite2D animatedSprite;

	public override void _Ready()
	{

		animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		animatedSprite.Play();
	}

	public override void _Process(double delta)
	{
		this.Position += new Vector2(0, 200 * (float)delta);
	}

	public void _on_Puffball_body_entered(Node2D body)
	{
		if (body.Name == "Player")
		{
			this.QueueFree();
			//EmitSignal("Hit");
		}
	}
}
