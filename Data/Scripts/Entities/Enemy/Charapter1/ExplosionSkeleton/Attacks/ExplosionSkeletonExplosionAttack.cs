using Godot;
using System;

public partial class ExplosionSkeletonExplosionAttack : EnemyAttack
{
    private const int END_RADIUS = 96;
    private const float LIFE_TIME = 0.15f;

    public ExplosionSkeletonExplosionAttack(Vector2 position, int damage) : base(damage, LIFE_TIME, "")
    {
        GlobalPosition = position;
        Shape = new CircleShape2D()
        {
            Radius = 0
        };
    }

    public override void _PhysicsProcess(double delta)
    {
        ((CircleShape2D)Shape).Radius += ((CircleShape2D)Shape).Radius + (END_RADIUS / LIFE_TIME * (float)delta) < END_RADIUS ? END_RADIUS / LIFE_TIME * (float)delta : END_RADIUS;
    }

    public override void OnPlayerAttackEntered(Area2D area)
    {
        if (area is Shard2D shard)
            shard.TakeDamage(Damage);
        else if (area is HitBox hitBox)
            hitBox.TakeDamage(Damage);
    }
}
