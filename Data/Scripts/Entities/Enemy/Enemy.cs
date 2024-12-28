using Godot;

public abstract partial class Enemy : CharacterBody2D
{
    private int _health = 1200;
    public AnimatedSprite2D Animation { get; set; }
    public Label Label { get; set; }
    public float Speed { get; private set; } = 200;
    public int Damage { get; private set; } = 12;
    public int Health
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

    public Enemy() =>
        AddNodes();

    public Enemy(float speed, int damage, int health)
    {
        _health = health;
        Speed = speed;
        Damage = damage;
        AddNodes();
    }

    private void AddNodes()
    {
        CollisionLayer = 4;
        CollisionMask = 4;
        AddChild(new CollisionShape2D()
        {
            Shape = new RectangleShape2D()
            {
                Size = new Vector2(32,32),
            }
        });
        Area2D hitbox = new Area2D()
        {
            CollisionLayer = 16,
            CollisionMask = 16,
        };
        AddChild(hitbox);
        hitbox.AreaEntered += ShardOnEntered;
        hitbox.AddChild(new CollisionShape2D()
        {
            Shape = new RectangleShape2D()
            {
                Size = new Vector2(34, 34)
            }
        });
        Animation = (AnimatedSprite2D)GD.Load<PackedScene>("res://Data/Textures/Enemys/TestEnemy1/TestEnemy1.tscn").Instantiate();
        AddChild(Animation);
        Label = new Label();
        AddChild(Label);
    }

    public abstract void Move(Vector2 playPosition);

    public abstract void Attack();

    public virtual void TakeDamage(int damage) =>
        Health -= damage;

    public void ShardOnEntered(Area2D area)
    {
        if (area is Shard2D shard)
            TakeDamage(shard.Attack());
    }
}
