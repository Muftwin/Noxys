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

    public bool InFanZone { get; set; }

    public enum Character
    {
        ADOL, CAT, BAT, BULL, DOLL, WIZARD, SKULL
    }

    public override void _Ready()
    {
        sprite = this.GetNode<Godot.Sprite2D>("plrsprite");

        spawn[0] = this.Position.x;
        spawn[1] = this.Position.y;

        bat = new Bat(this);
        cat = new Cat(this);
        bull = new Bull(this);
    }

    public override void _PhysicsProcess(double delta)
    {
        inputBuffer.update((float)delta);

        if (dead)
            return;

        bool jump = (inputBuffer.spaceLastDown <= jumpBufferLength * delta && !inputBuffer.usedTheSpaceToJumpAlready && IsOnFloor());
        bool coyoteJump = (inputBuffer.spaceLastDown <= jumpBufferLength * delta && coyoteTimeLength * delta >= inputBuffer.lastOnTheFloor && this.Position.y >= inputBuffer.yCoordinateLastOnTheFloor);

        if (jump || coyoteJump)
        {
            speed.y = JumpSpeed;
            inputBuffer.usedTheSpaceToJumpAlready = true;
        }

        if (InFanZone)
            Velocity -= new Vector2(0, 10);

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

        var vel = Velocity;
        vel.y += gravity * (float)delta;
        Velocity = vel;
        speed.y += gravity * (float)delta;
        if (bat.slowFall && speed.y > 0)
            speed.y /= 1.25f;

        if (IsOnFloor()) //Should this be inside input buffer? rn input buffer doesnt depend on player, but...idk
        {
            inputBuffer.lastOnTheFloor = 0;
            inputBuffer.yCoordinateLastOnTheFloor = this.Position.y;
        }

        //movement
        if (bull.dashing)
        {
            speed.y = 0;
            Velocity = new Vector2(500 * facing, 0);
            //MoveAndSlide(new Vector2(500 * facing, 0), UP);
        }
        else if (cat.climbing)
        {
            Velocity = new Vector2(speed.x, -50);
            //speed = MoveAndSlide(new Vector2(speed.x, -50), UP);
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

        //temp attack
        if (Input.IsKeyPressed(Key.X))
        {
            PackedScene bulletScene = GD.Load<PackedScene>("res://shadowbullet.tscn");
            var bullet = bulletScene.Instantiate<Bullet>();
            Owner.AddChild(bullet);

            bullet.Position = Position; //new Vector2(Position.x, Position.y);
            bullet.setPreset1(facing);
        }

        bat.passive();
        cat.passive();
        bull.passive((float)delta);

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
}





