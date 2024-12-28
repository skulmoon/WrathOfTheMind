using Godot;
using System;

public abstract partial class EnemyFabric : Node
{
    [Export] public EnemyArea[] EnemyZones { get; set; }

    public abstract void Create();
}
