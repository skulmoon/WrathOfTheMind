using Godot;
using System;
using static Godot.TextServer;

public partial class Player : NPC, IWalker
{
    private PlayerInteractionArea _interactionArea;
    private float _stamina = 0;
    private Vector2 _currentDirection;
    private float _currentSpeedMultiper;

    public AnimatedSprite2D Sprite { get; private set; }
    public ShardManager Shard { get; private set; }
    public Camera2D Camera { get; private set; }
    public HitBox HitBox { get; private set; }
    public float Stamina 
    { 
        get => _stamina;
        set
        {
            _stamina = value;
            ChangedPower?.Invoke(_stamina);
        }
    }
    [Export] public float MaxStamina { get; set; } = 50;
    [Export] public int PlayerSpeed { get; set; } = 7000;
    [Export] public float Acceleration { get; set; } = 2;

    public event Action<float> ChangedPower;
    public event Action<Vector2> ChangedDirection;
    public event Action<float> SpeedMultiperChanged;

    public override void _Ready()
    {
        Global.CutSceneManager.StartedCutScene += OnStartedCutScene;
        Sprite = GetNode<AnimatedSprite2D>("Sprite2D");
        _interactionArea = GetNode<PlayerInteractionArea>("PlayerInteractionArea");
        HitBox = GetNode<HitBox>("HitBox");
        Camera = GetNode<Camera2D>("Camera");
        Shard = new ShardManager();
        Global.SceneObjects.LocationChanged += OnLocationChanged;
        Stamina = Global.Settings.SaveData.Stamina;
        HitBox.Health = Global.Settings.SaveData.Health;
        Global.SceneObjects.Player = this;
    }

    public void OnLocationChanged(Location location) =>
        location.AddChild(Shard);


    public override void _PhysicsProcess(double delta)
    {
        if (!Global.Settings.CutScene)
            Move(delta);
    }

    private void Move(double delta)
    {
        float speedMultiper = 1;
        Vector2 direction = new Vector2(Input.GetAxis("left", "right"), Input.GetAxis("up", "down")).Normalized();
        if (Input.IsActionPressed("acceleration") && Stamina - (float)delta > 0 && direction != Vector2.Zero)
        {
            speedMultiper *= Acceleration;
            Stamina -= (float)delta * 10;
        }
        else if (!Input.IsActionPressed("acceleration") && Stamina < MaxStamina)
            Stamina += (float)delta * 20;
        Velocity = direction * PlayerSpeed * speedMultiper * (float)delta;
        MoveAndSlide();
        if (_currentDirection != direction)
        {
            ChangedDirection?.Invoke(direction);
            _currentDirection = direction;
        }
        if (_currentSpeedMultiper != speedMultiper)
        {
            SpeedMultiperChanged?.Invoke(speedMultiper);
            _currentSpeedMultiper = speedMultiper;
        }
    }  

    public void OnStartedCutScene() =>
        ChangedDirection.Invoke(Vector2.Zero);

    public override void _ExitTree()
    {
        Global.CutSceneManager.StartedCutScene -= OnStartedCutScene;
        Global.SceneObjects.LocationChanged -= OnLocationChanged;
        base._ExitTree();
    }
}
