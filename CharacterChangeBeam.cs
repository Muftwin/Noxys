using Godot;
using System;

public partial class CharacterChangeBeam : Area2D
{
	[Export]
	private Player.Character characterToChangeInto;
	
	public override void _Ready()
	{
	}
	
	private void _on_body_entered(Node2D body)
	{
		if (body is Player)
		{
			((Player)body).transform(characterToChangeInto);
		}
	}
}


