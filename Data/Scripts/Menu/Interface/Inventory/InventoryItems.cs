using Godot;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

public partial class InventoryItems : Control
{
	const int CELLS_IN_LINE = 6;
    private List<Cell> Items = new List<Cell>();
	private int lineCount = 0;
	private PlayerInventory _playerInventory;

    public override void _Ready()
    {
        _playerInventory = Global.SceneObjects.Player.Inventory;
        ShowInventory();
    }

    public void ShowInventory()
    {
        AddCells();
        AddCells();
    }

    public void AddCells()
	{
		for (int i = 0; i < CELLS_IN_LINE; i++)
		{
			float cellSize = Size.X/CELLS_IN_LINE;
			var cell = new Cell(new Vector2(i * cellSize, lineCount * cellSize), new Vector2(cellSize, cellSize), this, i + lineCount * CELLS_IN_LINE);
			var label = new Label();
			label.Text = (i + lineCount * CELLS_IN_LINE).ToString();
			cell.AddChild(label);
            AddChild(cell);
        }
		lineCount++;
	}
}
