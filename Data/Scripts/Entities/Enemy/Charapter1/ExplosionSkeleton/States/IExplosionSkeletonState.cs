using Godot;
using System;

public interface IExplosionSkeletonState : IEnemyState
{
    public void Attack() { }
    public void Chase() { }
}
