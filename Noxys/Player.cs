using System;
using Godot;
public class Player : KinematicBody2D
{
    [Signal]
    public delegate void Collide();

    int facing = 1; //1 for right -1 for left
    public Character character = Character.ADOL;
    Godot.Sprite plrsprite;
    CircleShape2D circle = new CircleShape2D();
    public const float INITIAL_GRAVITY = 5f;
    public float gravity;

    bool dashing = false;
    float dashTime = 0.25f;

    float[] reload = new float[2];

    Bat bat;

    public enum Character
    {
        ADOL, CAT, BAT, BULL, DOLL, WIZARD
    }
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        plrsprite = this.GetNode<Godot.Sprite>("plrsprite");
        circle.Radius = 8;

        gravity = INITIAL_GRAVITY;

        reload[0] = this.Position.x;
        reload[1] = this.Position.y;

        bat = new Bat(this);
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        circle.Draw(this.GetCanvasItem(), Color.FromHsv(10, 10, 10, 0.5f));

        float AMOUNT = 2;
        plrsprite.GlobalPosition = new Vector2(this.Position.x, this.Position.y);
        MoveAndCollide(new Vector2(0, gravity));

        if (Input.IsKeyPressed((int)KeyList.Left) || Input.IsKeyPressed((int)KeyList.A))
        {
            MoveAndCollide(new Vector2(-AMOUNT, 0));
            facing = -1;
        }
        if (Input.IsKeyPressed((int)KeyList.Right) || Input.IsKeyPressed((int)KeyList.D))
        {
            MoveAndCollide(new Vector2(AMOUNT, 0));
            facing = 1;
        }
        if (Input.IsKeyPressed((int)KeyList.Space))
        {
            MoveAndCollide(new Vector2(0, -10));
        }

        //temporary restart
        if (Input.IsKeyPressed((int)KeyList.R))
        {
            this.Position = new Vector2(reload[0], reload[1]);
        }

        //temporary dash
        if (Input.IsKeyPressed((int)KeyList.X))
        {
            dashing = true;
        }
        if (dashing)
        {
            MoveAndCollide(new Vector2(AMOUNT * facing, 0));
            dashTime -= delta;
        }
        if (dashTime <= 0)
        {
            dashing = false;
            dashTime = 0.25f;
        }

        //transform into bat
        if (Input.IsKeyPressed((int)KeyList.Up) || Input.IsKeyPressed((int)KeyList.W))
        {
            bat.transform();
        }

        bat.passive();
    }

    public void setSprite(String path)
    {
        plrsprite.Texture = ResourceLoader.Load(path) as Texture;
    }
}


