using System;
using Godot;
public class Player : KinematicBody2D
{
	[Signal]
	public delegate void Collide();

	public int facing = 1; //1 for right -1 for left
	public Character character = Character.ADOL;
	public Godot.Sprite plrsprite;
	CircleShape2D circle = new CircleShape2D();
	public const float INITIAL_GRAVITY = 5f;
	public float gravity;

	float[] reload = new float[2];

	Bat bat;
	Cat cat;
	Bull bull;

	public enum Character
	{
		ADOL, CAT, BAT, BULL, DOLL, WIZARD
	}

	public override void _Ready()
	{
		plrsprite = this.GetNode<Godot.Sprite>("plrsprite");
		circle.Radius = 8;

		gravity = INITIAL_GRAVITY;

		reload[0] = this.Position.x;
		reload[1] = this.Position.y;

		bat = new Bat(this);
		cat = new Cat(this);
		bull = new Bull(this);
	}

	public void setSprite(String path)
	{
		plrsprite.Texture = ResourceLoader.Load(path) as Texture;
	}

	[Export] public int RunSpeed = 150;
	[Export] public int JumpSpeed = -400;
	[Export] public int Gravity = 800;

	public Vector2 velocity = new Vector2();
	bool jumping = false;

	public void GetInput()
	{
		velocity.x = 0;
		bool right = Input.IsActionPressed("ui_right");
		bool left = Input.IsActionPressed("ui_left");
		bool jump = Input.IsActionPressed("ui_select");

		if (jump && IsOnFloor())
		{
			jumping = true;
			velocity.y = JumpSpeed;
		}

		if (right)
		{
			velocity.x += RunSpeed;
			facing = 1;
		}
		if (left)
		{
			velocity.x -= RunSpeed;
			facing = -1;
		}
	}

	public override void _PhysicsProcess(float delta)
	{
		circle.Draw(this.GetCanvasItem(), Color.FromHsv(10, 10, 10, 0.5f));
		plrsprite.GlobalPosition = new Vector2(this.Position.x, this.Position.y);

		GetInput();

		velocity.y += Gravity * delta;
		if (bat.slowFall && velocity.y > 0)
			velocity.y /= 1.25f;

		if (jumping && IsOnFloor())
			jumping = false;
		velocity = MoveAndSlide(velocity, new Vector2(0, -1));

		if (bull.dashing)
		{
			MoveAndCollide(new Vector2(5 * facing, 0));
		}
		if (Input.IsKeyPressed((int)KeyList.Up) || Input.IsKeyPressed((int)KeyList.W))
		{
			bat.transform();
		}
		if (Input.IsKeyPressed((int)KeyList.Shift))
		{
			bull.transform();
		}

		bat.passive();
		cat.passive();
		bull.passive(delta);

		//temp reset
		if (Input.IsKeyPressed((int)KeyList.R))
		{
			this.Position = new Vector2(reload[0], reload[1]);
		}
	}
}


