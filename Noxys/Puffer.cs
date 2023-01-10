using Godot;
using System;
using System.Diagnostics;

public class Puffer : BaseEnemy
{
	[Export] float attackspeed = 5;
	float time = 0;
	PackedScene Puffscence;

	float timer(float delta, float a)
	{
			return a += 1 * delta;
	}

	public override void _Ready()
	{
		Puffscence = GD.Load<PackedScene>("res://Puffball.tscn");
		
	}

	public override void _Process(float delta)
	{
		time = timer(delta,  time);
		if  (time > attackspeed)
		{
			Puffball ball  = (Puffball)Puffscence.Instance();
			GetParent().AddChild(ball);
			ball.GlobalPosition = new Vector2(this.GlobalPosition.x, this.GlobalPosition.y + 10);
			time = 0;
			GD.Print("puffball shot");
		}
	}
 
}



