using Godot;
using System;
using static Godot.Control;

public partial class TakeHalfCellState : Node, ICellState
{
    private Cell _parentCell;
    private Cell _childCell;
    private Vector2 CursorPosition { get => _childCell.GetViewport().GetMousePosition() - (_childCell.Size / 2); }

    public TakeHalfCellState(Cell childCell, Cell parentCell)
    {
        _childCell = childCell;
        _parentCell = parentCell;
        childCell.ItemInventory.AddChild(childCell);
        Cell.TakeCell = childCell;
        childCell.TopLevel = true;
        _childCell.GlobalPosition = CursorPosition;
        childCell.MouseFilter = MouseFilterEnum.Ignore;
    }

    public override void _Process(double delta) =>
        _childCell.GlobalPosition = _childCell.GlobalPosition.Lerp(CursorPosition, 10 * (float)delta);

    public void Release(Cell cell)
    {
        Vector2 buffer = cell.GlobalPosition;
        cell.TopLevel = false;
        cell.GlobalPosition = buffer;
        cell.Disabled = true;
        if (Cell.EnteredMouseCell == null || cell.ItemType != Cell.EnteredMouseCell.ItemType)
        {
            _parentCell.Item.Count += cell.Item.Count;
            _parentCell.State = new TeleportationCellState(_parentCell);
            cell.State = new DisappearanceCellState(cell);
        }
        else
        {
            if ((Cell.EnteredMouseCell?.Item?.ID) == (cell?.Item?.ID) && Cell.EnteredMouseCell?.Item != null)
            {
                cell.Item.Count = Cell.EnteredMouseCell.Item.Staked(cell.Item.Count);
                Cell.EnteredMouseCell.UpdateItem();
                if (cell.Item.Count != 0)
                {
                    _parentCell.Item.Count += cell.Item.Count;
                    _parentCell.UpdateItem();
                }
                cell.State = new DisappearanceCellState(cell);
            }
            else if (Cell.EnteredMouseCell?.Item == null)
            {
                Global.Inventory.MovingItem(ItemType.Item, cell.ItemNumber, Cell.EnteredMouseCell.ItemNumber);
                cell.UpdateItem();
                Cell.EnteredMouseCell.UpdateItem();
                (cell.Position, Cell.EnteredMouseCell.Position) = (Cell.EnteredMouseCell.Position, cell.Position);
                Cell.EnteredMouseCell.Disabled = true;
                cell.State = new DisappearanceCellState(cell);
                Cell.EnteredMouseCell.State = new MovingCellState(Cell.EnteredMouseCell);
            }
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
