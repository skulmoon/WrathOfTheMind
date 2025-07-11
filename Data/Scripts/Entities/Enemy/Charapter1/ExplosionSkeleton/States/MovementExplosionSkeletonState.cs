using Godot;
using System;

public partial class MovementExplosionSkeletonState : Node2D, IExplosionSkeletonState
{
    private ExplosionSkeleton _enemy;

    public MovementExplosionSkeletonState(ExplosionSkeleton enemy)
    {
        _enemy = enemy;
    }

    public override void _PhysicsProcess(double delta)
    {
        _enemy.Move(Global.SceneObjects.Player.GlobalPosition, delta);
    }

    public void Attack()
    {
        _enemy.State = new AttackExplosionSkeletonState(_enemy);
    }

    public string GetAnimation() =>
        "shovel_movement";
}
