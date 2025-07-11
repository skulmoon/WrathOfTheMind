using Godot;
using System;

public partial class FistAttackMeleSkeletonState : Node, IMeleSkeletonState
{
    private MeleSkeleton _enemy;

    public FistAttackMeleSkeletonState(MeleSkeleton enemy)
    {
        _enemy = enemy;
    }
    public override void _PhysicsProcess(double delta)
    {
        _enemy.Attack(new FistAttackMeleSkeleton(_enemy.Damage, _enemy.GlobalPosition, Global.SceneObjects.Player.GlobalPosition));
    }

    public void Chase()
    {
        _enemy.State = new FistMovementMeleSkeletonState(_enemy);
    }

    public string GetAnimation() =>
        "fist_attack";
}
