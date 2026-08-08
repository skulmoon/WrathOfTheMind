using Godot;
using System;

public partial class InventoryMenu : Control
{
    public InventoryMenu() =>
        Global.SceneObjects.InventoryMenu = this;

    public override void _Input(InputEvent @event)
    {
        if (Input.IsActionJustPressed("open_inventory") && (!Global.Variables.CutScene || Visible))
        {
            if (!Visible)
                ShowInventory();
            else
                HideInventory();
            Cell.ActiveShardCells?.ForEach(cell => cell.Visible = true);
        }
    }

    public void ShowInventory()
    {
        UIDark dark = GetNode<UIDark>("%MenuDark");
        Visible = true;
        this.ChangeAlphaModulate();
        Tween tween = CreateTween();
        tween.TweenProperty(dark, "CurrentDarkPower", 0.5f, 0.2);
        tween.TweenProperty(this, "modulate:a", 1, 0.2);
        Global.Variables.CutScene = true;
        GetTree().Paused = true;
    }

    public void HideInventory()
    {
        UIDark dark = GetNode<UIDark>("%MenuDark");
        this.ChangeAlphaModulate();
        Tween tween = CreateTween();
        tween.TweenProperty(dark, "CurrentDarkPower", 0f, 0.2);
        tween.TweenProperty(this, "modulate:a", 0, 0.2);
        tween.TweenCallback(new Callable(this, "Hide"));
        Global.Variables.CutScene = false;
        GetTree().Paused = false;
    }
}
