using Godot;
using System;

public partial class Puffer : Area2D
{
	float time = 0;
	AnimatedSprite2D sprite;
	PackedScene Puffscence;

	float timer(float delta, float a)
	{
		return a += 1 * delta;
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Puffscence = GD.Load<PackedScene>("res://Puffball.tscn");
		sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");

	}

	//  // Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		sprite.Play();
		time = timer((float)delta, time);
		if (time > 5.0f)
		{

			Puffball ball  = (Puffball)Puffscence.Instantiate();
			Owner.AddChild(ball);
			ball.Position = new Vector2(this.Position.x, this.Position.y + 10);

			time = 0;

		}
	}

	public void _on_Puffer_body_entered(PhysicsBody2D body)
	{
		if (sprite.Frame == 1 && body.Name == "Player")
		{
			EmitSignal("Hit");
		}

	}
}
