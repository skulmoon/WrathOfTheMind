using Godot;
using System;

public partial class ActiveDialogWindow : Control
{
    private ChangeControlElement _element;
    private StringName _currentAction;
    private int _currentIndex;
    private bool _isMouseEntered;

    [Export] public CustomButton Exit { get; set; }
    [Export] public CustomButton Clear { get; set; }

    public override void _Ready()
    {
        Exit.Pressed += () =>
        {
            CloseWindow();
        };
        Exit.MouseEntered += () => _isMouseEntered = true;
        Exit.MouseExited += () => _isMouseEntered = false;
        Clear.Pressed += () =>
        {
            InputMap.ActionEraseEvents(_currentAction);
            CloseWindow();
        };
        Clear.MouseEntered += () => _isMouseEntered = true;
        Clear.MouseExited += () => _isMouseEntered = false;
        base._Ready();
    }

    public void ChangeAction(StringName action, int index, ChangeControlElement control)
    {
        Visible = true;
        _currentIndex = index;
        _currentAction = action;
        _element = control;
    }

    public override void _Input(InputEvent @event)
    {
        if (!string.IsNullOrEmpty(_currentAction) && (@event is InputEventKey || @event is InputEventMouseButton) && !_isMouseEntered)
        {
            var list = InputMap.ActionGetEvents(_currentAction);
            if (list.Count == 0)
                InputMap.ActionAddEvent(_currentAction, @event);
            else if (list.Count == 1)
            {
                if (_currentIndex == 1)
                {
                    InputMap.ActionEraseEvents(_currentAction);
                    InputMap.ActionAddEvent(_currentAction, @event);
                }
                else if (_currentIndex == 2)
                    InputMap.ActionAddEvent(_currentAction, @event);
            }
            else if (list.Count == 2)
            {
                InputMap.ActionEraseEvents(_currentAction);
                if (_currentIndex == 1)
                {
                    InputMap.ActionAddEvent(_currentAction, @event);
                    InputMap.ActionAddEvent(_currentAction, list[1]);
                }
                else if (_currentIndex == 2)
                {
                    InputMap.ActionAddEvent(_currentAction, list[0]);
                    InputMap.ActionAddEvent(_currentAction, @event);
                }
            }
            CloseWindow();
        }
    }

    public void CloseWindow()
    {
        GD.Print(0);
        _currentAction = string.Empty;
        Visible = false;
        _element?.UpdateVision();
    }
}
