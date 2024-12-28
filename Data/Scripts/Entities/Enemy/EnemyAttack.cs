using Godot;
using System;

public abstract partial class EnemyAttack : Area2D
{
	Timer _lifeTime;

	public int Damage { get; private set; } = 10;

    public EnemyAttack(int damage, double lifeTime, string texture = "res://Data/Textures/EnemyAttack.png")
	{
		Damage = damage;
		CollisionLayer = 16;
		CollisionMask = 16;
		_lifeTime = new Timer()
		{
			WaitTime = lifeTime,
			Autostart = true

		};
		AddChild(_lifeTime);
		_lifeTime.Timeout += Destroy;
        AddChild(new Sprite2D()
        {
            Texture = GD.Load<Texture2D>(texture)
        });
        AreaEntered += ShardOnEntered;
    }

    public void ShardOnEntered(Area2D area)
	{
		if (area is Shard2D shard)
		{
			shard.TakeDamage(Damage);
			Destroy();
		}
		else if (area is HitBox hitBox)
		{
			hitBox.TakeDamage(Damage);
			Destroy();
        }
	}

    public virtual void Destroy() =>
		QueueFree();
}
