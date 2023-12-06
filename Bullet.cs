using Godot;

public partial class Bullet : CharacterBody2D
{
	public float MaxDistance = 800;
	private Vector2 originalPos;

	public int facing;
	public Vector2 speed = new Vector2();

	public Style style;

	public float lifeTime = 0;
	public float maxLifeTime = 10;

	public override void _PhysicsProcess(double delta)
	{
		lifeTime += (float)delta;
		if (style == Style.preset1)
			updatePreset1();

		Velocity = speed;

		MoveAndSlide();

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
		speed[0] = facing * 50;

	}

	public void updatePreset1()
	{
		speed[0] = facing * 100 * Mathf.Sin(lifeTime);
		speed[1] = facing * 100 * Mathf.Cos(lifeTime);
	}

	public void setPreset2()
	{
		style = Style.preset2;
	}

}
