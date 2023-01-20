using Godot;
using System;

public partial class Jumper : CharacterBody2D
{
	//[Signal]
	//public delegate void Hit();
	public bool hasjumped = false;
	public bool iscolliding = false;
	public float x = 0;
	public float y = 0;
	public float gravity = 200;
	public float jumpSpeed = -300;
	public double deltag; //Making delta global
	public bool seen = false;

	public Vector2 speed = new Vector2();

	public override void _PhysicsProcess(double delta)
	{
		deltag = delta;
		//MoveAndCollide(new Vector2(0, gravity * delta));
		speed.y += gravity * (float)delta;

		Velocity = speed;

		if (seen && !hasjumped)
		{
			hasjumped = true;
			speed.y = jumpSpeed;
		}

		//speed = MoveAndSlide(speed, new Vector2(0, -1)); //gravity
		MoveAndSlide();

		seen = false; //temp should do sight body exited?
	}

	public void _on_Hitbox_body_entered(Node2D body)
	{
		if (body.Name == "TileMap")
			hasjumped = false;
		//if (body.Name == "Player")
			//EmitSignal("Hit");
	}
	public void _on_Sight_body_entered(Node2D body)
	{

		if (body.Name == "Player")
		{
			//GD.Print("I see you");
			seen = true;
		}
	}
}





