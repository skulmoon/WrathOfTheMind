using Godot;

public partial class OnehandedMovementDistantSkeletonState : Node2D, IDistantSkeletonState
{
    private DistantSkeleton _enemy;

    public OnehandedMovementDistantSkeletonState(DistantSkeleton enemy)
    {
        _enemy = enemy;
        ((CircleShape2D)_enemy.Trigger.Shape).Radius = 200;
        if (((CircleShape2D)_enemy.Trigger.Shape).Radius + 16 > _enemy.GlobalPosition.DistanceTo(Global.SceneObjects.Player.GlobalPosition))
        {
            _enemy.State = new OnehandedAttackDistanceSkeletonState(_enemy);
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        _enemy.Move(Global.SceneObjects.Player.GlobalPosition, delta);
    }

    public void Attack()
    {
        _enemy.State = new OnehandedAttackDistanceSkeletonState(_enemy);
    }

    public string GetAnimation() =>
        "onehanded_movement";
}
