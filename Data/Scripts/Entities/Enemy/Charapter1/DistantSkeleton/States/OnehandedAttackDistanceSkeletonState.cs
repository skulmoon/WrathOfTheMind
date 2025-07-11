using Godot;
using System;

public partial class OnehandedAttackDistanceSkeletonState : Node, IDistantSkeletonState
{
    private DistantSkeleton _enemy;
    private Timer _endTimer = new Timer { Autostart = true, OneShot = true, WaitTime = 3 };
    private Timer _attackTimer = new Timer { Autostart = true, OneShot = true, WaitTime = 2 };

    public OnehandedAttackDistanceSkeletonState(DistantSkeleton enemy)
    {
        _enemy = enemy;
        AddChild(_attackTimer);
        AddChild(_endTimer);
        _attackTimer.Timeout += () => _enemy.Attack(new TestEnemyAttack1(_enemy.Damage, 2, _enemy.GlobalPosition, Global.SceneObjects.Player.GlobalPosition));
        _endTimer.Timeout += () => {
            _enemy.State =  new ZerohandedMovementDistantSkeletonState(_enemy);
        };
    }

    public string GetAnimation() =>
        "onehanded_attack";
}
