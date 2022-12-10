	using Godot;
using System;

public class Jumper : KinematicBody2D
{		
	[Signal]
	public delegate void Hit();
	public bool hasjumped = false;
	public bool iscolliding = false;
	public float x = 0;
	public float y = 0;
	public float gravity = 200;
	public float jumped = 100;
	public float deltag; //Making delta global
	
	public void frame(float delta)
	{
		MoveAndCollide(new Vector2(0,gravity*delta));
	}
	public void jump(float delta)
	{
		if(!hasjumped)
		{
		hasjumped = true;
GD.Print(delta);
		MoveAndCollide(new Vector2(0,-jumped));
		}
		
	}
		public override void _Ready()
		{
			
		}
		public override void _PhysicsProcess(float delta)
		{
frame(delta);
deltag = delta;
 void _on_Sight_body_entered(PhysicsBody2D body)
{

if(body.Name == "Player")
{
	GD.Print("I see you");
jump(deltag);
}

		}
		}
public void _on_Hitbox_body_entered(PhysicsBody2D body)
{
	if(body.Name == "TileMap")
	hasjumped = false;
if(body.Name == "Player")
EmitSignal("Hit");
}



}

