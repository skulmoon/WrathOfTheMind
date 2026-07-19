using Godot;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;

public partial class Cell : CustomButton
{
    private CellState _state;
    private Item _item;
    private TextureRect _sprite;
    private Label _label;

    static public Cell TakeCell { get; set; }
    static public Cell EnteredMouseCell { get; set; }
    static public List<Cell> ActiveShardCells { get; set; } = new List<Cell>() { null, null, null, null };
    static public Timer TakeTimer { get; set; }

    public InventoryItems ItemInventory { get; private set; }
    public int ItemNumber { get; private set; }
    public ItemType ItemType { get; private set; }
    public Vector2 StartPosition { get; set; }
    public Item Item
    {
        get => _item;
        set
        {
            if (value != null)
            {
                _sprite.Visible = true;
                _sprite.Texture = ResourceLoader.Load<Texture2D>($"res://Data/Textures/Items/{ItemType}s/{value.Name}.png");
                _label.Text = value.Count != 1 ? value.Count.ToString() : string.Empty;
            }
            else
            {
                _sprite.Visible = false;
                _label.Text = string.Empty;
            }
            _item = value;
        }
    }

    public CellState State {
        get => _state;
        set 
        {
            if (_state != null)
                RemoveChild(_state);
            _state = value;
            if (value != null)
                AddChild(_state);
        }
    }

    public Cell(Vector2 startPosition, Vector2 size, InventoryItems itemInventoryPresenter, int itemNumber) : this()
    {
        Initialized(startPosition, size, itemInventoryPresenter, itemNumber);
    }

    public Cell()
    {
        _sprite = new TextureRect();
        _sprite.SetAnchorsPreset(LayoutPreset.FullRect);
        _sprite.MouseFilter = MouseFilterEnum.Ignore;
        AddChild(_sprite);
        _label = new Label();
        _label.SetAnchorsPreset(LayoutPreset.BottomRight);
        _label.GrowHorizontal = GrowDirection.Begin;
        _label.GrowVertical = GrowDirection.Begin;
        AddChild(_label);
    }

    public override void _Ready()
    {
        Node state = new StaticCellState(this); 
        State = (CellState)state;
        MouseEntered += OnEntered;
        MouseExited += OnExited;
    }

    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed("take_or_release_item"))
        {
            if (TakeCell == this)
                State.Release(this);
            else if (_state is StaticCellState && EnteredMouseCell == this && TakeCell == null)
                State.Take(this);
        }
        else if (Input.IsActionJustPressed("manipulation_with_item"))
        {
            if (TakeCell == this)
                State.ReleaseOne(this);
            else if (_state is StaticCellState && EnteredMouseCell == this && TakeCell == null)
                State.TakeHalf(this);
        }
        base._Process(delta);
    }

    public void OnEntered()
    {
        if (!Disabled)
            EnteredMouseCell = this;
    }

    public void OnExited()
    {
        EnteredMouseCell = null;
    }
 
    public void UpdateItem()
    {
        Item = ItemType.GetList()[ItemNumber];
    }

    public void Initialized(Vector2 startPosition, Vector2 size, InventoryItems itemInventoryPresenter, int itemNumber)
    {
        StartPosition = startPosition;
        Position = startPosition;
        Size = size;
        ItemInventory = itemInventoryPresenter;
        ItemType = itemInventoryPresenter.Type;
        ItemNumber = itemNumber;
        UpdateItem();
        if (ItemType == ItemType.Shard && itemNumber < 20 && itemNumber > 15)
            ActiveShardCells[itemNumber - 16] = this;
    }

    public static Cell CreateCell(Vector2 startPosition, Vector2 size, InventoryItems itemInventoryPresenter, int itemNumber)
    {
        Cell cell = GD.Load<PackedScene>("res://Data/Scenes/Menu/Interface/Inventory/Cell.tscn").Instantiate<Cell>();
        cell.Initialized(startPosition, size, itemInventoryPresenter, itemNumber);
        return cell;
    }
}
