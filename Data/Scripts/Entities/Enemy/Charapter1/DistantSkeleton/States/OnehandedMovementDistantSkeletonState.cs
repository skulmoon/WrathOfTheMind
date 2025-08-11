using Godot;

public partial class OnehandedMovementDistantSkeletonState : Node2D, IDistantSkeletonState
{
    private DistantSkeleton _enemy;

    public OnehandedMovementDistantSkeletonState(DistantSkeleton enemy)
    {
        _enemy = enemy;
        ((CircleShape2D)_enemy.Trigger.Shape).Radius = 200;
        if (((CircleShape2D)_enemy.Trigger.Shape).Radius + 16 > _enemy.GlobalPosition.DistanceTo(Global.SceneObjects.Player?.GlobalPosition ?? Vector2.Zero))
        {
            _enemy.State = new OnehandedAttackDistanceSkeletonState(_enemy);
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        _enemy.Move(_enemy.PositionControl?.GetPosition() ?? Vector2.Zero, delta);
    }

    public void Attack()
    {
        _enemy.State = new OnehandedAttackDistanceSkeletonState(_enemy);
    }

    public string GetAnimation() =>
        "onehanded_movement";
}
