using Godot;
using System;

public partial class ButtonExitTrade : Button
{
    public void OnPressed() =>
        Global.SceneObjects.TradeMenu.Close();
}
