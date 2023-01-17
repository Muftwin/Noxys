using Godot;
using System;

public partial class World : Node
{
	[Export]
	public PackedScene EnemyScene;
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	public override void _Input(InputEvent @event)
	{
		//GD.Print(Engine.GetFramesPerSecond());
		if (Input.IsKeyPressed(Key.R))
		{
			var error = GetTree().ReloadCurrentScene();
			GD.Print($"Restart: {error}");
		}
	}
}
