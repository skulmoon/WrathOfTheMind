using Godot;
using System;

public partial class Cell : Button
{
    private ICellState _state;
    private Item _item;

    static public Cell TakeCell { get; set; }
    static public Cell EnteredMouseCell { get; set; }

    public InventoryItems ItemInventory { get; private set; }
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
        ItemNumber = itemNumber;
        Item = Global.SceneObjects.Player.Inventory.Items[itemNumber];
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
        GD.Print(1);
        EnteredMouseCell = this;
    }

    public void OnExited()
    {
        EnteredMouseCell = null;
    }

    public void UpdateItem() =>
        Item = Global.SceneObjects.Player.Inventory.Items[ItemNumber];
}

public interface ICellState
{
    public void Take(Cell cell) { }
    public void TakeHalf(Cell cell) { }
    public void Release(Cell cell) { }
    public void ReleaseOne(Cell cell) { }
}
