using Godot;
using System;

public partial class AttackExplosionSkeletonState : Node2D, IExplosionSkeletonState
{
    private ExplosionSkeleton _enemy;

    public AttackExplosionSkeletonState(ExplosionSkeleton enemy)
    {
        _enemy = enemy;
    }

    public override void _PhysicsProcess(double delta)
    {
        _enemy.Attack(new ExplosionSkeletonAttack(_enemy, GlobalPosition, Global.SceneObjects.Player?.GlobalPosition ?? Vector2.Zero));
    }

    public void Chase()
    {
        _enemy.State = new MovementExplosionSkeletonState(_enemy);
    }

    public string GetAnimation() =>
        "shovel_attack";
}
