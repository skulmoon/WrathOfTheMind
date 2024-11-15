using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Godot.Control;


internal partial class MovingCellState : Node, ICellState
{
    private Cell _cell;
    private double _delta;
    private Vector2 _targetPosition;
    private int _speed = 200;

    public MovingCellState(Cell cell)
    {
        _cell = cell;
        _cell.ButtonPressed = false;
        _delta = 0.0;
        _targetPosition = _cell.StartPosition;
    }

    public override void _Process(double delta)
    {
        Vector2 direction = (_targetPosition - _cell.Position).Normalized();
        if (_cell.Position.DistanceTo(_targetPosition) < _speed * (float)delta)
        {
            _cell.Position = _targetPosition;
            _cell.StartPosition = _targetPosition;
            _cell.State = new StaticCellState(_cell);
        }
        else
            _cell.Position += direction * 200 * (float)delta;
    }
}
