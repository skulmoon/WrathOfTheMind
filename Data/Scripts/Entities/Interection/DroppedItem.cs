using Godot;
using System;
using System.Collections.Generic;

public partial class DroppedItem : Area2D, IInteractionArea
{
    [Export] public Item Item { get; set; }

    public void Interaction()
    {
        Global.SceneObjects.Player.Inventory.TakeItem(Item);
        Global.CutSceneManager.StartCutScene(new NPCDialogue
        {
            DialogueNumber = 1,
            NPCID = 1,
            Speech = new List<IDAndText>(new IDAndText[] {
                new IDAndText { Text = $"Вы получили предмет \"{Item.Name}\"" + (Item.Count > 1 ? $" ({Item.Count})." : ".") }
            })
        }, null);
    }
}
