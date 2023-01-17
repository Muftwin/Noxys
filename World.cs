using Godot;
using System;

public partial class World3D : Node
{
	[Export]
	public PackedScene EnemyScene;
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

	}

	public override void _Process(double delta)
	{
		//GD.Print(Engine.GetFramesPerSecond());
		if (Input.IsKeyPressed(Key.R))
			GetTree().ReloadCurrentScene();
	}
}
