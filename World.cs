using Godot;
using System;

public partial class World : Node
{
	[Export]
	public PackedScene EnemyScene;

	private PackedScene Scene { get; set; }

	public override void _Ready()
	{
		Scene = GD.Load<PackedScene>("res://Scene.tscn");

		AddChild(Scene.Instantiate());
	}

	public override void _Input(InputEvent @event)
	{
		//GD.Print(Engine.GetFramesPerSecond());
		if (Input.IsKeyPressed(Key.R))
		{
			foreach (Node child in GetChildren())
				child.QueueFree();

			AddChild(Scene.Instantiate());
		}
	}
}
