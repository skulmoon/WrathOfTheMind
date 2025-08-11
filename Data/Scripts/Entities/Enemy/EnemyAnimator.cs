using Godot;
using System;

public partial class EnemyAnimator : AnimatedSprite2D
{
	private string _currentAnimation;
    public Vector2 _currentDirection;

    public override void _Ready()
    {
        Enemy enemy = GetParent<Enemy>();
        enemy.ChangedDirection += OnChangeDirection;
        enemy.ChangedState += OnChangeState;
    }

    public void OnChangeDirection(Vector2 direction)
	{
        direction = direction.Normalized().Round();
        if (direction != _currentDirection && _currentAnimation != null)
        {
            _currentDirection = direction;
            Animation = $"{_currentAnimation}_{GetDirectionString(direction)}";
        }
    }

    public void OnChangeState(IEnemyState state)
    {
        string animation = state.GetAnimation();
        if (animation != _currentAnimation)
        {
            _currentAnimation = animation;
            Animation = $"{animation}_{GetDirectionString(_currentDirection)}";
        }
    }

    public string GetDirectionString(Vector2 direction)
    {
        string directionString =
            direction == Vector2.Up ? "up" :
            direction == Vector2.Left ? "left" :
            direction == Vector2.Right ? "right" : "down";
        return directionString;
    }
}
