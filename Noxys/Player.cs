using Godot;


public class Player : KinematicBody2D
{
	[Signal]
	public delegate void Collide();
	
	int facing = 1; //1 for right -1 for left
	bool dashing = false;
	float dashTime = 0.25f;
float[] reload = new float[2];

	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{	  

reload[0] = this.Position.x;
reload[1] = this.Position.y;


		
	}
	public void moveplayer(string DIR,float AMOUNT,Sprite plrsprite)
	{
if(DIR == "right")
{
	MoveAndCollide(new Vector2(AMOUNT, 0));

			facing = 1;
}
if(DIR == "left")
{
		MoveAndCollide(new Vector2(-AMOUNT, 0));

			facing = -1;
}
if(DIR == "up")
{
		MoveAndCollide(new Vector2(0,-AMOUNT));

}
if(DIR == "down")
{
			MoveAndCollide(new Vector2(0,AMOUNT));

	
}
if(DIR == "dash")
{
				MoveAndCollide(new Vector2(AMOUNT * facing,0));

}

	}


//  // Called every frame. 'delta' is the elapsed time since the previous frame.
 public override void _Process(float delta)
 {
		Godot.Sprite plrsprite= this.GetNode<Godot.Sprite>("plrsprite");
		float AMOUNT = 2;
		plrsprite.GlobalPosition = new Vector2(this.Position.x,this.Position.y);
				MoveAndCollide(new Vector2(0, 1));
				if (Input.IsKeyPressed((int)KeyList.W))
		{
			moveplayer("up",AMOUNT,plrsprite);
			
		}
			if (Input.IsKeyPressed((int)KeyList.S))
		{
			moveplayer("down",AMOUNT,plrsprite);
			
		}
		if (Input.IsKeyPressed((int)KeyList.A))
		{
			moveplayer("left",AMOUNT,plrsprite);
			
		}
		if (Input.IsKeyPressed((int)KeyList.D))
		{
					moveplayer("right",AMOUNT,plrsprite);
		}
		
		if (Input.IsKeyPressed((int)KeyList.R))
		{

this.Position = new Vector2(reload[0],reload[1]);

			
		}
		if (Input.IsKeyPressed((int)KeyList.X)){
			dashing = true;
		}
		if(dashing){
			
					moveplayer("dash",AMOUNT,plrsprite);
			dashTime -= delta;
		}
		if(dashTime<= 0){
			dashing = false;
			dashTime = 0.25f;
		}
 }

}


