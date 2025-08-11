using Godot;
using System;

public partial class EnemyPositionsControlNode : Node
{
	private EnemyPositionsControl _control;

    public int SlotNumber { get; private set; }
    public Enemy Enemy { get; private set; }

    public EnemyPositionsControlNode(EnemyPositionsControl control, int number, Enemy enemy)
    {
        _control = control;
        SlotNumber = number;
        Enemy = enemy;
    }

    public Vector2 GetPosition() =>
        _control.Positions[SlotNumber].GlobalPosition;
}
