using Godot;

public class Bullet : KinematicBody2D
{
	public float MaxDistance = 800;
	private Vector2 originalPos;

	public int facing;
	public Vector2 speed = new Vector2();

	public Style style;

	public float lifeTime = 0;
	public float maxLifeTime = 10;

	public override void _PhysicsProcess(float delta)
	{
		lifeTime += delta;
		if (style == Style.preset1)
			updatePreset1();

		MoveAndSlide(speed);

		if (lifeTime > maxLifeTime)
			this.QueueFree();
	}

	public enum Style
	{
		preset1, preset2
	}

	public void setPreset1(int facing)
	{
		style = Style.preset1;
		this.facing = facing;
		speed.x = facing * 50;

	}

	public void updatePreset1()
	{
		speed.x = facing * 100 * Mathf.Sin(lifeTime);
		speed.y = facing * 100 * Mathf.Cos(lifeTime);
	}

	public void setPreset2()
	{
		style = Style.preset2;
	}

}
