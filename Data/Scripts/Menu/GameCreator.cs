using Godot;
using System;
using System.Collections.Generic;

public partial class GameCreator : Control
{
    private int _currentButton;
    private TextEdit _textEdit;
    private VBoxContainer _container;
    private Button _button;

    public override void _Ready()
    {
        _container = GetNode<VBoxContainer>("/root/GameCreator/Saves/VBoxContainer");
        for (int i = 0; i < Global.Settings.Saves.Count; i++)
        {
            var button = new Button();
            button.Text = Global.Settings.Saves[i].Name;
            button.Name = Global.Settings.Saves[i].Name;
            _container.AddChild(button);
        }
        _button = GetNode<Button>("/root/GameCreator/Button");
        _button.Pressed += NewButtonPressed;
        _textEdit = GetNode<TextEdit>("TextEdit");

    }

    public void NewButtonPressed()
    {
        var button = GetNode<Button>($"/root/GameCreator/Saves/VBoxContainer/{_textEdit.Text}");
        if (button != null)
        {
            //I will add logic later
        }
        else
        {
            Global.JSON.NewGame(_textEdit.Text, Global.Settings.Saves.Count + 1);
            GetTree().ChangeSceneToFile($"res://Data/Scenes/Location/{Global.Settings.GameSettings.CurrentLocation}.tscn");
        }
    }
}
