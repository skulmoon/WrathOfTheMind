using Godot;
using System;

public partial class ShovelAttackMeleSkeletonState : Node2D, IMeleSkeletonState
{
    private MeleSkeleton _enemy;

    public ShovelAttackMeleSkeletonState(MeleSkeleton enemy)
    {
        _enemy = enemy;
    }

    public override void _PhysicsProcess(double delta)
    {
        _enemy.Attack(new ShovelAttackMeleSkeleton(_enemy.Damage, _enemy.GlobalPosition, Global.SceneObjects.Player.GlobalPosition));
    }

    public void Chase()
    {
        _enemy.State = new ShovelMovementMeleSkeletonState(_enemy);
    }

    public string GetAnimation() =>
        "shovel_attack";
}
