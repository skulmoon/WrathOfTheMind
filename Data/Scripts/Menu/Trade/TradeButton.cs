using Godot;
using System;

public partial class TradeButton : Button
{
    private PlayerInventory _inventory;
    private TradeObject _tradeObject;

    public TradeObject TradeObject 
    { 
        get => _tradeObject; 
        set 
        {
            GetNode<TextureRect>("TextureRect").Texture = GD.Load<Texture2D>($"res://Data/Textures/Items/{value.Type}s/{value.Item.Name}.png");
            GetNode<Label>("Label").Text = value.Price.ToString();
            _tradeObject = value;
        }
    }

    public TradeButton()
    {
        Pressed += OnPressed;
        Global.SceneObjects.PlayerChanged += TakePlayer;
        CustomMinimumSize = new Vector2(0, 50);
    }

    public void TakePlayer(Node player) =>
        _inventory = ((Player)player).Inventory;

    public void OnPressed()
    {
        if (_inventory.Scruples >= TradeObject.Price && _inventory.TakeItem((Item)TradeObject.Item.Clone()))
            _inventory.Scruples -= TradeObject.Price;
    }
}
