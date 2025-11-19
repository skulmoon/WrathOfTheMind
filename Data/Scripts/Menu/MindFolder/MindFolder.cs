using Godot;
using System;

public partial class MindFolder : CanvasLayer
{
    [Export(PropertyHint.File)] public FolderCell FolderCell { get; set; }
}
