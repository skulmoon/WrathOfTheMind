using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal partial class TeleportationCellState : Node, ICellState
{
    private Cell _cell;
    private Vector2 _targetPosition;
    private bool IsChangePosition = false;
    private int _speed = 10;

    public TeleportationCellState(Cell cell)
    {
        _cell = cell;
        _targetPosition = cell.StartPosition;
    }

    public override void _Process(double delta)
    {
        delta *= _speed;
        if (!IsChangePosition && _cell.Modulate.A - (float)delta > 0)
        {
            Color color = _cell.Modulate;
            color.A -= (float)delta;
            _cell.Modulate = color;
        }
        else if (IsChangePosition && _cell.Modulate.A + (float)delta < 1)
        {
            Color color = _cell.Modulate;
            color.A += (float)delta;
            _cell.Modulate = color;
        }
        else if (IsChangePosition)
        {
            Color color = _cell.Modulate;
            color.A = 1;
            _cell.Modulate = color;
            _cell.State = new StaticCellState(_cell);
        }
        else
        {
            IsChangePosition = true;
            _cell.Position = _targetPosition;
            _cell.StartPosition = _targetPosition;
            _cell.Item = _cell.ItemType.GetList()[_cell.ItemNumber];
        }
    }
}

