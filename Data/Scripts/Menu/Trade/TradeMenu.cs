using Godot;
using System;
using System.Collections.Generic;
using System.ComponentModel;

public partial class TradeMenu : Control
{
	private List<Button> _tradeButtons = new List<Button>();

	public TradeMenu()
	{
		Global.SceneObjects.TradeMenu = this;
	}

	public void Show(TradeObject[] objects)
	{
		if (!Visible)
		{
			VBoxContainer container = GetNode<VBoxContainer>("VBoxContainer/VBoxContainer");
            TradeButton tradeButton;
			for (int i = 0; i < objects.Length; i++)
			{
				tradeButton = (TradeButton)GD.Load<PackedScene>("res://Data/Scenes/Menu/Trade/TradeButton.tscn").Instantiate();
				tradeButton.TradeObject = objects[i];
				container.AddChild(tradeButton);
                _tradeButtons.Add(tradeButton);
                tradeButton.Pressed += TryBuy;
            }
            TopLevel = true;
            Visible = true;
		}
    }

	public void Close()
	{
		VBoxContainer buttonContainer = GetNode<VBoxContainer>("VBoxContainer/VBoxContainer");
		Global.SceneObjects.InventoryMenu.Visible = false;
		for (int i = 0; i < _tradeButtons.Count; i++)
			buttonContainer.RemoveChild(_tradeButtons[i]);
		_tradeButtons.Clear();
        TopLevel = false;
        Visible = false;
    }

	public void TryBuy()
	{

	}
}
