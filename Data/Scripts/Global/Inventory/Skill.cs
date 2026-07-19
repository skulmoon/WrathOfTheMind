using Godot;
using System;

[GlobalClass]
public partial class Skill : Item
{
    [Export] public string SkillType { get; set; }

    public Skill() : base() { }

    public Skill(int id, int maxCount, string itemName, string description, string skillType) : base(id, maxCount, itemName, description)
    {
        SkillType = skillType;
    }

    public override void UpdateInfo()
    {
        Skill newSkill = GD.Load<Skill>($"res://Data/Resources/Items/Skills/{ID}.tres");
        UpdateInfo(newSkill);
        SkillType = newSkill.SkillType;
    }
}
