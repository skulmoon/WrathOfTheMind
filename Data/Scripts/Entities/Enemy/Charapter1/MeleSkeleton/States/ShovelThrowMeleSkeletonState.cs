using Godot;
using System;

public partial class ShovelThrowMeleSkeletonState : Node, IMeleSkeletonState
{
    private MeleSkeleton _enemy;
    private Timer _endTimer = new Timer { Autostart = true, OneShot = true, WaitTime = 3 };
    private Timer _attackTimer = new Timer { Autostart = true, OneShot = true, WaitTime = 2 };

    public ShovelThrowMeleSkeletonState(MeleSkeleton enemy)
    {
        _enemy = enemy;
        AddChild(_attackTimer);
        AddChild(_endTimer);
        _attackTimer.Timeout += () => _enemy.Attack(new ThrowShovelMeleSkeleton(_enemy.Damage * 3, 3, _enemy.GlobalPosition, Global.SceneObjects.Player.GlobalPosition));
        _enemy.SpeedMultiplier = 1.2f;
        _endTimer.Timeout += () => _enemy.State = new FistMovementMeleSkeletonState(_enemy);
    }

    public string GetAnimation() =>
        "shovel_throw";
}
