using Godot;
using System;

public partial class RunningMeleSkeletonState : Node2D, IMeleSkeletonState
{
    private MeleSkeleton _enemy;

    public RunningMeleSkeletonState(MeleSkeleton enemy)
    {
        _enemy = enemy;
        _enemy.SpeedMultiplier = 2;
        ((CircleShape2D)_enemy.MeleTrigger.Shape).Radius = 60;
    }

    public override void _PhysicsProcess(double delta)
    {
        _enemy.Move(Global.SceneObjects.Player.GlobalPosition, delta);
    }

    public void Attack()
    {
        _enemy.Attack(new ShovelAttackMeleSkeleton(_enemy.Damage, _enemy.GlobalPosition, Global.SceneObjects.Player.GlobalPosition));
        _enemy.SpeedMultiplier = 1;
        _enemy.State = new ShovelAttackMeleSkeletonState(_enemy);
    }

    public string GetAnimation() =>
        "running";
}
