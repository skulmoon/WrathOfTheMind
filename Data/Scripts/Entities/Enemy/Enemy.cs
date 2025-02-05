using Godot;

public abstract partial class Enemy : CharacterBody2D
{
    private float _health = 1200;
    public AnimatedSprite2D Animation { get; set; }
    public CollisionShape2D Collision { get; set; }
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
        FloorBlockOnWall = false;
        FloorStopOnSlope = false;
    }

    public abstract void Move(Vector2 playerPosition, double delta);

    public abstract void Attack(EnemyAttack attack);

    public virtual void TakeDamage(float damage) =>
        Health -= damage;

    public void ShardOnEntered(Area2D area)
    {
        if (area is Shard2D shard)
            TakeDamage(shard.Attack());
    }
}
