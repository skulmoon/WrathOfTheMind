using Godot;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System;

public class SceneObjects
{
    private Storage _storage;
    private Player _player;
    private DialogPanel _panel;
    private Location _location;

    private Action<Storage> _storageReady;
    private Action<Player> _playerChanged;
    private Action<DialogPanel> _dialoguePanelChanged;
    private Action<Location> _locationChanged;

    public Player Player { get => _player; set => _player = SetObject(value, ref _playerChanged); }
    public DialogPanel DialoguePanel { get => _panel; set => _panel = SetObject(value, ref _dialoguePanelChanged); }
    public TradeMenu TradeMenu { get; set; }
    public Storage Storage { get => _storage; set => _storage = SetObject(value, ref _storageReady); }
    public List<NPC> Npcs { get; set; } = new List<NPC>();
    public List<Enemy> Enemies { get; set; } = new List<Enemy>();
    public InventoryMenu InventoryMenu { get; set; } 
    public Location Location { get => _location; set => _location = SetObject(value, ref _locationChanged); }


    public event Action<Player> PlayerChanged { add => Subscribe(ref _playerChanged, value, _player); remove => _playerChanged -= value; }
    public event Action<DialogPanel> DialoguePanelChanged { add => Subscribe(ref _dialoguePanelChanged, value, _panel); remove => _dialoguePanelChanged -= value; }
    public event Action<Storage> StorageReady { add => Subscribe(ref _storageReady, value, _storage); remove => _storageReady -= value; }
    public event Action<Location> LocationChanged { add => Subscribe(ref _locationChanged, value, Location); remove => _locationChanged -= value; }

    private T SetObject<T>(T value, ref Action<T> handler)
    {
        if (value != null)
            handler?.Invoke(value);
        return value;
    }

    private void Subscribe<T>(ref Action<T> handler, Action<T> subscriber, T node)
    {
        handler += subscriber;
        if (node != null)
            subscriber.Invoke(node);
    }

    public void ChangeScene(string path)
    {
        Enemies.Clear();
        Npcs.Clear();
        Player = null;
        Location = null;
        InventoryMenu = null;
        Storage.GetTree().CallDeferred("change_scene_to_file", path);
    }
}
