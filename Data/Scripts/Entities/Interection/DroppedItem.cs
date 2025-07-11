using Godot;
using System;
using System.Collections.Generic;

public partial class DroppedItem : Area2D, IInteractionArea
{
    [Export] public Item Item { get; set; }
    [Export] public int Count { get; set; }
    public event Action Take;

    public override void _Ready()
    {
        CollisionLayer = 8;
        CollisionMask = 8;
        CollisionShape2D collision = new CollisionShape2D()
        {
            Shape = new RectangleShape2D { Size = new Vector2(32, 32) }
        };
        Sprite2D sprite = new Sprite2D()
        {
            Texture = GD.Load<Texture2D>($"res://Data/Textures/Items/{Item.GetType()}s/{Item.Name}.png")
        };
        AddChild(collision);
        AddChild(sprite);
    }

    public void Interaction()
    {
        Take?.Invoke();
        Item item = (Item)Item.Duplicate();
        item.Count = Count;
        Global.Inventory.TakeItem(item);
        Global.CutSceneManager.StartCutScene(new NPCDialogue
        {
            DialogueNumber = 1,
            NPCID = 1,
            Speech = new List<IDAndText>(new IDAndText[] {
                new IDAndText { Text = $"Вы получили предмет \"{Item.Name}\"" + (Count > 1 ? $" ({Count})." : ".") }
            })
        }, null);
        QueueFree();
    }
}
