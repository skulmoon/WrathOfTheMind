using Godot;
using System;
using static Godot.Control;

public partial class TakeHalfCellState : Node, ICellState
{
    private Cell _parentCell;
    private Cell _childCell;

    public TakeHalfCellState(Cell childCell, Cell parentCell)
    {
        _childCell = childCell;
        _parentCell = parentCell;
        childCell.ItemInventory.AddChild(childCell);
        Cell.TakeCell = childCell;
        childCell.TopLevel = true;
        childCell.MouseFilter = MouseFilterEnum.Ignore;
    }

    public override void _Process(double delta) =>
        _childCell.GlobalPosition = GetViewport().GetMousePosition() - (_childCell.Size / 2);

    public void Release(Cell cell)
    {
        cell.TopLevel = false;
        cell.GlobalPosition = GetViewport().GetMousePosition() - (cell.Size / 2);
        cell.Disabled = true;
        GD.Print(12);
        if (Cell.EnteredMouseCell != null)
        {
            if ((Cell.EnteredMouseCell?.Item?.ID) == (cell?.Item?.ID) && Cell.EnteredMouseCell?.Item != null)
            {
                var item = Cell.EnteredMouseCell.Item;
                var item2 = cell.Item;
                item2.Count = item.Staked(item2.Count);
                Cell.EnteredMouseCell.Item = item;
                if (item2.Count != 0)
                {
                    _parentCell.Item.Count += item2.Count;
                    _parentCell.Item = Global.SceneObjects.Player.Inventory.Items[_parentCell.ItemNumber];
                }
                cell.State = new DisappearanceCellState(cell);
            }
            else if (Cell.EnteredMouseCell?.Item == null)
            {
                Global.SceneObjects.Player.Inventory.MovingItem(ItemType.Item, cell.ItemNumber, Cell.EnteredMouseCell.ItemNumber);
                cell.Item = Global.SceneObjects.Player.Inventory.Items[cell.ItemNumber];
                Cell.EnteredMouseCell.Item = Global.SceneObjects.Player.Inventory.Items[Cell.EnteredMouseCell.ItemNumber];
                Cell.EnteredMouseCell.StartPosition = Cell.EnteredMouseCell.Position;
                Vector2 buffer = Cell.EnteredMouseCell.Position;
                Cell.EnteredMouseCell.Position = cell.Position;
                cell.Position = buffer;
                Cell.EnteredMouseCell.Disabled = true;
                cell.State = new DisappearanceCellState(cell);
                Cell.EnteredMouseCell.State = new MovingCellState(Cell.EnteredMouseCell);
            }
        }
        else
        {
            _parentCell.Item.Count += cell.Item.Count;
            _parentCell.State = new TeleportationCellState(_parentCell);
            cell.State = new DisappearanceCellState(cell);
        }
    }

    public void ReleaseOne(Cell cell)
    {
        StateCellMethods.ReleaseOne(cell);
        if (cell.Item == null)
        {

            cell.State = new DisappearanceCellState(cell);
        }
    }
}
