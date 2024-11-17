using Godot;
using Newtonsoft.Json.Linq;
using System;

public class SceneObjects
{
    public Player Player { get; set; }
    public DialogPanel DialoguePanel { get; set; }
    public InventoryItems InventoryItems { get; set; }

    public void TakePlayerSettings(Player player)
    {
        player.Position = Global.Settings.PlayerSettings.CurrentPosition;
        player.TargetPosition = Global.Settings.PlayerSettings.CurrentTargetPosition;
        for (int i = 0; i < Global.Settings.PlayerSettings.Items.Count; i++)
            GD.Print(Global.Settings.PlayerSettings.Items[i] != null);
        player.Inventory.Items = Global.Settings.PlayerSettings.Items;
        player.Inventory.Weapons = Global.Settings.PlayerSettings.Weapons;
        player.Inventory.Shards = Global.Settings.PlayerSettings.Shards;
    }
}
