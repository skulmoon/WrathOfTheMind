using Godot;
using System;
using System.Collections.Generic;
using System.Reflection;

public partial class ShardManager : Node2D
{
    private ShardDecorate decorate = new ShardDecorate();
    private List<Shard2D> _shards = new();
    private Shard2D _mainShard;
    private List<Shard2D> _activeShards = new();
    private Timer _reloadTimer;
    private Player _player;
    private int _destroyShards;

    private Shard2D MainShard
    {
        get => _mainShard;
        set
        {
            if (_mainShard is ShardAbility shard1)
                shard1.IsMain = false;
            if (value is ShardAbility shard2)
                shard2.IsMain = true;
            _mainShard = value;
        }
    }

    public ShardManager(Player player)
    {
        _reloadTimer = new Timer()
        {
            WaitTime = MainShard?.TimeReload ?? 1,
            OneShot = true,
        };
        AddChild(_reloadTimer);
        _shards.Clear();
        _activeShards.Clear();
        MainShard = null;
        _player = player;
    }

    public override void _Ready()
    {
        _reloadTimer.Timeout += CompleteReload;
        Global.Inventory.ShardChanged += UpdateShard;
        UpdateShard();
    }

    public override void _PhysicsProcess(double delta)
    {
        if (!Global.Settings.CutScene && MainShard != null)
        {
            if (_reloadTimer.TimeLeft == 0)
            {            
                Vector2 cursorPosition = GetGlobalMousePosition();
                if (MainShard != null)
                    decorate.DecorateMainShard(this, MainShard, cursorPosition, (float)delta);
                decorate.DecorateSubordinateShards(_shards);
            }
            else
                MainShard.Light.Energy = (float)_reloadTimer.WaitTime - (float)_reloadTimer.TimeLeft / (float)_reloadTimer.WaitTime;
        }
    }

    public void StartReload()
    {
        if (_activeShards.Count != 0)
        {
            MainShard = _activeShards[0];
            _shards.Clear();
            for (int i = 1; i < _activeShards.Count; i++)
                if (_activeShards[i] != null)
                    _shards.Add(_activeShards[i]);
            decorate.StartReload(this, MainShard);
            _reloadTimer.Start();
        }
    }

    private void CompleteReload()
    {
        foreach (Shard2D shard in _activeShards)
        {
            AddChild(shard);
            shard.RecoveryHealth();
        }
        decorate.CompleteReload(this, MainShard, _shards);
        _destroyShards = 0;
    }

    private void DestroyShard(Shard2D shard)
    {
        if (MainShard.Equals(shard))
        {
            if (_shards.Count > 0)
            {
                MainShard = _shards[0];
                _shards.RemoveAt(0);
                MainShard.Position = Vector2.Zero;
            }
        }
        else
        {
            _shards.Remove(shard);
        }
        RemoveChild(shard);
        _destroyShards++;
        if (_destroyShards >= _activeShards.Count)
        {
            StartReload();
            _destroyShards = 0;
        }
    }
    
    public void UpdateShard()
    {
        for (int i = 0; i < _activeShards.Count; i++)
            if (this == _activeShards[i].GetParent())
                RemoveChild(_activeShards[i]);
        _activeShards.Clear();
        for (int i = 16; i < 20; i++)
        {
            Shard item = Global.Inventory.Shards[i] as Shard; 
            if (item != null)
            {
                Type shardType = Type.GetType($"{item.ShardType}, {Assembly.GetExecutingAssembly().FullName}");
                Shard2D shard = (Shard2D)Activator.CreateInstance(shardType, (object)DestroyShard, item.Health, item.Damage, item.Speed, item.TimeReload, item.CritChance, item.MaxRange);
                _activeShards.Add(shard);
            } 
        }
        if (_activeShards.Count != 0)
        {
            _reloadTimer.WaitTime = _activeShards[0].TimeReload;
        }
        StartReload();
    }

    public override void _ExitTree()
    {
        Global.Inventory.ShardChanged -= UpdateShard;
    }
}