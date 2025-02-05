using Godot;
using System;
using System.Reflection;

[Tool]
public partial class ResourceArmorCreator : ResourcesItemCreator
{
    [Export] public string ArmorType { get; set; }
    [Export] public float Protection { get; set; }
    [Export] public float AdditionalHealth { get; set; }
    [Export] public override bool Create
    {
        get => default;
        set
        {
            if (!CheckID(ItemType.Armor) || (Type.GetType($"{ArmorType}, {Assembly.GetExecutingAssembly().FullName}") is null))
                return;
            if (ItemName != null && Description != null)
            {
                Armor armor = new Armor(ID, MaxCount, Name, Description, Protection, AdditionalHealth, ArmorType);
                ResourceSaver.Save(armor, "res://Data/Resources/Items/Armors/" + armor.ID + ".tres");
            }
        }
    }
}
