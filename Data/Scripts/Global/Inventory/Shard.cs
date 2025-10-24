using Godot;
using System;

[GlobalClass]
public partial class Shard : Item
{
    [Export] public string ShardType { get; set; }
    [Export] public int Health { get; set; }
    [Export] public float Damage { get; set; }
    [Export] public int Speed { get; set; }
    [Export] public float TimeReload { get; set; }
    [Export] public float CritChance { get; set; }
    [Export] public int MaxRange { get; set; }

    public Shard() : base() { }

    public Shard(int id, int maxCount, string itemName, string description, string shard2D, int health, float damage, int speed, float timeReload, float critChance, int maxRange) : base(id, maxCount, itemName, description)
    {
        ShardType = shard2D;
        Health = health;
        Damage = damage;
        Speed = speed;
        TimeReload = timeReload;
        CritChance = critChance;
        MaxRange = maxRange;
    }

    public override object Clone()
    {
        Item item = new Shard(ID, MaxCount, Name, Description, ShardType, Health, Damage, Speed, TimeReload, CritChance, MaxRange);
        item.Count = Count;
        return item;
    }
}