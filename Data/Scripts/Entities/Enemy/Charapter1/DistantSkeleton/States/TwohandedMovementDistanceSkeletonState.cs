using Godot;
using System;

public partial class TwohandedMovementDistanceSkeletonState : Node2D, IDistantSkeletonState
{
    private DistantSkeleton _enemy;

    public TwohandedMovementDistanceSkeletonState(DistantSkeleton enemy)
    {
        _enemy = enemy;
        ((CircleShape2D)_enemy.Trigger.Shape).Radius = 300;
        if (((CircleShape2D)_enemy.Trigger.Shape).Radius + 16 > _enemy.GlobalPosition.DistanceTo(Global.SceneObjects.Player.GlobalPosition))
            _enemy.State = new TwohandedAttackDistanceSkeletonState(_enemy);
    }

    public override void _PhysicsProcess(double delta)
    {
        _enemy.Move(Global.SceneObjects.Player.GlobalPosition, delta);
    }

    public void Attack()
    {
        _enemy.State = new TwohandedAttackDistanceSkeletonState(_enemy);
    }

    public string GetAnimation() =>
        "twohanded_movement";
}
