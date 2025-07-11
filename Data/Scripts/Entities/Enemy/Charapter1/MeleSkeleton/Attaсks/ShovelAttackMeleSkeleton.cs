using Godot;
using System;
using static Godot.TextServer;

public partial class ShovelAttackMeleSkeleton : EnemyAttack
{
    public ShovelAttackMeleSkeleton(int damage, Vector2 enemyPosition, Vector2 targetPosition) : base(damage, 0.25f, "")
    {
        Shape = new RectangleShape2D()
        {
            Size = new Vector2(4, 30)
        };
        GlobalPosition = enemyPosition;
        Collision.Position += new Vector2(0, -50);
        Rotation = enemyPosition.AngleToPoint(targetPosition) + Mathf.DegToRad(150);

    }

    public override void _PhysicsProcess(double delta)
    {
        Rotate(Mathf.DegToRad(-480) * (float)delta);
    }
}
