using Godot;
using System;
using System.Collections.Generic;

public partial class GameLoader : Control
{
    private VBoxContainer _container;
    private int _buttonCount = 0;
	private TextureButton _loadButton;
	private TextureButton _deleteButton;
	private string _currentSave;

	public override void _Ready()
	{
        Control saves = GetNode<Control>("Saves");
        float pixelSize = saves.Size.Y / 144;
        saves.OffsetRight = pixelSize * 104;
        saves.OffsetLeft = -saves.OffsetRight;
        _container = GetNode<VBoxContainer>("/root/MainMenu/GameLoader/Saves/VBoxContainer");
        for (int i = 0; i < Global.Variables.Saves.Count; i++)
        {
            var button = new NameSaveButton();
            button.CustomMinimumSize = new Vector2(0, pixelSize * 26);
            button.Text = Global.Variables.Saves[i].Name;
            button.Name = Global.Variables.Saves[i].Name;
            button.CurrentSaveName += ChangeCurrentButton;
            _container.AddChild(button);
        }
        _buttonCount = Global.Variables.Saves.Count;
        _loadButton = GetNode<TextureButton>("Options/Load");
        _loadButton.Pressed += OnPressedLoad;
        _deleteButton = GetNode<TextureButton>("Options/Delete"); 
        _deleteButton.Pressed += OnPressedDelete;
    }

	public void ChangeCurrentButton(string name)
	{
        _currentSave = name;
    }

    public void OnPressedLoad()
    {
        if (_currentSave != null)
            GetNode<UIDark>("%Dark").ShowDark(LoadSave);
    }

    public void LoadSave()
    {
        Global.SaveManager.LoadSave(_currentSave);
        Global.SceneObjects.Storage.GetTree().ChangeSceneToFile($"res://Data/Scenes/Location/{Global.Variables.SaveData.CurrentLocation}.tscn");
    }

    public void OnPressedDelete()
    {
        if (_currentSave != null)
        {
            _container.RemoveChild(GetNode($"/root/MainMenu/GameLoader/Saves/VBoxContainer/{_currentSave}"));
            Global.SaveManager.DeleteSave(_currentSave);
            _currentSave = null;
        }
    }
}
