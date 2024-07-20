using Godot;
using System;

public partial class DialogPanel : NinePatchRect
{
	public override void _Ready()
	{
        Json fg = GD.Load<Json>("res://Data/Dialogs/Vitalik.json");
    }
}
