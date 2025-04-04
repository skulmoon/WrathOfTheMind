using Godot;
using System;

[GlobalClass]
public partial class Armor : Item
{
    [Export] public string ArmorType { get; set; }
    [Export] public float Protection { get; set; }
    [Export] public float AdditionalHealth { get; set; }

    public Armor() : base() { }

    public Armor(int id, int maxCount, string itemName, string description, float protection, float additionalHealth, string armorType) : base(id, maxCount, itemName, description)
    {
        Protection = protection;
        AdditionalHealth = additionalHealth;
        ArmorType = armorType;
    }
}