using Godot;

public partial class CellState : Node
{
    public virtual void Take(Cell cell) { }
    public virtual void TakeHalf(Cell cell) { }
    public virtual void Release(Cell cell) { }
    public virtual void ReleaseOne(Cell cell) { }
}
