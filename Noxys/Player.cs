using System;
using Godot;
public class Player : KinematicBody2D
{
    [Signal]
    public delegate void Collide();

    public int facing = 1; //1 for right -1 for left
    public Character character = Character.ADOL;
    public Godot.Sprite plrsprite;

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

    [Export] public int maxWalkSpeed = 350;
    int walkIncrement = 10;
    int slowIncrement = 50;

    [Export] public int JumpSpeed = -350;
    [Export] public int Gravity = 500;

    public Vector2 speed = new Vector2();
    bool jumping = false;

    public void GetInput()
    {
        bool right = Input.IsActionPressed("ui_right");
        bool left = Input.IsActionPressed("ui_left");
        bool jump = Input.IsActionPressed("ui_select");

        if (jump && IsOnFloor())
        {
            jumping = true;
            speed.y = JumpSpeed;
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
    }

    public override void _PhysicsProcess(float delta)
    {

        GetInput();

        speed.y += Gravity * delta;
        if (bat.slowFall && speed.y > 0)
            speed.y /= 1.25f;

        if (jumping && IsOnFloor())
            jumping = false;
        speed = MoveAndSlide(speed, new Vector2(0, -1));

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


