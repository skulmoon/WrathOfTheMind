using Godot;
using System;
using System.Reflection;

public partial class HitBox : Area2D
{
    private Player _player;
    private float _maxHealth;
    private float _health = 1000;

    public Armor2D Armor2D { get; set; }
    public float Health { 
        get => _health + Armor2D?.AdditionalHealth ?? 0;
        set
        {
            if (value > _maxHealth)
            {
                _health = _maxHealth;
                Armor2D.AdditionalHealth = value - _maxHealth;
            }
            else
            {
                if (Armor2D != null)
                    Armor2D.AdditionalHealth = 0;
                _health = value;
            }
            if (value < 0)
            {
                GetTree().CallDeferred("change_scene_to_file", "res://Data/Scenes/Menu/MainMenu.tscn");
            }
        }
    }

    public HitBox()
    {
        Global.SceneObjects.PlayerChanged += ChangePlayer;
    }

    public void TakeDamage(float damage)
	{
		Health -= Armor2D?.ChangeDamage(damage) ?? damage;
	}

    public void ChangePlayer(Node player)
    {
        _player = (Player)player;
        Global.Inventory.ArmorChanged += ChangeArmor;
        ChangeArmor();
    }

    public override void _ExitTree() =>
        Global.SceneObjects.PlayerChanged -= ChangePlayer;

    public void ChangeArmor()
    {
        if (this == Armor2D?.GetParent())
            RemoveChild(Armor2D);
        Armor item = (Armor)Global.Inventory.Armors[16];
        if (item is not null)
        {
            Type armorType = Type.GetType($"{item.ArmorType}, {Assembly.GetExecutingAssembly().FullName}");
            Armor2D armor = (Armor2D)Activator.CreateInstance(armorType, item.Protection, item.AdditionalHealth);
            Armor2D = armor;
            AddChild(armor);
        }
    }
}
