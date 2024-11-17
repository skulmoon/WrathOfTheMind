using Godot;
using System;
using System.Collections.Generic;

public partial class GameCreator : Control
{
    private int _currentButton;
    private TextEdit _textEdit;
    private VBoxContainer _container;
    private Button _buttonNew;
    private Button _buttonExit;

    public override void _Ready()
    {
        _buttonExit = GetNode<Button>("ButtonExit");
        _buttonExit.Pressed += ExitButtonPressed;
        _buttonNew = GetNode<Button>("ButtonNew");
        _buttonNew.Pressed += NewButtonPressed;
        _textEdit = GetNode<TextEdit>("TextEdit");
    }

    public void NewButtonPressed()
    {
        if (Global.Settings.Saves.Find(x => x.Name == _textEdit.Text) != null)
        {
            //I will add logic later
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
