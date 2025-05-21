using Godot;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System;

public class SceneObjects
{
    private Player _player;
    private DialogPanel _panel;
    private Storage _storage;
    private Location _location;

    private Action<Node> _storageReady;
    private Action<Node> _playerChanged;
    private Action<Node> _dialoguePanelChanged;
    private Action<Node> _locationChanged;

    public Player Player { get => _player; set => _player = (Player)SetObject(value, ref _playerChanged); }
    public DialogPanel DialoguePanel { get => _panel; set => _panel = (DialogPanel)SetObject(value, ref _dialoguePanelChanged); }
    public TradeMenu TradeMenu { get; set; }
    public Storage Storage { get => _storage; set => _storage = (Storage)SetObject(value, ref _storageReady); }
    public List<NPC> Npcs { get; set; } = new List<NPC>();
    public InventoryMenu InventoryMenu { get; set; } 
    public Location Location { get => _location; set => _location = (Location)SetObject(value, ref _locationChanged); }


    public event Action<Node> PlayerChanged { add => Subscribe(ref _playerChanged, value, _player); remove => _playerChanged -= value; }
    public event Action<Node> DialoguePanelChanged { add => Subscribe(ref _dialoguePanelChanged, value, _player); remove => _dialoguePanelChanged -= value; }
    public event Action<Node> StorageReady { add => Subscribe(ref _storageReady, value, _storage); remove => _storageReady -= value; }
    public event Action<Node> LocationChanged { add => Subscribe(ref _locationChanged, value, Location); remove => _locationChanged -= value; }

    public void TakePlayerSettings(Player player)
    {
        player.Position = Global.Settings.PlayerSettings.CurrentPosition;
        //player.Inventory.Items = Global.Settings.PlayerSettings.Items;
        //player.Inventory.Armors = Global.Settings.PlayerSettings.Armors;
        //player.Inventory.Shards = Global.Settings.PlayerSettings.Shards;
    }

    private Node SetObject(Node value, ref Action<Node> handler)
    {
        handler?.Invoke(value);
        return value;
    }

    private void Subscribe(ref Action<Node> handler, Action<Node> subscriber, Node node)
    {
        handler += subscriber;
        if (node != null)
            subscriber.Invoke(node);
    }

    public void ResetPLayer() =>
        _player = null;
}
