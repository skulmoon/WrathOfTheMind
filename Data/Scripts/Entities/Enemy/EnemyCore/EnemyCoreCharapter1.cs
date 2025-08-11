using Godot;
using System;
using System.Collections.Generic;

public partial class EnemyCoreCharapter1 : EnemyCore
{
	private List<MeleSkeleton> _meleSkeletons = new List<MeleSkeleton>();
	private List<DistantSkeleton> _distantSkeletons = new List<DistantSkeleton>();
    private List<ExplosionSkeleton> _explosionSkeletons = new List<ExplosionSkeleton>();
	private EnemyPositionsControl _positionControl;

    public override void _Ready()
	{
		Global.SceneObjects.LocationChanged += OnLocationChanged;
	}

	public void OnLocationChanged(Node location)
	{
        GD.Print(location.GetType());
        _positionControl = GD.Load<PackedScene>("res://Data/Scenes/Entities/EnemyPositionsControl.tscn").Instantiate<EnemyPositionsControl>();
        Global.SceneObjects.Player.AddChild(_positionControl);
        foreach (Enemy enemy in Global.SceneObjects.Enemies)
        {
            enemy.NoticedPlayer += OnNoticePlayer;
            _positionControl.GetControlPositionNode(enemy);
            if (enemy is MeleSkeleton meleSkeleton)
                _meleSkeletons.Add(meleSkeleton);
            else if (enemy is DistantSkeleton distantSkeleton)
                _distantSkeletons.Add(distantSkeleton);
            else if (enemy is ExplosionSkeleton explosionSkeleton)
                _explosionSkeletons.Add(explosionSkeleton);
        }
    }

    public void OnNoticePlayer(Enemy trash)
    {
        _meleSkeletons.ForEach(x => x.State.NoticePlayer());
        _distantSkeletons.ForEach(x => x.State.NoticePlayer());
        _explosionSkeletons.ForEach(x => x.State.NoticePlayer());
        foreach (Enemy enemy in Global.SceneObjects.Enemies)
            enemy.NoticedPlayer -= OnNoticePlayer;
    }

    public override void _ExitTree()
    {
        Global.SceneObjects.LocationChanged -= OnLocationChanged;
    }
}
