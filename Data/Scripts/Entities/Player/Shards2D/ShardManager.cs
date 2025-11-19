using Godot;
using System;
using System.Collections.Generic;
using System.Reflection;

public partial class ShardManager : Node2D
{
    private ShardDecorate decorate = new ShardDecorate();
    private List<Shard2D> _shards = new();
    private Shard2D _mainShard;
    private Timer _reloadTimer;
    private Player _player;
    private int _destroyShards;

    public List<Shard2D> ActiveShards { get; set; } = new();

    public Action<Shard2D> _changedShard;

    public event Action<Shard2D> MainShard2DChanged
    {
        remove => _changedShard -= value;
        add
        {
            _changedShard += value;
            value.Invoke(_mainShard);
        }
    }

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
        ActiveShards.Clear();
        MainShard = null;
        _player = player;
    }

    public override void _Ready()
    {
        _reloadTimer.Timeout += CompleteReload;
        Global.Inventory.ShardsChanged += UpdateShard;
        UpdateShard(Global.Inventory.GetActiveShardList());
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
        if (ActiveShards.Count != 0)
        {
            MainShard = ActiveShards[0];
            _shards.Clear();
            for (int i = 1; i < ActiveShards.Count; i++)
                if (ActiveShards[i] != null)
                    _shards.Add(ActiveShards[i]);
            decorate.StartReload(this, MainShard);
            _reloadTimer.Start();
        }
    }

    private void CompleteReload()
    {
        foreach (Shard2D shard in ActiveShards)
        {
            shard.Enable();
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
        shard.Disable();
        _destroyShards++;
        if (_destroyShards >= ActiveShards.Count)
        {
            StartReload();
            _destroyShards = 0;
        }
    }
    
    public void UpdateShard(List<Shard> shards)
    {
        for (int i = 0; i < ActiveShards.Count; i++)
            if (this == ActiveShards[i].GetParent())
                RemoveChild(ActiveShards[i]);
        ActiveShards.Clear();
        for (int i = 16; i < 20; i++)
        {
            Shard item = Global.Inventory.Shards[i] as Shard; 
            if (item != null)
            {
                Type shardType = Type.GetType($"{item.ShardType}, {Assembly.GetExecutingAssembly().FullName}");
                Shard2D shard = (Shard2D)Activator.CreateInstance(shardType, (object)DestroyShard, item.Health, item.Damage, item.Speed, item.TimeReload, item.CritChance, item.MaxRange);
                ActiveShards.Add(shard);
            } 
        }
        if (ActiveShards.Count != 0)
        {
            _changedShard?.Invoke(ActiveShards[0]);
            _reloadTimer.WaitTime = ActiveShards[0].TimeReload;
            foreach (Shard2D shard in ActiveShards)
                AddChild(shard);
        }
        else
            _changedShard?.Invoke(null);
        StartReload();
    }

    public override void _ExitTree()
    {
        Global.Inventory.ShardsChanged -= UpdateShard;
    }
}