using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

public partial class ShardManager : Node2D
{
    private int _mainShardNumber = 0;
    private Timer _reloadTimer;
    private int _destroyShards;

    public List<Shard2D> ActiveShards { get; set; } = new();

    public Action<List<Shard2D>> _changedShards;

    public event Action<List<Shard2D>> ReloadStarted;
    public event Action<List<Shard2D>> ReloadCompleted;
    public event Action<List<Shard2D>> ShardsChanged
    {
        remove => _changedShards -= value;
        add
        {
            _changedShards += value;
            value.Invoke(ActiveShards);
        }
    }

    public double CurrentReload { get => _reloadTimer.TimeLeft; }

    public Shard2D MainShard
    {
        get => ActiveShards[_mainShardNumber];
        private set
        {
            MainShard.IsMain = false;
            if (value != null)
            {
                value.IsMain = true;
                _mainShardNumber = ActiveShards.IndexOf(value);
            }
            _changedShards?.Invoke(ActiveShards);
        }
    }

    public override void _Ready()
    {
        AddChild(new ShardDecorate(this));
        _reloadTimer = new Timer()
        {
            OneShot = true,
        };
        AddChild(_reloadTimer);
        _reloadTimer.Timeout += CompleteReload;
        Global.Inventory.ShardsChanged += UpdateShard;
        UpdateShard(Global.Inventory.GetActiveShardList());
    }

    public override void _Process(double delta)
    {
        for (int i = 0; i < 4; i++)
            if (Input.IsActionJustPressed($"change_shard_{i + 1}"))
            {
                Shard2D shard = ActiveShards.Find(x => x.Number == i);
                MainShard = shard?.IsEnabled ?? false ? shard : MainShard;
            }
        base._Process(delta);
    }

    public void StartReload()
    {
        if (ActiveShards.Count != 0)
        {
            MainShard = ActiveShards[0];
            _reloadTimer.Start();
            ReloadStarted?.Invoke(ActiveShards);
        }
    }

    private void CompleteReload()
    {
        foreach (Shard2D shard in ActiveShards)
            shard.Enable();
        MainShard.IsMain = true;
        _destroyShards = 0;
        ReloadCompleted?.Invoke(ActiveShards);
    }

    private void DestroyShard(Shard2D shard)
    {
        if (MainShard.Equals(shard) && ActiveShards.Count > 1)
            MainShard = ActiveShards.Find(x => !x.IsMain && x.IsEnabled);
        _destroyShards++;
        if (_destroyShards >= ActiveShards.Count)
            StartReload();
    }
    
    public void UpdateShard(List<Shard> shards)
    {
        foreach (Node node in GetChildren().Where(x => x is Shard2D))
            RemoveChild(node);
        ActiveShards.Clear();
        for (int i = 0; i < shards.Count; i++)
        {
            if (shards[i] == null)
                continue;
            Type shardType = Type.GetType($"{shards[i].ShardType}, {Assembly.GetExecutingAssembly().FullName}");
            Shard2D shard2D = (Shard2D)Activator.CreateInstance(shardType, (object)DestroyShard, shards[i], i);
            ActiveShards.Add(shard2D);
            AddChild(shard2D);
        }
        float reloadSum = ActiveShards.Sum(x => x.TimeReload);
        _reloadTimer.WaitTime = reloadSum <= 0 ? 1 : reloadSum;
        StartReload();
    }

    public override void _ExitTree()
    {
        Global.Inventory.ShardsChanged -= UpdateShard;
    }
}