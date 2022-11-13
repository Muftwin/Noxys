using Godot;
using System;


public class World : Node
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
//      
 }





public void _on_Player_Collide()
{
GD.Print("Player Colliding");
}
}
