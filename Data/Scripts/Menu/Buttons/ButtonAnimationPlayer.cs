using Godot;
using System;

public partial class ButtonAnimationPlayer : AnimationPlayer
{
    public override void _Ready()
    {
        Play("NormalButton");
    }
}
