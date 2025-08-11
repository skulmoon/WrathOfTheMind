using Godot;
using System;

public partial class ShovelMovementMeleSkeletonState : Node2D, IMeleSkeletonState
{
    private MeleSkeleton _enemy;

    public ShovelMovementMeleSkeletonState(MeleSkeleton enemy)
    {
        _enemy = enemy;
    }

    public override void _PhysicsProcess(double delta)
    {
        _enemy.Move(_enemy.PositionControl?.GetPosition() ?? Vector2.Zero, delta);
    }

    public void Attack()
    {
        _enemy.State = new ShovelAttackMeleSkeletonState(_enemy);
    }

    public void ThrowShovel()
    {
        _enemy.State = new ShovelThrowMeleSkeletonState(_enemy);
    }

    public string GetAnimation() =>
        "shovel_movement";
}
