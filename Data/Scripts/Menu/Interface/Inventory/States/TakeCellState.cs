using Godot;
using static Godot.Control;

public partial class TakeCellState : Node, ICellState
{
    private Cell _cell;
    private Vector2 CursorPosition { get => _cell.GetViewport().GetMousePosition() - (_cell.Size / 2); }

    public TakeCellState(Cell cell)
    {
        Cell.TakeCell = cell;
        cell.TopLevel = true;
        cell.MouseFilter = MouseFilterEnum.Ignore;        
        _cell = cell;
        _cell.GlobalPosition = CursorPosition;
    }

    public override void _Process(double delta) =>
        _cell.GlobalPosition = _cell.GlobalPosition.Lerp(CursorPosition, 10 * (float)delta);

    public void Release(Cell cell)
    {
        Vector2 buffer = cell.GlobalPosition;
        cell.TopLevel = false;
        cell.GlobalPosition = buffer;
        cell.Disabled = true;
        if (Cell.EnteredMouseCell == null || cell.ItemType != Cell.EnteredMouseCell.ItemType)
            cell.State = new TeleportationCellState(cell);
        else
        {
            if (cell.ItemType == ItemType.Shard && ((cell.ItemNumber < 20 && cell.ItemNumber > 15) || (Cell.EnteredMouseCell.ItemNumber < 20 && Cell.EnteredMouseCell.ItemNumber > 15)))
                StateCellMethods.ReleasedActiveShard(cell);
            if ((Cell.EnteredMouseCell?.Item?.ID) == (cell?.Item?.ID) && Cell.EnteredMouseCell?.Item != null && cell?.Item != null && cell.ItemType == ItemType.Item)
            {
                cell.Item.Count = Cell.EnteredMouseCell.Item.Staked(cell.Item.Count);
                if (cell.Item.Count == 0)
                {
                    cell.ItemType.GetList()[cell.ItemNumber] = null;
                    cell.UpdateItem();
                }
                cell.UpdateItem();
                Cell.EnteredMouseCell.UpdateItem();
                cell.State = new TeleportationCellState(cell);
            }
            else
            {
                Global.SceneObjects.Player.Inventory.MovingItem(cell.ItemType, cell.ItemNumber, Cell.EnteredMouseCell.ItemNumber);
                cell.UpdateItem();
                Cell.EnteredMouseCell.UpdateItem();
                (cell.Position, Cell.EnteredMouseCell.Position) = (Cell.EnteredMouseCell.Position, cell.Position);
                Cell.EnteredMouseCell.Disabled = true;
                cell.State = new TeleportationCellState(cell);
                Cell.EnteredMouseCell.State = new MovingCellState(Cell.EnteredMouseCell);
            }
        }
    }

    public void ReleaseOne(Cell cell)
    {
        if (cell.Item.Count > 1)
            StateCellMethods.ReleaseOne(cell);
        else
            Release(cell);
    }
}
