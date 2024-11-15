using Godot;
using System;
using static Godot.WebSocketPeer;

public partial class Cell : Button
{
    private InventoryItems _itemInventoryPresenter;
    private ICellState _state;
    private Item _item;

    static public Cell TakeCell { get; set; }
    static public Cell EnteredMouseCell { get; set; }

    public int ItemNumber { get; private set; }
    public Vector2 StartPosition { get; set; }
    public Item Item
    {
        get => _item;
        set
        {
            if (_item != null)
            {
                RemoveChild(GetNode("1"));
                RemoveChild(GetNode("2"));
            }
            if (value != null)
            {
                Sprite2D sprite2D = new Sprite2D();
                sprite2D.Texture = ResourceLoader.Load<Texture2D>("res://Data/Textures/Items/Items/" + value.Name + ".png");
                Control control = new Control();
                control.AnchorTop = 0.5f;
                control.AnchorLeft = 0.5f;
                control.AddChild(sprite2D);
                Label label = new Label();
                label.Text = value.Count.ToString();
                label.SetAnchorsPreset(LayoutPreset.BottomRight);
                label.GrowHorizontal = GrowDirection.Begin;
                label.GrowVertical = GrowDirection.Begin;
                control.Name = "1";
                label.Name = "2";
                AddChild(label);
                AddChild(control);
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
            AddChild((Node)_state);
        }
    }

    public Cell(Vector2 startPosition, Vector2 size, InventoryItems itemInventoryPresenter, int itemNumber)
    {
        StartPosition = startPosition;
        Position = startPosition;
        Size = size;
        _itemInventoryPresenter = itemInventoryPresenter;
        ItemNumber = itemNumber;
    }

    public override void _Ready()
    {
        Node state = new StaticCellState(this);
        State = (ICellState)state;
        Pressed += OnButtonPressed;
        MouseEntered += OnEntered;
        MouseExited += OnExited;
    }

    public override void _Input(InputEvent @event)
    {
        if (Input.IsActionJustPressed("take_or_release_item") && TakeCell == this)
        {
            if (TakeCell == this)
                State.Release(this);
            else if (_state is StaticCellState && EnteredMouseCell == this && TakeCell == null)
                State.Take(this);
        }
        else if (Input.IsMouseButtonPressed(MouseButton.Right) && TakeCell == this)
        {
            Global.SceneObjects.Player.Inventory.Items[25] = (Item)Item.Duplicate(true);
            Global.SceneObjects.Player.Inventory.Items[25].Count /= 2;
            Item.Count -= Global.SceneObjects.Player.Inventory.Items[25].Count;
            TakeCell = new Cell(StartPosition, Size, _itemInventoryPresenter, 25);
        }
    }

    public void OnButtonPressed()
    {

    }

    public void OnEntered()
    {
        EnteredMouseCell = this;
    }
    public void OnExited()
    {
        EnteredMouseCell = null;
    }
}

public interface ICellState
{
    public void Take(Cell cell) { }
    public void Release(Cell cell) { }
}
