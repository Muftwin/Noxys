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

    public override void _Process(float delta)
    {
        circle.Draw(this.GetCanvasItem(), Color.FromHsv(10, 10, 10, 0.5f));
        plrsprite.GlobalPosition = new Vector2(this.Position.x, this.Position.y);

        if (bull.dashing)
        {
            MoveAndCollide(new Vector2(5 * facing, 0));
        }
        else
        {
            MoveAndCollide(new Vector2(0, gravity));
            if (Input.IsKeyPressed((int)KeyList.Left) || Input.IsKeyPressed((int)KeyList.A))
            {
                MoveAndCollide(new Vector2(-2, 0));
                facing = -1;
            }
            if (Input.IsKeyPressed((int)KeyList.Right) || Input.IsKeyPressed((int)KeyList.D))
            {
                MoveAndCollide(new Vector2(2, 0));
                facing = 1;
            }
            this.MoveAndSlideWithSnap(new Vector2(0, 0), new Vector2(0, 0));
            if (Input.IsKeyPressed((int)KeyList.Space) && this.IsOnFloor())
            {
                MoveAndCollide(new Vector2(0, -10));
            }
        }
        //temporary restart
        if (Input.IsKeyPressed((int)KeyList.R))
        {
            this.Position = new Vector2(reload[0], reload[1]);
        }


        //transform into bat
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
    }

    public void setSprite(String path)
    {
        plrsprite.Texture = ResourceLoader.Load(path) as Texture;
    }
}


