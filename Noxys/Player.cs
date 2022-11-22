using System;
using Godot;
public class Player : KinematicBody2D
{
	[Signal]
	public delegate void Collide();

	public int facing = 1; //1 for right -1 for left
	public Character character = Character.ADOL;
	public Godot.Sprite sprite;

	[Export] public int maxWalkSpeed = 350;
	[Export] int walkIncrement = 10;
	[Export] int slowIncrement = 50;

	public const float INITIAL_GRAVITY = 5f;
	public float gravity = INITIAL_GRAVITY; //delete this I don't need 2 gravitys
	public int Gravity = 500;

	[Export] public int JumpSpeed = -350;
	public Vector2 speed = new Vector2();
	bool jumping = false;
	[Export] public const float jumpBufferLength = 0.1f;

	float[] spawn = new float[2];

	Bat bat;
	Cat cat;
	Bull bull;

	InputBuffer inputBuffer = new InputBuffer();

	public enum Character
	{
		ADOL, CAT, BAT, BULL, DOLL, WIZARD
	}

	public override void _Ready()
	{
		sprite = this.GetNode<Godot.Sprite>("plrsprite");

		spawn[0] = this.Position.x;
		spawn[1] = this.Position.y;

		bat = new Bat(this);
		cat = new Cat(this);
		bull = new Bull(this);
	}

	public override void _PhysicsProcess(float delta)
	{
		inputBuffer.update(delta);
		bool right = Input.IsActionPressed("ui_right");
		bool left = Input.IsActionPressed("ui_left");

		bool jump = (inputBuffer.spaceLastDown < jumpBufferLength && !inputBuffer.usedTheSpaceToJumpAlready);

		if (jump && IsOnFloor())
		{
			jumping = true;
			speed.y = JumpSpeed;
			inputBuffer.usedTheSpaceToJumpAlready = true;
		}

		if (right)
		{
			if (speed.x >= 0)
				speed.x = Math.Min(speed.x + walkIncrement, maxWalkSpeed);
			else //if you were going left slow down faster
				speed.x = Math.Min(speed.x + slowIncrement, maxWalkSpeed);
			facing = 1;
		}
		if (left)
		{
			if (speed.x <= 0)
				speed.x = Math.Max(speed.x - walkIncrement, -maxWalkSpeed);
			else
				speed.x = Math.Max(speed.x - slowIncrement, -maxWalkSpeed);
			facing = -1;
		}
		if (!right && !left)
		{
			if (speed.x < 0)
				speed.x = Math.Min(0, speed.x + slowIncrement);
			else if (speed.x > 0)
				speed.x = Math.Max(0, speed.x - slowIncrement);
		}

		speed.y += Gravity * delta;
		if (bat.slowFall && speed.y > 0)
			speed.y /= 1.25f;

		if (IsOnFloor()) //Do I really need this?
			jumping = false;

		if (bull.dashing)
		{
			jumping = false;
			speed.y = 0;
			MoveAndSlide(new Vector2(500 * facing, 0));
		}
		else
		{
			speed = MoveAndSlide(speed, new Vector2(0, -1)); //gravity
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
			this.Position = new Vector2(spawn[0], spawn[1]);
		}
	}

	public void setSprite(String path)
	{
		sprite.Texture = ResourceLoader.Load(path) as Texture;
	}
}


