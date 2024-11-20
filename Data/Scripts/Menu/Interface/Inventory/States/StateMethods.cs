using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Godot;

partial class StateCellMethods : Node
{
    static public void ReleaseOne(Cell cell)
    {
        if (Cell.EnteredMouseCell != null)
        {
            if ((Cell.EnteredMouseCell?.Item?.ID) == (cell?.Item?.ID) && Cell.EnteredMouseCell?.Item != null)
            {
                if (Cell.EnteredMouseCell?.Item.Count < Cell.EnteredMouseCell?.Item.MaxCount)
                {
                    Cell.EnteredMouseCell.Item.Count++;
                    DecrementItem(cell);
                    Cell.EnteredMouseCell.UpdateItem();
                    cell.UpdateItem();
                }
            }
            else if (Cell.EnteredMouseCell.Item == null && cell?.Item != null)
            {
                Global.SceneObjects.Player.Inventory.Items[Cell.EnteredMouseCell.ItemNumber] = (Item)cell.Item.Clone();
                Global.SceneObjects.Player.Inventory.Items[Cell.EnteredMouseCell.ItemNumber].Count = 1;
                DecrementItem(cell);
                Cell.EnteredMouseCell.UpdateItem();
                cell.UpdateItem();
            }
        }
    }

    static void DecrementItem(Cell cell)
    {
        if (cell.Item.Count > 1)
            cell.Item.Count--;
        else if (cell.Item.Count <= 1)
        {
            cell.Item = null;
            Global.SceneObjects.Player.Inventory.Items[cell.ItemNumber] = null;
            cell.TopLevel = false;
            cell.GlobalPosition = cell.GetViewport().GetMousePosition() - (cell.Size / 2);
            cell.State = new TeleportationCellState(cell);
        }
    }
}

