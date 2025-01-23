using Godot;
using System;

public partial class TestEnemyAttack1 : EnemyAttack
{
    private Vector2 _direction;

    public TestEnemyAttack1(int damage, double lifeTime, Vector2 enemyPosition, Vector2 targetPosition) : base(damage, lifeTime)
    {
        AddChild(new CollisionShape2D()
        {
            Shape = new CircleShape2D()
            {
                Radius = 2
            }
        });
        GlobalPosition = enemyPosition;
        _direction = GlobalPosition.DirectionTo(targetPosition);
    }

    public override void _PhysicsProcess(double delta)
    {
        GlobalPosition += _direction * 400 * (float)delta;
    }
}
