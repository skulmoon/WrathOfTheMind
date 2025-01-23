using Godot;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using static Godot.OpenXRHand;

public partial class InventoryItems : Control
{
    const int FIRST_ACTIVE_ITEM = 16;

	private PlayerInventory _playerInventory;
	private int lineCount = 0;
    [Export] public int SellInLine { get; set; } = 6;
    [Export] public ItemType Type { get; set; } = ItemType.Item;

    public override void _Ready()
    {
        Global.SceneObjects.OnPlayerChanged += TakePlayer;
    }

    public void TakePlayer(Node player)
    {
        _playerInventory = ((Player)player).Inventory;
        ShowInventory();
    }

    public void ShowInventory()
    {
        AddCells();
        float cellSize = AddCells();
        if (Type == ItemType.Shard)
        {
            float angelDistance = 2 * MathF.PI / 3;
            Cell mainCell = new Cell(new Vector2((Size.X - cellSize) / 2, (-Size.Y - cellSize) / 2), new Vector2(cellSize, cellSize), this, FIRST_ACTIVE_ITEM);
            Label label = new Label
            {
                Text = 0.ToString()
            };
            mainCell.AddChild(label);
            AddChild(mainCell);
            for (int i = 0; i < 3; i++)
            {
                Cell cell = new Cell(mainCell.Position + new Vector2(MathF.Cos(i * angelDistance - MathF.PI / 2), MathF.Sin(i * angelDistance - MathF.PI / 2)) * Size.X / 2.5f, new Vector2(cellSize, cellSize), this, FIRST_ACTIVE_ITEM + i + 1);
                label = new Label
                {
                    Text = i.ToString()
                };
                cell.AddChild(label);
                AddChild(cell);
            }
            StateCellMethods.CheckActiveShards();
        }
        else if (Type == ItemType.Armor)
        {
        }
    }

    public float AddCells()
    {
        float cellSize = Size.X / SellInLine;
        for (int i = 0; i < SellInLine; i++)
        {
            Cell cell = new Cell(new Vector2(i * cellSize, lineCount * cellSize), new Vector2(cellSize, cellSize), this, i + (lineCount * SellInLine));
            Label label = new Label
            {
                Text = (i + lineCount * SellInLine).ToString()
            };
            cell.AddChild(label);
            AddChild(cell);
        }
        lineCount++;
        return cellSize;
    }
}
