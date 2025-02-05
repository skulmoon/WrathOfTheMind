using Godot;
using System;
using System.Collections.Generic;

public partial class TradeArea : Area2D, IInteractionArea
{
	[Export] public TradeObject[] Objects { get; set; } 

	public void Interaction()
	{
		Global.SceneObjects.TradeMenu.Show(Objects);
		Global.SceneObjects.InventoryMenu.ShowInventory();
    }
}
