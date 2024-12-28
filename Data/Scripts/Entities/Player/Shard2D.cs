using Godot;
using System;

public partial class Shard2D : Area2D
{
    private Sprite2D _sprite;
    private PointLight2D _light;
    private float _currentReload = 0;
    private float _lightNumber = 0;
    private bool _loaded = false;
    private int _health = 30;
    private int _maxHealth = 30;

    public int Damage { get; set; } = 20;
    public int Health
    {
        get => _health;
        private set
        {
            if (value <= 0)
                StartReload();
            else
                _health = value;
        }
    }
    public float Speed { get; set; } = 300f;
    public float TimeReload { get; set; } = 1f;

    public override void _Ready()
    {
        _sprite = GetNode<Sprite2D>("Sprite2D");
        _light = GetNode<PointLight2D>("PointLight2D");
    }

    public override void _PhysicsProcess(double delta)
    {
        if (!_loaded)
        {
            Vector2 cursorPosition = GetViewport().GetMousePosition() - GetViewport().GetWindow().Size / 2 + Global.SceneObjects.Player?.Position ?? Vector2.One;
            _lightNumber += (float)delta * 10;
            _light.Energy = MathF.Sin(_lightNumber) * 0.1f + 0.9f;
            if (GlobalPosition.DistanceTo(cursorPosition) > (float)delta * Speed && !Global.Settings.CutScene)
            {
                Vector2 direction = Position.DirectionTo(cursorPosition);
                GlobalPosition += direction * (float)delta * Speed;
            }
            else
                GlobalPosition = cursorPosition;
        }
        else
        {
            _light.Energy = _currentReload / TimeReload;
            _currentReload += (float)delta;
            if (_currentReload >= TimeReload)
                CompleteReload();
        }
    }

    public int Attack()
    {
        int result = Health * Damage;
        StartReload();
        return result;
    }

    public virtual void TakeDamage(int damage) =>
        Health -= damage;

    public void StartReload()
    {
        TopLevel = false;
        Position = Vector2.Zero;
        _loaded = true;
        _sprite.Visible = false;
        _light.Energy = 0;
        _health = _maxHealth;
    }

    private void CompleteReload()
    {
        TopLevel = true;
        GlobalPosition = Global.SceneObjects.Player?.Position ?? Vector2.One;
        _sprite.Visible = true;
        _loaded = false;
        _light.Energy = 1;
        _currentReload = 0;
    }
}