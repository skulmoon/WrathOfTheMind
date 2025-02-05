using Godot;
using System;

public partial class InventoryMenu : Control
{
    public InventoryMenu() =>
        Global.SceneObjects.InventoryMenu = this;

    public override void _Input(InputEvent @event)
    {
        if (Input.IsActionJustPressed("open_inventory") && (!Global.Settings.CutScene || Visible))
        {
            Visible = !Visible;
            Global.Settings.CutScene = Visible;
            Cell.ActiveShardCells.ForEach(cell => cell.Visible = true);
        }
    }

    public void ShowInventory()
    {
        Visible = true;
        Cell.ActiveShardCells.ForEach(cell => cell.Visible = false);
    }
}
