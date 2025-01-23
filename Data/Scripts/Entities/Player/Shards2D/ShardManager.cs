using Godot;
using System;
using System.Collections.Generic;
using System.Reflection;

public partial class ShardManager : Node2D
{
    private ShardDecorate decorate = new ShardDecorate();
    private Shard2D _mainShard;
    private List<Shard2D> _shards = new();
    private List<Shard2D> _activeShards = new();
    private Timer _reloadTimer;
    private Player _player;
    private int _destroyShards;

    public override void _Ready()
    {
        Global.SceneObjects.OnPlayerChanged += ChangePlayer;
        _reloadTimer = new Timer()
        {
            WaitTime = _mainShard?.TimeReload ?? 1,
            Autostart = true,
            OneShot = true,
        };
        AddChild(_reloadTimer);
        _reloadTimer.Timeout += CompleteReload;
    }

    public void ChangePlayer(Node player)
    {
        _player = (Player)player;
        _player.Inventory.ShardChanged += ChangeShard;
        ChangeShard();
    }

    public override void _PhysicsProcess(double delta)
    {
        if (!Global.Settings.CutScene)
        {
            if (_reloadTimer.TimeLeft == 0)
            {            
                Vector2 cursorPosition = GetViewport().GetMousePosition() - GetViewport().GetWindow().Size / 2 + _player?.Position ?? Vector2.One;
                if (_mainShard != null)
                    decorate.DecorateMainShard(this, _mainShard, cursorPosition, (float)delta);
                decorate.DecorateSubordinateShards(_shards);
            }
            else
                _mainShard.Light.Energy = (float)_reloadTimer.WaitTime - (float)_reloadTimer.TimeLeft / (float)_reloadTimer.WaitTime;
        }
    }

    public void StartReload()
    {
        if (_activeShards.Count != 0)
        {
            _mainShard = _activeShards[0];
            _shards.Clear();
            for (int i = 1; i < _activeShards.Count; i++)
                if (_activeShards[i] != null)
                    _shards.Add(_activeShards[i]);
            decorate.StartReload(this, _mainShard);
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
        decorate.CompleteReload(this, _mainShard, _shards);
        _destroyShards = 0;
    }

    private void DestroyShard(Shard2D shard)
    {
        if (_mainShard.Equals(shard))
        {
            if (_shards.Count > 0)
            {
                _mainShard = _shards[0];
                _shards.RemoveAt(0);
                _mainShard.Position = Vector2.Zero;
            }
            GD.Print($"Main shard is destroy. ({((Test1Shard2D)shard).Number})");
        }
        else
        {
            _shards.Remove(shard);
            GD.Print($"Secondary shard is destroy. ({((Test1Shard2D)shard).Number})");
        }
        RemoveChild(shard);
        _destroyShards++;
        if (_destroyShards >= _activeShards.Count)
        {
            StartReload();
            _destroyShards = 0;
        }
    }
    
    public void ChangeShard()
    {
        for (int i = 0; i < _activeShards.Count; i++)
            if (this == _activeShards[i].GetParent())
                RemoveChild(_activeShards[i]);
        _activeShards.Clear();
        for (int i = 16; i < 20; i++)
        {
            Shard item = (Shard)_player.Inventory.Shards[i]; 
            if (item is not null)
            {
                Type shardType = Type.GetType($"{item.ShardType}, {Assembly.GetExecutingAssembly().FullName}");
                Shard2D shard = (Shard2D)Activator.CreateInstance(shardType, DestroyShard, item.Health, item.Damage, item.Speed, item.TimeReload, item.CritChance, item.MaxRange);
                _activeShards.Add(shard);
                ((Test1Shard2D)shard).Number = i;
            } 
        }
        if (_activeShards.Count != 0)
            _reloadTimer.WaitTime = _activeShards[0].TimeReload;
        StartReload();
    }
}