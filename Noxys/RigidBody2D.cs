using Godot;
using System;

public class RigidBody2D : Godot.RigidBody2D
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}


public override void _Process(float delta)      
{
	this.Position = this.Position;

}
}
