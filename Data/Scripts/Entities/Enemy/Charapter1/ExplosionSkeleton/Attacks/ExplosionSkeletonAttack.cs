using Godot;
using System;

public partial class ExplosionSkeletonAttack : EnemyAttack
{
    private ExplosionSkeleton _enemy;

    public ExplosionSkeletonAttack(ExplosionSkeleton enemy, Vector2 enemyPosition, Vector2 targetPosition) : base(0, 0.25f, "")
    {
        Shape = new RectangleShape2D()
        {
            Size = new Vector2(4, 30)
        };
        GlobalPosition = enemyPosition;
        Collision.Position += new Vector2(0, -50);
        Rotation = enemyPosition.AngleToPoint(targetPosition) + Mathf.DegToRad(150);
        _enemy = enemy;
    }

    public override void _PhysicsProcess(double delta)
    {
        Rotate(Mathf.DegToRad(-480) * (float)delta);
    }

    public override void OnPlayerAttackEntered(Area2D area)
    {
        if (area is HitBox or Shard2D)
        {
            _enemy.TakeDamage(_enemy.Damage);
            GetTree()?.CurrentScene?.CallDeferred("add_child", new ExplosionSkeletonExplosionAttack(Collision.GlobalPosition, _enemy.Damage));
            base.Destroy();
        }
    }
}
