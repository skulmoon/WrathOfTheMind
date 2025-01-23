using Godot;
using System;
using System.Reflection;

[Tool]
public partial class ResourcesShardCreator : ResourcesItemCreator
{
    [Export] public string ShardType { get; set; }
    [Export] public int Health { get; set; }
    [Export] public int Damage { get; set; }
    [Export] public int Speed { get; set; }
    [Export] public float ReloadSpeed { get; set; }
    [Export] public float CritChance { get; set; }
    [Export] public float MaxRange { get; set; }
    [Export] public override bool Create
    {
        get => default;
        set
        {
            if (!CheckID(ItemType.Shard) || (Type.GetType($"{ShardType}, {Assembly.GetExecutingAssembly().FullName}") is null))
                return;
            if (ItemName != null && Description != null)
            {
                Shard shard = new Shard(ID, MaxCount, Name, Description, ShardType, Health, Damage, Speed, ReloadSpeed, CritChance, MaxRange);
                ResourceSaver.Save(shard, "res://Data/Resources/Items/Shards/" + shard.ID + ".tres");
            }
        }
    }
}
