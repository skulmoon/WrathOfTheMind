using Godot;
using System;
using System.Drawing;
using static Godot.Control;

public partial class StaticCellState : Node, ICellState
{
    public StaticCellState(Cell cell)
    {
        Cell.TakeCell = null;
        cell.Disabled = false;
        cell.MouseFilter = MouseFilterEnum.Stop;
    }

    public void Take(Cell cell)
    {
        cell.State = new TakeCellState(cell);
    }

    public void TakeHalf(Cell cell)
    {
        if ((cell.Item?.Count ?? 0) > 1)
        {
            Global.SceneObjects.Player.Inventory.Items[24] = (Item)cell.Item.Clone();
            Global.SceneObjects.Player.Inventory.Items[24].Count /= 2;
            cell.Item.Count -= Global.SceneObjects.Player.Inventory.Items[24].Count;
            cell.Item = Global.SceneObjects.Player.Inventory.Items[cell.ItemNumber];
            Cell cellHalf = new Cell(cell.StartPosition, cell.Size, cell.ItemInventory, 24);
            cellHalf.State = new TakeHalfCellState(cellHalf, cell);
        }
        else
            cell.State = new TakeCellState(cell);
    }
}
