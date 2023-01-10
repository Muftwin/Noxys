using Godot;
using System;

public class Jumper : BaseEnemy
{
	[Signal]
	public delegate void Hit();
	public bool hasjumped = false;
	public bool iscolliding = false;
	public float x = 0;
	public float y = 0;
	public float gravity = 200;
	public float jumpSpeed = -300;
	public float deltag; //Making delta global
	public Vector2 speed = new Vector2();

	public override void _PhysicsProcess(float delta)
	{
		deltag = delta;
		//MoveAndCollide(new Vector2(0, gravity * delta));
		speed.y += gravity * delta;

		if (playerseen && !hasjumped)
		{
			hasjumped = true;
			speed.y = jumpSpeed;
		}

		speed = MoveAndSlide(speed, new Vector2(0, -1)); //gravity

		//playerseen = false; //temp should do sight body exited?
	}

	/*public void _on_Hitbox_body_entered(PhysicsBody2D body)
	{
		if (body.Name == "TileMap")
			hasjumped = false;
		if (body.Name == "Player")
			EmitSignal("Hit");
	}*/
}







