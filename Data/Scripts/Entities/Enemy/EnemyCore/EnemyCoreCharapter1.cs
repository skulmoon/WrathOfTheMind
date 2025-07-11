using Godot;
using System;
using System.Collections.Generic;

public partial class EnemyCoreCharapter1 : EnemyCore
{
	private List<MeleSkeleton> _meleSkeletons = new List<MeleSkeleton>();
	private List<DistantSkeleton> _distantSkeletons = new List<DistantSkeleton>();

	public override void _Ready()
	{
		foreach (Enemy enemy in Global.SceneObjects.Enemies)
		{
			if (enemy is MeleSkeleton meleSkeleton)
				_meleSkeletons.Add(meleSkeleton);
			else if (enemy is DistantSkeleton distantSkeleton)
				_distantSkeletons.Add(distantSkeleton);
		}
	}

}
