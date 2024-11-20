using Godot;
using System;

public partial class DisappearanceCellState : Node, ICellState
{
	private Cell _cell;
    private int _speed = 10;

    public DisappearanceCellState(Cell cell) =>
		_cell = cell;

    public override void _Process(double delta)
    {
        delta *= _speed;
        if (_cell.Modulate.A - (float)delta > 0)
        {
            Color color = _cell.Modulate;
            color.A -= (float)delta;
            _cell.Modulate = color;
        }
        else
        {
            Cell.TakeCell = null;
            Global.SceneObjects.Player.Inventory.Items[_cell.ItemNumber] = null;
            _cell.State = null;
            _cell.ItemInventory.RemoveChild(_cell);
            _cell = null;
        }
    }
}
