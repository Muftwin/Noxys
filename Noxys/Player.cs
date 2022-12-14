using System;
using Godot;
public class Player : KinematicBody2D
{
	[Signal]
	public delegate void Collide();
	[Signal]
	public delegate void Hit();

	public bool dead = false;
	public int facing = 1; //1 for right -1 for left
	public Character character = Character.ADOL;
	public Godot.Sprite sprite;
	public Vector2 speed = new Vector2();
	public InputBuffer inputBuffer = new InputBuffer();

	[Export] public int gravity = 500;
	[Export] public int maxWalkSpeed = 350;
	[Export] int walkIncrement = 10;
	[Export] int slowIncrement = 50;
	[Export] public int JumpSpeed = -350;
	[Export] public float jumpBufferLength = 0.5f;
	[Export] public float coyoteTimeLength = 7.5f;
	[Export] public float dashBufferLength = 0.65f;

	float[] spawn = new float[2];

	Bat bat;
	Cat cat;
	Bull bull;

	public enum Character
	{
		ADOL, CAT, BAT, BULL, DOLL, WIZARD, SKULL
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
		if (Input.IsKeyPressed((int)KeyList.R)) //temp reset
		{
			this.Position = new Vector2(spawn[0], spawn[1]);
			setSprite("res://bat.png");
			dead = false;
			inputBuffer = new InputBuffer();
			return;
		}

		if (dead)
			return;

		bool jump = (inputBuffer.spaceLastDown <= jumpBufferLength * delta && !inputBuffer.usedTheSpaceToJumpAlready && IsOnFloor());
		bool coyoteJump = (inputBuffer.spaceLastDown <= jumpBufferLength * delta && coyoteTimeLength * delta >= inputBuffer.lastOnTheFloor && this.Position.y >= inputBuffer.yCoordinateLastOnTheFloor);

		if (jump || coyoteJump)
		{
			speed.y = JumpSpeed;
			inputBuffer.usedTheSpaceToJumpAlready = true;
		}

		bool right = Input.IsActionPressed("ui_right");
		bool left = Input.IsActionPressed("ui_left");
		if (right && (!left || !inputBuffer.leftMoreRecentThanRight))
		{
			if (speed.x >= 0)
				speed.x = Math.Min(speed.x + walkIncrement, maxWalkSpeed);
			else //if you were going left slow down faster
				speed.x = Math.Min(speed.x + slowIncrement, maxWalkSpeed);
			facing = 1;
		}
		if (left && (!right || inputBuffer.leftMoreRecentThanRight))
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

		speed.y += gravity * delta;
		if (bat.slowFall && speed.y > 0)
			speed.y /= 1.25f;

		if (IsOnFloor()) //Should this be inside input buffer? rn input buffer doesnt depend on player, but...idk
		{
			inputBuffer.lastOnTheFloor = 0;
			inputBuffer.yCoordinateLastOnTheFloor = this.Position.y;
		}

		if (bull.dashing)
		{
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
		if (inputBuffer.shiftLastDown <= dashBufferLength /* * delta */ && !inputBuffer.usedTheShiftToDashAlready)
		{
			bull.transform();
		}
		//temp attack
		if (Input.IsKeyPressed((int)KeyList.X))
		{
			PackedScene bulletScene = GD.Load<PackedScene>("res://shadowbullet.tscn");

			Bullet bullet = (Bullet)bulletScene.Instance();
			bullet.Position = new Vector2(200, 200);
			bullet.Rotation = Mathf.Deg2Rad(40);

			bullet.LaunchBullet();
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

	public void setSprite(string path)
	{
		sprite.Texture = ResourceLoader.Load(path) as Texture;
	}

	public void _on_Enemy_body_entered(PhysicsBody2D body) //delete this?
	{
		if (body.Name == "Player")
		{
			setSprite("res://skeleton.png");
			GD.Print("Game Over");
			dead = true;
		}
	}
}





