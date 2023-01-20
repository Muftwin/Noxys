using Godot;
using System;

public partial class Scene : Node
{
	double modulateA = 0.5;
	private Control _control;
	private Area2D _area2d;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_control = GetNode<Control>("Control");
		_control.Visible = false;

		if (_control.Visible == false)
		{
			GD.Print("Scoreboard is Invisisble");
		}
		else {
			GD.Print("Scoreboard is visible"); 
		}
	}
	
	private void _on_area_2d_body_entered(Node2D body)
	{
		_control.Visible = true;
		GD.Print("Entered");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}

