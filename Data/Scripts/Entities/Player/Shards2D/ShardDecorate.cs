using Godot;
using System;
using System.Collections.Generic;

public partial class ShardDecorate : Node2D
{
    private const float MATH_SHIT1 = 45 * MathF.PI / 180;
    private const float MATH_SHIT2 = -45 * MathF.PI / 180;
    private const float MATH_SHIT3 = 2 * MathF.PI;

    private float _delta = 0;
    private Player _player;
    private ShardManager _manager;
    private List<Shard2D> _slaveShards = new List<Shard2D>();
    private Shard2D _mainShard;
    private float _angelDistance = 0;
    private float _acceleration = 1;

    private Shard2D MainShard
    {
        get => _mainShard;
        set
        {
            if (value == null)
                ProcessMode = ProcessModeEnum.Disabled;
            _mainShard = value;
            if (value != null)
            {
                ProcessMode = ProcessModeEnum.Inherit;
                _mainShard.Position = Vector2.Zero;
            }
        }
    } 

    public ShardDecorate(ShardManager manager)
    {
        ProcessMode = ProcessModeEnum.Disabled;
        _manager = manager;
        manager.ShardsChanged += OnShardsChanged;
        manager.ReloadStarted += StartReload;
        manager.ReloadCompleted += OnReloadCompleted;
        Global.SceneObjects.PlayerChanged += OnPlayerChanged;
    }

    public void OnPlayerChanged(Node player)
    {
        _player = (Player)player;
        _manager.Position = (new Vector2(0, -32)) + _player?.Position ?? Vector2.Zero;
        _player.SpeedMultiperChanged += OnSpeedMultiperChanged;
    }

    public void OnSpeedMultiperChanged(float speed) =>
        _acceleration = speed;

    public void OnShardsChanged(List<Shard2D> shards)
    {
        _slaveShards = shards.GetSlaveShards();
        MainShard = shards.Find(x => x.IsMain);
        _angelDistance = MATH_SHIT3 / (shards.Count > 1 ? shards.Count - 1 : 1);
    }

    public override void _PhysicsProcess(double delta)
    {
        Vector2 cursorPosition = GetGlobalMousePosition();
        DecorateMainShard(MainShard, cursorPosition, (float)delta);
        DecorateSlaveShards(_slaveShards);
    }

    public void DecorateMainShard(Shard2D shard, Vector2 cursorPosition, float delta)
	{
        _delta += (float)delta * 10;
        float distanceToСursor = _manager.GlobalPosition.DistanceTo(cursorPosition);
        Vector2 direction = _manager.Position.DirectionTo(cursorPosition);
        if (_manager.GlobalPosition.DistanceTo(cursorPosition) > (float)delta * shard.Speed)
            shard.Sprite.Rotation = Vector2.FromAngle(shard.Sprite.Rotation).Lerp(Vector2.FromAngle(shard.GlobalPosition.AngleToPoint(cursorPosition) + MATH_SHIT1 + (MathF.Sin(_delta) / 10)), 20 * (float)delta).Angle();
        else
            shard.Sprite.Rotation = Vector2.FromAngle(shard.Sprite.Rotation).Lerp(Vector2.FromAngle(MATH_SHIT2 + (MathF.Sin(_delta) / 10)), 10 * (float)delta).Angle();
        if (cursorPosition.DistanceTo(_player?.GlobalPosition ?? Vector2.Zero) > shard.MaxRange)
            cursorPosition = (_player?.GlobalPosition ?? Vector2.Zero).DirectionTo(cursorPosition) * shard.MaxRange + _player?.GlobalPosition ?? Vector2.Zero;
        if (_manager.GlobalPosition.DistanceTo(cursorPosition) > (float)delta * shard.Speed)
            _manager.GlobalPosition += _manager.GlobalPosition.DirectionTo(cursorPosition) * (float)delta * shard.Speed * _acceleration;
        else
            _manager.GlobalPosition = cursorPosition;
    }

    public void DecorateSlaveShards(List<Shard2D> shards)
    {
        for (int i = 0; i < shards.Count; i++)
        {
            Vector2 newPosition = new Vector2(Mathf.Cos(_angelDistance * i + (_delta * 0.8f)), Mathf.Sin(_angelDistance * i + (_delta * 0.8f))) * 20;
            shards[i].Sprite.Rotation = shards[i].Position.AngleToPoint(newPosition) + MATH_SHIT1;
            shards[i].Position = newPosition;
        }
    }

    public void StartReload(List<Shard2D> shards)
    {
        _manager.Position = (new Vector2(0, -32)) + _player?.Position ?? Vector2.Zero;
        _manager.ActiveShards.ForEach(x => x.Sprite.Visible = false);
        _manager.MainShard.Light.Energy = 0;
        _manager.MainShard.DisableMode = CollisionObject2D.DisableModeEnum.KeepActive;
        ProcessMode = ProcessModeEnum.Disabled;
    }

    public void OnReloadCompleted(List<Shard2D> shards)
    {
        _manager.Position = (new Vector2(0, -32)) + _player?.Position ?? Vector2.Zero;
        _manager.MainShard.Light.Energy = 1;
        for (int i = 0; i < shards.Count; i++)
        {
            shards[i].Sprite.Visible = true;
            shards[i].DisableMode = CollisionObject2D.DisableModeEnum.KeepActive;
        }
        _manager.MainShard.Sprite.Visible = true;
        _manager.MainShard.DisableMode = CollisionObject2D.DisableModeEnum.KeepActive;
        ProcessMode = ProcessModeEnum.Inherit;
    }

    public override void _ExitTree()
    {
        _player.Shard.ShardsChanged -= OnShardsChanged;
        Global.SceneObjects.PlayerChanged -= OnPlayerChanged;
        base._ExitTree();
    }
}
