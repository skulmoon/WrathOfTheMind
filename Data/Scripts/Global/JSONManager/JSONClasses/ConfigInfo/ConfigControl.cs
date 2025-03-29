using Godot;
using System;
using System.Collections.Generic;

public class ConfigControl
{
    public List<(string, List<(long?, int?)>)> KeyActionList { get; set; }
}
