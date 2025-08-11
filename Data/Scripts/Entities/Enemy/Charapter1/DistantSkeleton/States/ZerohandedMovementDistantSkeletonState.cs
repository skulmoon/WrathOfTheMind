using Godot;
using System;

public partial class ZerohandedMovementDistantSkeletonState : Node2D, IDistantSkeletonState
{
    private DistantSkeleton _enemy;

    public ZerohandedMovementDistantSkeletonState(DistantSkeleton enemy)
    {
        _enemy = enemy;
        ((CircleShape2D)_enemy.Trigger.Shape).Radius = 45;
        if (((CircleShape2D)_enemy.Trigger.Shape).Radius + 16 > _enemy.GlobalPosition.DistanceTo(Global.SceneObjects.Player?.GlobalPosition ?? Vector2.Zero))
            _enemy.State = new ZerohandedAttackDistanceSkeletonState(_enemy);
    }

    public override void _PhysicsProcess(double delta)
    {
        _enemy.Move(_enemy.PositionControl?.GetPosition() ?? Vector2.Zero, delta);
    }

    public void Attack()
    {
        _enemy.State = new ZerohandedAttackDistanceSkeletonState(_enemy);
    }

    public string GetAnimation() =>
        "zerohanded_movement";
}
