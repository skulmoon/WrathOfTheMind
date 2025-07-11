using Godot;
using System;

public interface IMeleSkeletonState : IEnemyState
{
    public void Attack() { }
    public void Chase() { }
    public void ThrowShovel() { }
}
