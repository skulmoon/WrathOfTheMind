using Godot;
using System;
using System.Collections.Generic;

public partial class ChangeControlElement : Control
{
    [Export] public CustomButton FirstButton { get; set; }
    [Export] public CustomButton SecondButton { get; set; }
    [Export] public string Action { get; set; }

    public override void _Ready()
	{
		CustomMinimumSize = new Vector2(0, ((Control)GetParent().GetParent()).Size.Y / 8.3f);
        var list = InputMap.ActionGetEvents(Action);
        UpdateVision();
        FirstButton.Pressed += () => GetNode<ActiveDialogWindow>("%ActiveDialogWindow").ChangeAction(Action, 1, this);
        SecondButton.Pressed += () => GetNode<ActiveDialogWindow>("%ActiveDialogWindow").ChangeAction(Action, 2, this);
    }

    public void UpdateVision()
    {
        var list = InputMap.ActionGetEvents(Action);
        FirstButton.Text = list.Count >= 1 ? GetKeyName((InputEventWithModifiers)list[0]) : string.Empty;
        SecondButton.Text = list.Count >= 2 ? GetKeyName((InputEventWithModifiers)list[1]) : string.Empty;
    }

    public string GetKeyName(InputEventWithModifiers @event)
    {
        if (@event is InputEventKey key)
            return key.Keycode.ToString();
        else if (@event is InputEventMouseButton mouseButton)
            return mouseButton.ButtonIndex.GetName();
        return string.Empty;
    }
}
