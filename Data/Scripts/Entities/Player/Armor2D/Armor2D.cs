using Godot;
using System;

public partial class Armor2D : Node2D
{
    private AnimatedSprite2D _sprite;
    public float _maxHealth { get; private set; }
    public float Protection { get; set; }
    public float AdditionalHealth { get; set; }

    public Armor2D(float protection, float additionalHealth)
    {
        _sprite = (AnimatedSprite2D)GD.Load<PackedScene>("res://Data/Textures/Entities/Armors/Test1Armor2D/Test1Armor2D.tscn").Instantiate();
        AddChild(_sprite);
        _sprite.Play();
        Protection = protection;
        AdditionalHealth = additionalHealth;
        _maxHealth = additionalHealth;
    }

    public float ChangeDamage(float damage) =>
		damage;

    public float SetHealth() =>
        AdditionalHealth = _maxHealth;
}
