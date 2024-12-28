using Godot;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System;

public delegate void TakeNodeHandler(Node node);

public class SceneObjects
{
    private Player _player;
    private DialogPanel _panel;

    private TakeNodeHandler _takePlayer;
    private TakeNodeHandler _takeDialoguePanel;

    public Player Player { get => _player; set => _player = (Player)SetObject(value, ref _takePlayer); }
    public DialogPanel DialoguePanel { get => _panel; set => _panel = (DialogPanel)SetObject(value, ref _takeDialoguePanel); }
    public List<NPC> Npcs { get; set; } = new List<NPC>();

    public event TakeNodeHandler TakePlayer { add => Subscribe(ref _takePlayer, value, _player); remove => _takePlayer -= value; }
    public event TakeNodeHandler TakeDialoguePanel { add => Subscribe(ref _takeDialoguePanel, value, _player); remove => _takeDialoguePanel -= value; }

    public void TakePlayerSettings(Player player)
    {
        player.Position = Global.Settings.PlayerSettings.CurrentPosition;
        player.TargetPosition = Global.Settings.PlayerSettings.CurrentTargetPosition;
        player.Inventory.Items = Global.Settings.PlayerSettings.Items;
        player.Inventory.Weapons = Global.Settings.PlayerSettings.Weapons;
        player.Inventory.Shards = Global.Settings.PlayerSettings.Shards;
    }

    private Node SetObject(Node value, ref TakeNodeHandler handler)
    {
        if (handler != null)
            handler.Invoke(value);
        return value;
    }

    private void Subscribe(ref TakeNodeHandler handler, TakeNodeHandler subscriber, Node node)
    {
        handler += subscriber;
        if(node != null)
            subscriber.Invoke(node);
    }
}
