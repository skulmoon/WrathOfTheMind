using Godot;
using System;

public partial class CalmExplosionSkeletonState : Node2D, IExplosionSkeletonState
{
    private ExplosionSkeleton _enemy;

    public CalmExplosionSkeletonState(ExplosionSkeleton enemy)
    {
        _enemy = enemy;
    }

    public void Attack()
    {
        ((CircleShape2D)_enemy.Trigger.Shape).Radius = 60;
        _enemy.State = new MovementExplosionSkeletonState(_enemy);
        _enemy.NoticePlayer();
    }

    public void NoticePlayer() =>
        Attack();

    public string GetAnimation() =>
        "shovel_movement";
}
