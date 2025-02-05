using Godot;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System;

public class SceneObjects
{
    private Player _player;
    private DialogPanel _panel;

    private Action<Node> _onPlayerChanged;
    private Action<Node> _onDialoguePanelChanged;

    public Player Player { get => _player; set => _player = (Player)SetObject(value, ref _onPlayerChanged); }
    public DialogPanel DialoguePanel { get => _panel; set => _panel = (DialogPanel)SetObject(value, ref _onDialoguePanelChanged); }
    public TradeMenu TradeMenu { get; set; }
    public List<NPC> Npcs { get; set; } = new List<NPC>();
    public InventoryMenu InventoryMenu { get; set; } 

    public event Action<Node> OnPlayerChanged { add => Subscribe(ref _onPlayerChanged, value, _player); remove => _onPlayerChanged -= value; }
    public event Action<Node> OnDialoguePanelChanged { add => Subscribe(ref _onDialoguePanelChanged, value, _player); remove => _onDialoguePanelChanged -= value; }

    public void TakePlayerSettings(Player player)
    {
        player.Position = Global.Settings.PlayerSettings.CurrentPosition;
        player.TargetPosition = Global.Settings.PlayerSettings.CurrentTargetPosition;
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
