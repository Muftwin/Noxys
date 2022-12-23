using Godot;
using System;

public class Puffball : Area2D
{
AnimatedSprite animatedSprite;

	public override void _Ready()
	{

				animatedSprite = GetNode<AnimatedSprite>("AnimatedSprite");
						animatedSprite.Play();
	}


public override void _Process(float delta)
 {

this.Position += new Vector2(0,200*delta);
 }
 public void _on_Puffball_body_entered(PhysicsBody2D body)
{
	if(body.Name == "Player")
	{
this.QueueFree();
EmitSignal("Hit");
}
}

}
