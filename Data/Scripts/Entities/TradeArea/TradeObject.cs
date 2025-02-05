using Godot;
using System;

[GlobalClass]
public partial class TradeObject : Resource
{
    [Export] public Item Item { get; set; }
    [Export] public int Price { get; set; }
    [Export] public ItemType Type { get; set; }
}
