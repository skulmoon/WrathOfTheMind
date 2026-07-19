using Godot;
using System;

public interface IWalker
{
    public event Action<Vector2> ChangedDirection;
    public event Action<float> SpeedMultiperChanged;
}
