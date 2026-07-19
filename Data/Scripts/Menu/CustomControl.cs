using Godot;
using System;
using static System.Net.Mime.MediaTypeNames;

public partial class CustomControl : Control
{
    const int INTERFACE_TEORETICAL_Y_SIZE = 256;

    [Export] public bool AutoCorrectSize { get; set; } = false;
    [Export] public float TheoreticalXSize { get; set; }
    [Export(PropertyHint.Range, "0,1")] public float RatioX { get; set; } = 0.5f;
    [Export] public bool AutoCorrectPivotOffset { get; set; } = false;
    [Export(PropertyHint.Range, "0,1,or_greater,or_less")] public float PivotOffsetAnchorX { get; set; } = 0;
    [Export(PropertyHint.Range, "0,1,or_greater,or_less")] public float PivotOffsetAnchorY { get; set; } = 0;
    [Export] public bool CustomYSize { get; set; } = false;
    [Export] public float TheoreticalYSize { get; set; }

    public override void _Ready()
    {
        if (AutoCorrectSize)
        {
            float xSize;
            if (!CustomYSize)
                xSize = (Size.Y / ((AnchorBottom - AnchorTop) * INTERFACE_TEORETICAL_Y_SIZE)) * TheoreticalXSize;
            else
                xSize = (Size.Y / TheoreticalYSize) * TheoreticalXSize;
            OffsetLeft = -(xSize * (1 - RatioX));
            OffsetRight = (xSize * RatioX);
        }
        if (AutoCorrectPivotOffset)
        {
            PivotOffset = new Vector2(Size.X * PivotOffsetAnchorX, Size.Y * PivotOffsetAnchorY);
        }
        base._Ready();
    }
}
