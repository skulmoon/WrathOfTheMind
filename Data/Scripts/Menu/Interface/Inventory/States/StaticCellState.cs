using Godot;
using System;
using static Godot.Control;

public partial class StaticCellState : Node, ICellState
{
    private Cell _cell;

    public StaticCellState(Cell cell)
    {
        Cell.TakeCell = null;
        _cell = cell;
        _cell.Disabled = false;
        _cell.MouseFilter = MouseFilterEnum.Stop;
    }

    public void Take(Cell cell)
    {
        cell.State = new TakeCellState(cell);
    }
}
