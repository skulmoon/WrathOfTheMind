using Godot;
using System;
using System.Collections.Generic;
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
            List<Item> inventory = cell.ItemType.GetList();
            inventory[^1] = (Item)cell.Item.Clone();
            inventory[^1].Count /= 2;
            cell.Item.Count -= inventory[^1].Count;
            cell.Item = inventory[cell.ItemNumber];
            Cell cellHalf = new Cell(cell.StartPosition, cell.Size, cell.ItemInventory, inventory.Count - 1);
            cellHalf.State = new TakeHalfCellState(cellHalf, cell);
        }
        else
            cell.State = new TakeCellState(cell);
    }
}
