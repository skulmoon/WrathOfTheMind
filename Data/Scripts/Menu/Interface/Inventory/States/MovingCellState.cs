using Godot;

internal partial class MovingCellState : Node, ICellState
{
    private Cell _cell;
    private int _speed = 600;

    public MovingCellState(Cell cell)
    {
        _cell = cell;
        _cell.ButtonPressed = false;
    }

    public override void _Process(double delta)
    {
        Vector2 direction = (_cell.StartPosition - _cell.Position).Normalized();
        if (_cell.Position.DistanceTo(_cell.StartPosition) < _speed * (float)delta)
        {
            _cell.Position = _cell.StartPosition;
            _cell.StartPosition = _cell.StartPosition;
            _cell.State = new StaticCellState(_cell);
        }
        else
            _cell.Position += direction * _speed * (float)delta;
    }
}
