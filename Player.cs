using System;
using Godot;
public partial class Player : CharacterBody2D
{
	//[Signal]
	//public delegate void Collide();
	//[Signal]
	//public delegate void Hit();

	public bool dead = false;
	public int facing = 1; //1 for right -1 for left
	public Character character = Character.ADOL;
	public Godot.Sprite2D sprite;
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
	Vector2 UP = new Vector2(0, -1);

	Bat bat;
	Cat cat;
	Bull bull;
	Wizard wizard;

	public bool InFanZone { get; set; }

	public enum Character
	{
		ADOL, CAT, BAT, BULL, DOLL, WIZARD, SKULL
	}

	public override void _Ready()
	{
		sprite = this.GetNode<Godot.Sprite2D>("plrsprite");

		spawn[0] = this.Position[0];
		spawn[1] = this.Position[1];

		bat = new Bat(this);
		cat = new Cat(this);
		bull = new Bull(this);
		wizard = new Wizard(this);
	}

	public override void _PhysicsProcess(double delta)
	{
		inputBuffer.update((float)delta);

		if (dead)
			return;

		bool jump = (inputBuffer.spaceLastDown <= jumpBufferLength * delta && !inputBuffer.usedTheSpaceToJumpAlready && IsOnFloor());
		bool coyoteJump = (inputBuffer.spaceLastDown <= jumpBufferLength * delta && coyoteTimeLength * delta >= inputBuffer.lastOnTheFloor && this.Position[1] >= inputBuffer.yCoordinateLastOnTheFloor);

		if (jump || coyoteJump)
		{
			speed[1] = JumpSpeed;
			inputBuffer.usedTheSpaceToJumpAlready = true;
		}

		if (InFanZone)
			Velocity -= new Vector2(0, 10);

		bool right = Input.IsActionPressed("ui_right");
		bool left = Input.IsActionPressed("ui_left");
		if (right && (!left || !inputBuffer.leftMoreRecentThanRight))
		{
			if (speed[0] >= 0)
				speed[0] = Math.Min(speed[0] + walkIncrement, maxWalkSpeed);
			else //if you were going left slow down faster
				speed[0] = Math.Min(speed[0] + slowIncrement, maxWalkSpeed);
			facing = 1;
		}
		if (left && (!right || inputBuffer.leftMoreRecentThanRight))
		{
			if (speed[0] <= 0)
				speed[0] = Math.Max(speed[0] - walkIncrement, -maxWalkSpeed);
			else
				speed[0] = Math.Max(speed[0] - slowIncrement, -maxWalkSpeed);
			facing = -1;
		}
		if (!right && !left)
		{
			if (speed[0] < 0)
				speed[0] = Math.Min(0, speed[0] + slowIncrement);
			else if (speed[0] > 0)
				speed[0] = Math.Max(0, speed[0] - slowIncrement);
		}

		var vel = Velocity;
		vel[1] += gravity * (float)delta;
		Velocity = vel;
		speed[1] += gravity * (float)delta;
		if (bat.slowFall && speed[1] > 0)
			speed[1] /= 1.25f;

		if (IsOnFloor()) //Should this be inside input buffer? rn input buffer doesnt depend on player, but...idk
		{
			inputBuffer.lastOnTheFloor = 0;
			inputBuffer.yCoordinateLastOnTheFloor = this.Position[1];
		}

		//movement
		if (bull.dashing)
		{
			speed[1] = 0;
			Velocity = new Vector2(500 * facing, 0);
			//MoveAndSlide(new Vector2(500 * facing, 0), UP);
		}
		else if (cat.climbing)
		{
			Velocity = new Vector2(speed[0], -50);
			//speed = MoveAndSlide(new Vector2(speed[0], -50), UP);
		}
		else
		{
			Velocity = speed;
			//speed = MoveAndSlide(speed, UP); //gravity
		}

		//transforming
		if (Input.IsKeyPressed(Key.Up) || Input.IsKeyPressed(Key.W))
		{
			bat.transform();
		}
		if (inputBuffer.shiftLastDown <= dashBufferLength /* * delta */ && !inputBuffer.usedTheShiftToDashAlready)
		{
			bull.transform();
		}
		if (IsOnWall())
		{
			cat.transform();
		}
		if (Input.IsKeyPressed(Key.Down) || Input.IsKeyPressed(Key.S))
		{
			wizard.transform();
		}

		//temp attack
		if (Input.IsKeyPressed(Key.X))
		{
			PackedScene bulletScene = GD.Load<PackedScene>("res://shadowbullet.tscn");
			var bullet = bulletScene.Instantiate<Bullet>();
			Owner.AddChild(bullet);

			bullet.Position = Position; //new Vector2(Position[0], Position[1]);
			bullet.setPreset1(facing);
		}

		bat.passive();
		cat.passive();
		bull.passive((float)delta);
		wizard.passive();

		MoveAndSlide();
	}

	public void setSprite(string path)
	{
		sprite.Texture = ResourceLoader.Load(path) as Texture2D;
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

	public void transform(Character character)
	{
		switch (character)
		{
			case Character.ADOL:
				break;
			case Character.CAT:
				cat.transform();
				break;
			case Character.BAT:
				bat.transform();
				break;
			case Character.BULL:
				bull.transform();
				break;
			case Character.DOLL:
				break;
			case Character.WIZARD:
				wizard.transform();
				break;
			case Character.SKULL:
				break;
		}
	}
}





