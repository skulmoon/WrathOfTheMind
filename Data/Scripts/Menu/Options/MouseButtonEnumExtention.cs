using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Godot;

static class MouseButtonEnumExtension
{
    public static string GetName(this MouseButton button)
    {
        return button switch
        {
            MouseButton.Left => "Mouse left",
            MouseButton.Right => "Mouse right",
            MouseButton.Middle => "Mouse middle",
            MouseButton.Xbutton1 => "Mouse 4",
            MouseButton.Xbutton2 => "Mouse 5",
            MouseButton.WheelUp => "Scrolling up",
            MouseButton.WheelDown => "Scrolling down",
            _ => button.ToString()
        };
    }
}
