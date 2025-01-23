using Godot;
using System;

public partial class TestEnemyFabric : EnemyFabric
{
	public override void _Ready() =>
		Create();

    public override void Create()
	{
		foreach (var item in EnemyZones)
			for (int j = 0; j < item.Difficulty; j++)
                item.PlaceEnemy(new TestEnemy1(1, 100, 30, 50));
	}
}
