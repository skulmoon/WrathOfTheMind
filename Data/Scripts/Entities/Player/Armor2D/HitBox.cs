using Godot;
using System;
using System.Reflection;

public partial class HitBox : Area2D
{
    private Player _player;
    private int _maxHealth = 100;
    private int _health = 100;
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
        get => _health + (Armor2D?.AdditionalHealth ?? 0);
        set
        {
            if (value > _maxHealth)
            {
                _health = _maxHealth;
                if (Armor2D != null)
                    Armor2D.AdditionalHealth = value - _maxHealth;
            }
            else
            {
                _health = value;
                if (Armor2D != null)
                    Armor2D.AdditionalHealth = 0;
            }
            ChangeHealth?.Invoke(Health);
            if (value <= 0)
            {
                Global.Variables.CutScene = true;
                Global.Music.PlaySound("DeathSound.ogg");
                _player.ZIndex = 1;
                _player.Sprite.Play("death");
                _player.TopLevel = true;
                _player.Modulate = Global.SceneObjects.Location.Modulate;
                _player.Shard.Visible = false;
                Tween tween = CreateTween();
                Interface Interface = _player.GetNode<Interface>("%Interface");
                Interface.HideBars();
                tween.TweenProperty(Global.SceneObjects.Location, "modulate:a", 0, 0.5f);
                tween.Parallel().TweenProperty(_player.Camera, "zoom", _player.Camera.Zoom + new Vector2(2, 2), 0.5f).SetTrans(Tween.TransitionType.Sine);
                tween.TweenInterval(4);
                tween.TweenProperty(_player, "modulate:a", 0, 0.5f);
                tween.TweenCallback(Callable.From(() =>
                {
                    Global.Inventory.Clear();
                    Global.SceneObjects.ChangeScene("res://Data/Scenes/Location/death_room.tscn");
                }));
            }
        }
    }
    public int MaxHealth { get => _maxHealth + (Armor2D?.MaxHealth ?? 0); }

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
