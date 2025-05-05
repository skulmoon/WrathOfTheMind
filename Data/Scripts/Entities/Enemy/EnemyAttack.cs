using Godot;
using System;

public abstract partial class EnemyAttack : CharacterBody2D
{
	private Timer _lifeTime;
	private CollisionShape2D _collision = new CollisionShape2D();

	public int Damage { get; private set; } = 10;
	public Shape2D Shape 
	{
		get => _collision.Shape;
        set => _collision.Shape = value;
	}

    public EnemyAttack(int damage, double lifeTime, string texture = "")
	{

        Area2D area = new Area2D();
		area.AddChild(_collision);
        area.AreaEntered += ShardOnEntered;
		AddChild(area);
        area.CollisionLayer = 16;
        area.CollisionMask = 16;
		Damage = damage;
		_lifeTime = new Timer()
		{
			WaitTime = lifeTime,
			Autostart = true,
			OneShot = true,

		};
		AddChild(_lifeTime);
		_lifeTime.Timeout += Destroy;
		if (texture != string.Empty)
		{
			AddChild(new Sprite2D()
			{
				Texture = GD.Load<Texture2D>(texture)
			});
		}
    }

    public virtual void ShardOnEntered(Area2D area)
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
