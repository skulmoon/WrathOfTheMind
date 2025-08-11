using Godot;
using System;

public interface IEnemyState
{
    public void NoticePlayer() { }
    public string GetAnimation();
}
