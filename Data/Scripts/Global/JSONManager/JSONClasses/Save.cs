using Godot;
using System;

public class Save : IComparable<Save>
{
    public string Name { get; set; }
    public int Number { get; set; }

    public int CompareTo(Save compareNumber)
    {
        if (compareNumber == null)
            return 1;

        else
            return Number.CompareTo(compareNumber.Number);
    }
}
