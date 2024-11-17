using Godot;
using static Godot.Control;

public partial class TakeCellState : Node, ICellState
{
    private Vector2 _startPosition;
    private Cell _cell;

    public TakeCellState(Cell cell)
    {
        _cell = cell;
        Cell.TakeCell = _cell;
        _startPosition = _cell.Position;
        _cell.TopLevel = true;
        _cell.MouseFilter = MouseFilterEnum.Ignore;
    }

    public override void _Process(double delta)
    {
        _cell.GlobalPosition = GetViewport().GetMousePosition() - (_cell.Size / 2);
    }

    public void Release(Cell cell)
    {
        cell.TopLevel = false;
        cell.GlobalPosition = GetViewport().GetMousePosition() - (_cell.Size / 2);
        cell.Disabled = true;
        if (Cell.EnteredMouseCell != null)
        {
            if ((Cell.EnteredMouseCell?.Item?.ID) == (cell?.Item?.ID) && Cell.EnteredMouseCell?.Item != null && cell?.Item != null)
            {
                var item = Cell.EnteredMouseCell.Item;
                var item2 = cell.Item;
                item2.Count = item.Staked(item2.Count);
                Cell.EnteredMouseCell.Item = item;
                if (item2.Count == 0)
                {
                    Global.SceneObjects.Player.Inventory.Items[cell.ItemNumber] = null;
                    cell.Item = null;
                }
                else
                    cell.Item = item2;
                cell.State = new TeleportationCellState(cell);
            }
            else
            {
                Global.SceneObjects.Player.Inventory.MovingItem(ItemType.Item, cell.ItemNumber, Cell.EnteredMouseCell.ItemNumber);
                cell.Item = Global.SceneObjects.Player.Inventory.Items[cell.ItemNumber];
                Cell.EnteredMouseCell.Item = Global.SceneObjects.Player.Inventory.Items[Cell.EnteredMouseCell.ItemNumber];
                Cell.EnteredMouseCell.StartPosition = Cell.EnteredMouseCell.Position;
                Vector2 buffer = Cell.EnteredMouseCell.Position;
                Cell.EnteredMouseCell.Position = cell.Position;
                cell.Position = buffer;
                Cell.EnteredMouseCell.Disabled = true;
                cell.State = new TeleportationCellState(cell);
                Cell.EnteredMouseCell.State = new MovingCellState(Cell.EnteredMouseCell);
            }
        }
        else
        {
            cell.State = new TeleportationCellState(cell);
        }
    }
}
