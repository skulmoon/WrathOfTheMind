using Godot;
using System;
using static Godot.TextServer;

public partial class ThrowShovelMeleSkeleton : EnemyAttack
{
    private Vector2 _direction;

    public ThrowShovelMeleSkeleton(int damage, double lifeTime, Vector2 enemyPosition, Vector2 targetPosition) : base(damage, lifeTime)
    {
        Shape = new RectangleShape2D()
        {
            Size = new Vector2(4, 30)
        };
        GlobalPosition = enemyPosition;
        _direction = GlobalPosition.DirectionTo(targetPosition);
    }

    public override void _PhysicsProcess(double delta)
    {
        GlobalPosition += _direction * 700 * (float)delta;
        Rotate(Mathf.DegToRad(480) * (float)delta);
    }
}
