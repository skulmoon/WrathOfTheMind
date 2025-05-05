using Godot;
using System;

public partial class Storage : Node
{
    public override void _Ready()
    {
        Global.SceneObjects.Storage = this;
        ProcessMode = ProcessModeEnum.Always;
    }
}
