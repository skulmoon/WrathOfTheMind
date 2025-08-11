using Godot;
using System;
using System.Collections.Generic;

public partial class EnemyPositionsControl : Node2D
{
    private Node2D[] _positions;
    private List<EnemyPositionsControlNode> _nodes = new List<EnemyPositionsControlNode>();
    private Queue<Enemy> _queue = new Queue<Enemy>();

    [Export] public Node2D[] Positions 
    { 
        get => _positions;
        set
        {
            _positions = value;
            for (int i = 0; i <_positions.Length; i++)
            {
                _positions[i].Position = Vector2.FromAngle(Mathf.DegToRad(360 / Positions.Length * i)) * Global.Settings.GridSize;
            }
        } 
    }

    public void GetControlPositionNode(Enemy enemy)
    {
        int freeNumber = GetFreeNumber();
        if (freeNumber > -1)
        {
            enemy.PositionControl = new EnemyPositionsControlNode(this, freeNumber, enemy);
            _nodes.Add(enemy.PositionControl);
        }
        else
            _queue.Enqueue(enemy);
    }

    private int GetFreeNumber()
    {
        for (int i = 0; i < _positions.Length; i++)
        {
            if (_nodes.Find(x => x.SlotNumber == i) == null)
                return i;
        }
        return -1;
    }

    private void RemoveControlPositionNode(Enemy enemy)
    {
        _nodes.Remove(_nodes.Find(x => x.Enemy == enemy));
        if (_queue.Count > 0)
            GetControlPositionNode(_queue.Dequeue());
    }
}
