using Godot;
using System;

public partial class CalmMeleSkeletonState : Node2D, IMeleSkeletonState
{
    private MeleSkeleton _enemy;

    public CalmMeleSkeletonState(MeleSkeleton enemy)
    {
        _enemy = enemy;
    }

    public void Attack()
    {
        _enemy.State = new RunningMeleSkeletonState(_enemy);
    }

    public string GetAnimation() =>
        "shovel_movement";
}
