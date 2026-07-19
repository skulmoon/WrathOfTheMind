using Godot;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

public partial class InventoryItems : CustomTextureRect
{
    const int FIRST_ACTIVE_ITEM = 16;
    const int ACTIVE_SHARDS_COUNT = 4;
    private PlayerInventory _playerInventory;
    private float CellYSize;

    public List<Cell> Cells { get; private set; } = new List<Cell>();
    [Export] public int CellInLine { get; set; } = 6;
    [Export] public int LineCount { get; set; } = 2;
    [Export] public ItemType Type { get; set; } = ItemType.Item;
    [Export] public int CellSize { get; set; } = 32;
    [Export] public int CellOffset { get; set; } = 1;
    [Export] public int BarriarOffset { get; set; } = 1;

    public override void _Ready()
    {
        base._Ready();
        Global.SceneObjects.PlayerChanged += TakePlayer;
    }

    public void TakePlayer(Node player)
    {
        _playerInventory = Global.Inventory;
        ShowInventory();
    }

    public override void _ExitTree() =>
        Global.SceneObjects.PlayerChanged -= TakePlayer;

    public void ShowInventory()
    {
        float pixelSize = Size.Y / TheoreticalYSize;
        float cellSize = pixelSize * CellSize;
        float cellOffset = pixelSize * CellOffset;
        float barriarOffset = pixelSize * BarriarOffset;
        float lineLenght = barriarOffset + (cellSize + cellOffset) * CellInLine;
        AddCells(pixelSize, cellSize, cellOffset, barriarOffset);
        if (Type == ItemType.Shard)
        {

            for (int i = 0; i < ACTIVE_SHARDS_COUNT; i++)
            {
                Cell cell = Cell.CreateCell(
                    new Vector2(i * ((lineLenght - (cellSize + cellOffset)) / (ACTIVE_SHARDS_COUNT - 1)), -(cellSize + barriarOffset) * 2),
                    new Vector2(cellSize, cellSize), this, FIRST_ACTIVE_ITEM + i
                );
                AddTexture(cell, barriarOffset);
                AddChild(cell);
            }
            StateCellMethods.CheckActiveShards();
        }
        else if (Type == ItemType.Armor)
        {
            Cell mainCell = Cell.CreateCell(
                new Vector2((lineLenght - cellSize) / 2, -Size.Y * 0.8f),
                new Vector2(cellSize, cellSize), this, FIRST_ACTIVE_ITEM
            );
            AddTexture(mainCell, barriarOffset);
            AddChild(mainCell);
        }
    }

    public float AddCells(float pixelSize, float cellSize, float cellOffset, float barriarOffset)
    {
        for (int j = 0; j < LineCount; j++)
        {
            for (int i = 0; i < CellInLine; i++)
            {
                Cell cell = Cell.CreateCell(
                    new Vector2(barriarOffset + (cellSize + cellOffset) * i, barriarOffset + (cellSize + cellOffset) * j), 
                    new Vector2(cellSize, cellSize), this, i + (j * CellInLine)
                );
                AddChild(cell);
                Cells.Add(cell);
            }
        }
        return cellSize;
    }

    public void AddTexture(Cell cell, float bufferSize)
    {
        TextureRect texture = new TextureRect()
        {
            Size = cell.Size + new Vector2(bufferSize * 2, bufferSize * 2),
            Position = cell.Position + new Vector2(-bufferSize, -bufferSize)
        };
        texture.Texture = GD.Load<Texture2D>("res://Data/Textures/Menu/Buttons/InventoryCell/CellSelected.png");
        AddChild(texture);
    }
}
