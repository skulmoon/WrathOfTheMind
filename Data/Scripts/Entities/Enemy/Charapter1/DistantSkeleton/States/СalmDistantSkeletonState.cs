using Godot;
using System;

public partial class СalmDistantSkeletonState : Node, IDistantSkeletonState
{
    private DistantSkeleton _enemy;

    public СalmDistantSkeletonState(DistantSkeleton enemy)
    {
        _enemy = enemy;
    }

    public void Attack()
    {
        _enemy.State = new TwohandedMovementDistanceSkeletonState(_enemy);
    }

    public string GetAnimation() =>
        "twohanded_movement";
}
