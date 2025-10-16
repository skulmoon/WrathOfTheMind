using Godot;
using System;

public partial class Armor2D : Node2D
{
    private AnimatedSprite2D _sprite;
    public int MaxHealth { get; private set; }
    public float Protection { get; set; }
    public int AdditionalHealth { get; set; }

    public Armor2D(float protection, int additionalHealth)
    {
        _sprite = (AnimatedSprite2D)GD.Load<PackedScene>("res://Data/Textures/Entities/Armors/Test1Armor2D/Test1Armor2D.tscn").Instantiate();
        AddChild(_sprite);
        _sprite.Play();
        Protection = protection;
        AdditionalHealth = additionalHealth;
        MaxHealth = additionalHealth;
    }

    public int ChangeDamage(int damage) =>
		damage;

    public float SetHealth() =>
        AdditionalHealth = MaxHealth;
}
