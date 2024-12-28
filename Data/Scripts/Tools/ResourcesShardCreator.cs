using Godot;
using System;
using System.Reflection;

public partial class ResourcesShardCreator : ResourcesItemCreator
{
    [Export] public string ShardClass { get; set; }
    [Export] public int Health { get; set; }
    [Export] public int Damage { get; set; }
    [Export] public int CritChance { get; set; }
    [Export] public override bool Create
    {
        get => default;
        set
        {
            if (!CheckID(ItemType.Shard) && (Type.GetType($"{ShardClass}, {Assembly.GetExecutingAssembly().FullName}") is null))
            {
                
                return;
            }
            if (!(ItemName == null || Description == null))
            {
                Shard shard = new Shard(ID, MaxCount, ItemName, Description, Health, Damage, CritChance, ShardClass);
                ResourceSaver.Save(shard, "res://Data/Resources/Items/Shards/" + shard.ID + ".tres");
            }
        }
    }
}
