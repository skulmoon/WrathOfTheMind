using Godot;
using System;

public static class Logger
{
    public static void PrintInfo(object message) =>
        GD.Print($"[Info] {message}");

    public static void PrintError(object message) =>
		GD.Print($"[Error] {message}");

    public static void PrintWarring(object message) =>
        GD.Print($"[Warring] {message}");
}
