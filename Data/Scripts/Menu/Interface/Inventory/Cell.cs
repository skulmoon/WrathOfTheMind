using Godot;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

public partial class Cell : Button
{
    private ICellState _state;
    private Item _item;
    private Sprite2D _sprite;
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

    public ICellState State {
        get => _state;
        set 
        {
            if (_state != null)
                RemoveChild((Node)_state);
            _state = value;
            if (value != null)
                AddChild((Node)_state);
        }
    }

    public Cell(Vector2 startPosition, Vector2 size, InventoryItems itemInventoryPresenter, int itemNumber)
    {
        StartPosition = startPosition;
        Position = startPosition;
        Size = size;
        ItemInventory = itemInventoryPresenter;
        ItemType = itemInventoryPresenter.Type;
        _sprite = new Sprite2D();
        Control control = new Control
        {
            AnchorTop = 0.5f,
            AnchorLeft = 0.5f
        };
        control.AddChild(_sprite);
        _label = new Label();
        _label.SetAnchorsPreset(LayoutPreset.BottomRight);
        _label.GrowHorizontal = GrowDirection.Begin;
        _label.GrowVertical = GrowDirection.Begin;
        AddChild(_label);
        AddChild(control);
        ItemNumber = itemNumber;
        UpdateItem();
        if (ItemType == ItemType.Shard && itemNumber < 20 && itemNumber > 15)
        {
            if (itemNumber == 16)
                ActiveShardCells[0] = this;
            else
                for (int i = 0; i < 3;  i++)
                    if (ActiveShardCells[i + 1] == null)
                    {
                        ActiveShardCells[i + 1] = this;
                        break;
                    }
        }
    }

    public override void _Ready()
    {
        Node state = new StaticCellState(this); 
        State = (ICellState)state;
        MouseEntered += OnEntered;
        MouseExited += OnExited;
    }

    public override void _Input(InputEvent @event)
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

    public void UpdateItem() =>
        Item = ItemType.GetList()[ItemNumber];
}
