using Godot;
using System;
using System.Reflection.Emit;

public partial class Subconscious1 : Location
{
    private RichTextLabel _label;
    private string _text;

    public override void _Ready()
    {
        base._Ready();
        _label = GetNode<RichTextLabel>("%Titles");
        _text = _label.Text;
        _label.Text = string.Empty;
        CutSceneCustomizes.Add((1, () =>
        {
            GetNode<UIDark>("%MenuDark").CurrentDarkPower = 1.6f;
            _label.Visible = true;
            Tween tween = CreateTween();
            tween.TweenMethod(new Callable(this, "PrintText"), 0, _text.Length, 5);
        }));
        CutSceneCustomizes.Add((2, () =>
        {
            ((Falmer)Global.SceneObjects.Npcs.Find(x => x.ID == 4)).TimerScared.Start();
        }
        ));
        CutSceneCustomizes.Add((3, () =>
        {
            ((Falmer)Global.SceneObjects.Npcs.Find(x => x.ID == 4)).TimerScared.Stop();
        }
        ));
        CutSceneCustomizes.Add((4, () =>
        {
            Global.Variables.CutScene = false;
            GetNode<ConductivePath>("ConductivePath").Interaction();
        }
        ));
        Global.CutSceneManager.OutputCutScene(4, 1); 
    }

    public void PrintText(float symbolsCount)
    {
        _label.Text = _text[0..(int)MathF.Floor(symbolsCount)];
    }
}
