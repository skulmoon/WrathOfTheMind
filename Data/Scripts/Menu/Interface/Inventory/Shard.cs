using Godot;
using System;

public partial class Shard : Item
{
    public string Shad2D { get; set; }
    public int Health { get; set; }
    public int Damage { get; set; }
    public double CritChance { get; set; }
    public Shard(int id, int maxCount, string itemName, string description, int health, int damage, double critChance, string shad2D) : base(id, maxCount, itemName, description)
    {
        Health = health;
        Damage = damage;
        CritChance = critChance;
        Shad2D = shad2D;
    }
}