using Godot;
using System;
using System.Text;

public abstract partial class Enemy : CharacterBody2D
{
    private float _health = 1200;
    private float _speed = 200;
    private float _speedMultiplier = 1;

    public int MyProperty { get; set; }
    public EnemyPositionsControlNode PositionControl { get; set; }
    public AnimatedSprite2D Animation { get; private set; }
    public NavigationAgent2D NavigationAgent { get; private set; } = new NavigationAgent2D() { MaxNeighbors = 0 };
    public CollisionShape2D Collision { get; set; }
    public float SpeedMultiplier 
    { 
        get => _speedMultiplier;
        set
        {
            _speedMultiplier = value;
            NavigationAgent.MaxSpeed = _speed * _speedMultiplier;
        }
    } 
    public float Speed 
    { 
        get => _speed; 
        private set
        {
            _speed = value;
            NavigationAgent.MaxSpeed = _speed * _speedMultiplier;
        }
    }
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

    public event Action<Enemy> NoticedPlayer;
    public event Action<Vector2> ChangedDirection;
    public event Action<IEnemyState> ChangedState;

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
        hitbox.AreaEntered += OnPLayerAttackEntered;
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
        Global.SceneObjects.Enemies.Add(this);
    }

    public virtual void Attack(EnemyAttack attack) =>
        GetTree()?.CurrentScene?.CallDeferred("add_child", attack);

    public virtual void Move(Vector2 point, double delta)
    {
        NavigationAgent.TargetPosition = point;
        Velocity = (NavigationAgent.GetNextPathPosition() - GlobalPosition).Normalized() * _speed * _speedMultiplier * (float)delta * 100;
        ChangedDirection?.Invoke(Velocity.Normalized());
        MoveAndSlide();
    }

    public virtual void TakeDamage(float damage) =>
        Health -= damage;

    public void OnPLayerAttackEntered(Area2D area)
    {
        if (area is PlayerAttack playerAttack)
            TakeDamage(playerAttack.Attack());
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
            ChangedState?.Invoke((IEnemyState)NewState);
        }
    }

    public void NoticePlayer() =>
        NoticedPlayer?.Invoke(this);
}
