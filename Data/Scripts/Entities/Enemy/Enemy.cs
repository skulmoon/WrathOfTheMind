using Godot;
using System;

public abstract partial class Enemy : CharacterBody2D
{
    private float _health = 1200;

    public NavigationAgent2D NavigationAgent { get; set; } = new NavigationAgent2D();
    public AnimatedSprite2D Animation { get; private set; }
    public CollisionShape2D Collision { get; set; }
    public float SpeedMultiplier { get; set; } = 1; 
    public float Speed { get; private set; } = 200;
    public int Damage { get; private set; } = 12;
    public float Health
    {
        get => _health;
        private set
        {
            if (value <= 0)
                QueueFree();
            else
                _health = value;
        }
    }

    public event Action<Vector2> ChangeDirection;
    public event Action<IEnemyState> ChangeState;

    public Enemy(float speed, int damage, int health, string animation)
    {
        _health = health;
        Speed = speed;
        Damage = damage;
        CollisionLayer = 4;
        CollisionMask = 4;
        Collision = new CollisionShape2D()
        {
            Shape = new CircleShape2D()
            {
                Radius = 15.5f
            }
        };
        AddChild(Collision);
        Area2D hitbox = new Area2D()
        {
            CollisionLayer = 16,
            CollisionMask = 16,
        };
        AddChild(hitbox);
        hitbox.AreaEntered += ShardOnEntered;
        hitbox.AddChild(new CollisionShape2D()
        {
            Shape = new CircleShape2D()
            {
                Radius = 17
            }
        });
        Animation = (AnimatedSprite2D)(Node2D)GD.Load<PackedScene>($"res://Data/Textures/Entities/Enemys/{animation}").Instantiate();
        Animation.Play();
        AddChild(Animation);
        AddChild(NavigationAgent);
        FloorBlockOnWall = false;
        FloorStopOnSlope = false;
    }

    public virtual void Attack(EnemyAttack attack) =>
        GetTree()?.CurrentScene?.CallDeferred("add_child", attack);

    public virtual void Move(Vector2 point, double delta)
    {
        Vector2 direction = GlobalPosition.DirectionTo(point);
        ChangeDirection?.Invoke(direction);
        Velocity = direction * (float)delta * Speed * SpeedMultiplier * 100;
        MoveAndSlide();
    }

    public virtual void TakeDamage(float damage) =>
        Health -= damage;

    public void ShardOnEntered(Area2D area)
    {
        if (area is Shard2D shard)
            TakeDamage(shard.Attack());
    }

    public void SetState(Node NewState, Node LastState)
    {
        if (LastState != null && LastState.GetParent() == this)
        {
            CallDeferred("remove_child", LastState);
            LastState.QueueFree();
        }
        if (NewState != null)
        {
            CallDeferred("add_child", NewState);
            ChangeState?.Invoke((IEnemyState)NewState);
        }
    }
}
