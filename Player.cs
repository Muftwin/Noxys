using Godot;
using System;

public class Player : Sprite
{
	int facing = 1; //1 for right -1 for left
	bool dashing = false;
	float dashTime = 0.25f;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{	  
		//Godot.Sprite platform = this.GetNode<Godot.Sprite>("Platform");

		
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
 public override void _Process(float delta)
 {
		float AMOUNT = 5;
		if (Input.IsKeyPressed((int)KeyList.W))
		{
			this.Position += new Vector2(0, -AMOUNT);
		}
		if (Input.IsKeyPressed((int)KeyList.S))
		{
			this.Position += new Vector2(0, AMOUNT);
		}
		if (Input.IsKeyPressed((int)KeyList.A))
		{
			this.Position += new Vector2(-AMOUNT,0);
			facing = -1;
		}
		if (Input.IsKeyPressed((int)KeyList.D))
		{
			this.Position += new Vector2(AMOUNT,0);
			facing = 1;
		}

		if (Input.IsKeyPressed((int)KeyList.X)){
			dashing = true;
		}
		if(dashing){
			this.Position += new Vector2(AMOUNT * facing,0);
			dashTime -= delta;
		}
		if(dashTime<= 0){
			dashing = false;
			dashTime = 0.25f;
		}
 }
}
