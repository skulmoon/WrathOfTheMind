using Godot;
using System;
using System.Collections.Generic;

public partial class GameCreator : Control
{
    private List<Save> _saves;
    private int _currentButton;
    private TextEdit _textEdit;
    private VBoxContainer _container;
    private Button _button;
    private int _buttonCount = 0;

    public override void _Ready()
    {
        _saves = Global.DirectoryManager.GetSaveNames();
        _saves.Sort();
        _container = GetNode<VBoxContainer>("/root/GameCreator/Saves/VBoxContainer");
        for (int i = 0; i < _saves.Count; i++)
        {
            var button = new Button();
            button.Text = _saves[i].Name;
            button.Name = _saves[i].Name;
            _container.AddChild(button);
        }
        _buttonCount = _saves.Count;
        _button = GetNode<Button>("/root/GameCreator/Button");
        _button.Pressed += NewButtonPressed;
        _textEdit = GetNode<TextEdit>("TextEdit");

    }

    public void NewButtonPressed()
    {
        var button = GetNode<Button>($"/root/GameCreator/Saves/VBoxContainer/{_textEdit.Text}");
        if ( button != null)
        {
            //I will add logic later
        }
        else
        {
            _buttonCount++;
            Global.DirectoryManager.CreateSave(_textEdit.Text, _buttonCount);
            Global.JSONManager.NewGame();
            GetTree().ChangeSceneToFile($"res://Data/Scenes/Location/{Global.Settings.CurrentLocation}.tscn");
        }
    }
}
