using Godot;
using System;
using System.Collections.Generic;

[GlobalClass]
public partial class PAMS : Resource
{
    public List<PAData> PAData { get; set; }
    public List<FinalValues> FinalValues { get; set; }
    public string Music { get; set; }
    public int? FinalCustomize { get; set; }
}
