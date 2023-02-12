using Godot;
using System;

public partial class CharacterChangeBeam : Area2D
{
	[Export]
	private Player.Character characterToChangeInto;
	[Export]
	private int heightpx;
	
	public override void _Ready()
	{
		update_height_of_beam();
	}
	
	private void update_height_of_beam()
	{
		CollisionShape2D cs = (CollisionShape2D)ShapeOwnerGetOwner((uint)GetShapeOwners()[0]);
		SegmentShape2D shape = (SegmentShape2D)cs.Shape;
		shape.B = new Vector2(0, -heightpx);
	}
	
	private void _on_body_entered(Node2D body)
	{
		if (body is Player)
		{
			((Player)body).transform(characterToChangeInto);
		}
	}
}


