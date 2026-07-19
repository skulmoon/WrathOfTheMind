using Godot;
using System;

[Tool]
public partial class Mask : Sprite2D
{
    [Export]
    public Texture2D TextureMask { get; set; }

    public override void _Ready()
    {
        if (TextureMask != null)
        {
            
        }
    }
}

