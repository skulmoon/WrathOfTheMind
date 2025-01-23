public interface ICellState
{
    public void Take(Cell cell) { }
    public void TakeHalf(Cell cell) { }
    public void Release(Cell cell) { }
    public void ReleaseOne(Cell cell) { }
}
