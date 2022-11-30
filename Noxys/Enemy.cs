	using Godot;
using System;

public class Enemy : RigidBody2D
{

	public bool hasjumped = false;
	public bool iscolliding = false;
	public float x = 0;
	public float y = 0;
	public float gravity = 1;
	public void jump()
	{
		if(!hasjumped)
		{
this.ApplyCentralImpulse(new Vector2(0, -300));
		hasjumped = true;
		}
	}

public void Move(float x, float y)
{
	
this.ApplyCentralImpulse(new Vector2(x, y));
}
public void _on_Sight_body_entered(PhysicsBody2D body)
{
	if(body.Name == "Player")
	{
		jump();
	GD.Print("I See you");
	}
	}
public void _on_Enemy_body_entered(PhysicsBody2D body)
{
hasjumped = false;
iscolliding = true;

}
	public override void _Ready()
	{
		
	}


public override void _Process(float delta)
 {
	if(iscolliding == false)
			this.ApplyCentralImpulse(new Vector2(0,gravity));

	
	
	
 }




}








