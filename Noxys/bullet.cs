using Godot;

public class Bullet : RigidBody2D
{
    public float MaxDistance = 800; // How far (in pixels) the bullet will travel before it is destroyed
    public float ImpulseMag = 1200; // How much impulse to give the bullet when it is launched

    private Vector2 originalPos;

    public override void _PhysicsProcess(float delta)
    {
        float distanceTravelled = this.Position.DistanceTo(this.originalPos);
        if (distanceTravelled > this.MaxDistance)
            this.QueueFree();
    }

    public void LaunchBullet()
    {
        this.originalPos = this.Position;

        this.ApplyCentralImpulse(this.Transform.x.Normalized() * this.ImpulseMag); // apply an impulse in the same direction that the bullet is facing
    }
}