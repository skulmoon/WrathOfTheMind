using Godot;
using System;
using System.Collections.Generic;

public partial class GameCreator : Control
{
    private int _currentButton;
    private LineEdit _textEdit;
    private VBoxContainer _container;
    private TextureButton _buttonNew;
    private TextureButton _buttonExit;

    public override void _Ready()
    {
        _buttonExit = GetNode<TextureButton>("ButtonExit");
        _buttonExit.Pressed += ExitButtonPressed;
        _buttonNew = GetNode<TextureButton>("ButtonNew");
        _buttonNew.Pressed += NewButtonPressed;
        _textEdit = GetNode<LineEdit>("TextEdit");
    }

    public void NewButtonPressed()
    {
        if (Global.Settings.Saves.Find(x => x.Name == _textEdit.Text) != null)
        {
            //I will add logic later
            //more later...
        }
        else
        {
            Global.JSON.NewGame(_textEdit.Text, Global.Settings.Saves.Count + 1);
            GetTree().ChangeSceneToFile($"res://Data/Scenes/Location/{Global.Settings.GameSettings.CurrentLocation}.tscn");
        }
    }

    public void ExitButtonPressed() =>
        GetTree().ChangeSceneToFile("res://Data/Scenes/Menu/MainMenu.tscn");
}
