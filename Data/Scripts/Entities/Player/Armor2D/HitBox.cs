using Godot;
using System;
using System.Reflection;

public partial class HitBox : Area2D
{
    private Player _player;
    private int _maxHealth = 1000;
    private int _health = 1000;

    public Action<Armor2D> _changedArmor;

    public Armor2D Armor2D { get; set; }

    public event Action<int> ChangeHealth;
    public event Action<Armor2D> ChangedArmor { 
        remove => _changedArmor -= value;  
        add
        {
            _changedArmor += value;
            value.Invoke(Armor2D);
        } 
    }

    public int Health { 
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
            ChangeHealth.Invoke(Health);
            if (value < 0)
            {
                Global.SceneObjects.ChangeScene("res://Data/Scenes/Menu/MainMenu.tscn");
            }
        }
    }
    public int MaxHealth { get => _maxHealth + Armor2D?.MaxHealth ?? 0; }

    public HitBox()
    {
        Global.SceneObjects.PlayerChanged += ChangePlayer;
    }

    public void TakeDamage(int damage)
	{
		Health -= Armor2D?.ChangeDamage(damage) ?? damage;
	}

    public void ChangePlayer(Node player)
    {
        _player = (Player)player;
        Global.Inventory.ArmorChanged += ChangeArmor;
        ChangeArmor(Global.Inventory.GetActiveArmor());
    }

    public void ChangeArmor(Armor armor)
    {
        if (this == Armor2D?.GetParent())
            RemoveChild(Armor2D);
        Armor item = (Armor)Global.Inventory.Armors[16];
        if (item != null)
        {
            Type armorType = Type.GetType($"{item.ArmorType}, {Assembly.GetExecutingAssembly().FullName}");
            Armor2D armor2D = (Armor2D)Activator.CreateInstance(armorType, item.Protection, item.AdditionalHealth);
            _changedArmor?.Invoke(armor2D);
            Armor2D = armor2D;
            AddChild(armor2D);
        }
        else
            _changedArmor?.Invoke(null);
    }

    public override void _ExitTree()
    {
        Global.SceneObjects.PlayerChanged -= ChangePlayer;
        Global.Inventory.ArmorChanged -= ChangeArmor;
    }
}
