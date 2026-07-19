using Godot;
using System;

public partial class TheBeginning : Location
{
	private int _labelNumber;

	[Export] public Label[] Labels { get; set; }
	[Export] public TextureRect[] TextureRects { get; set; }
	public string[] Text { get; set; }

	public override void _Ready()
	{
		base._Ready();
		Text = new string[Labels.Length];
		for (int i = 0; i < Labels.Length; i++)
			Text[i] = Labels[i].Text;
		Global.Music.PlayMusic("Ligronia.ogg");
		CutSceneCustomizes.Add((1, () =>
		{
			_labelNumber = 0;
			ChangeVisible(_labelNumber);
            Tween tween = CreateTween();
            tween.TweenMethod(new Callable(this, nameof(PrintText)), 0, Text[_labelNumber].Length, Text[_labelNumber].Length * 0.1);
			tween.TweenInterval(4);
            tween.TweenProperty(Labels[_labelNumber], "modulate:a", 0, 1);
            Tween tween2 = CreateTween();
			float startPosition = Labels[_labelNumber].Position.Y;
			tween2.TweenProperty(Labels[_labelNumber], "position:y", startPosition + 20, 3).SetTrans(Tween.TransitionType.Sine);
			tween2.Chain().TweenProperty(Labels[_labelNumber], "position:y", startPosition, 3).SetTrans(Tween.TransitionType.Sine);
			tween2.SetLoops(int.MaxValue);
        }
		));
		CutSceneCustomizes.Add((2, () =>
		{
			_labelNumber = 1;
            Vector2 startTextPosition = Labels[_labelNumber].Position;
			float startImagePosition = TextureRects[0].Position.X;
            Labels[_labelNumber].Position = new Vector2(startTextPosition.X - 150, Labels[_labelNumber].Position.Y);
			TextureRects[0].Position = new Vector2(startImagePosition + 150, TextureRects[0].Position.Y);
            Labels[_labelNumber].ChangeAlphaModulate();
            TextureRects[0].ChangeAlphaModulate();
            Tween tween = CreateTween();
            tween.TweenProperty(Labels[_labelNumber], "modulate:a", 1, 1);
            tween.Parallel().TweenProperty(TextureRects[0], "modulate:a", 1, 1);
            tween.Parallel().TweenProperty(Labels[_labelNumber], "position:x", startTextPosition.X, 2).SetTrans(Tween.TransitionType.Sine);
            tween.Parallel().TweenProperty(TextureRects[0], "position:x", startImagePosition, 2).SetTrans(Tween.TransitionType.Sine);
			tween.TweenInterval(4);
			tween.TweenProperty(Labels[_labelNumber], "modulate:a", 0, 1);
            tween.Parallel().TweenProperty(TextureRects[0], "modulate:a", 0, 1);
            tween.Parallel().TweenProperty(Labels[_labelNumber], "position:x", startTextPosition.X - 150, 2).SetTrans(Tween.TransitionType.Sine);
            tween.Parallel().TweenProperty(TextureRects[0], "position:x", startImagePosition + 150, 2).SetTrans(Tween.TransitionType.Sine);
            Tween tween2 = CreateTween();
            tween2.TweenProperty(Labels[_labelNumber], "position:y", startTextPosition.Y + 20, 3).SetTrans(Tween.TransitionType.Sine);
            tween2.Chain().TweenProperty(Labels[_labelNumber], "position:y", startTextPosition.Y, 3).SetTrans(Tween.TransitionType.Sine);
            tween2.SetLoops(int.MaxValue);
            ChangeVisible(_labelNumber);
            TextureRects[0].Visible = true;
			GetNode<AnimationPlayer>("%AnimationPlayer1").Play("animation");
		}
		));
		CutSceneCustomizes.Add((3, () =>
		{
			_labelNumber = 2;
            Vector2 startTextPosition = Labels[_labelNumber].Position;
            float startImagePosition = TextureRects[2].Position.Y;
            Labels[_labelNumber].Position -= new Vector2(0, 150);
            TextureRects[1].Position -= new Vector2(0, -150);
            Labels[_labelNumber].ChangeAlphaModulate();
            TextureRects[1].ChangeAlphaModulate();
            Tween tween = CreateTween();
            tween.TweenProperty(Labels[_labelNumber], "modulate:a", 1, 1);
            tween.Parallel().TweenProperty(TextureRects[1], "modulate:a", 1, 1);
            tween.Parallel().TweenProperty(Labels[_labelNumber], "position:y", startTextPosition.Y, 2).SetTrans(Tween.TransitionType.Sine);
            tween.Parallel().TweenProperty(TextureRects[1], "position:y", startImagePosition, 2).SetTrans(Tween.TransitionType.Sine);
            tween.TweenInterval(3);
			tween.TweenProperty(Labels[_labelNumber], "position:y", startTextPosition.Y - 150, 2).SetTrans(Tween.TransitionType.Sine);
			tween.Parallel().TweenProperty(TextureRects[1], "position:y", startImagePosition + 150, 2).SetTrans(Tween.TransitionType.Sine);
            tween.Parallel().TweenProperty(Labels[_labelNumber], "modulate:a", 0, 1);
            tween.Parallel().TweenProperty(TextureRects[1], "modulate:a", 0, 1);
            Tween tween2 = CreateTween();
			tween2.TweenInterval(2);
            tween2.TweenProperty(Labels[_labelNumber], "position:y", startTextPosition.Y + 20, 3).SetTrans(Tween.TransitionType.Sine);
            tween2.TweenProperty(Labels[_labelNumber], "position:y", startTextPosition.Y, 3).SetTrans(Tween.TransitionType.Sine);
            ChangeVisible(_labelNumber);
            TextureRects[1].Visible = true;
            GetNode<AnimationPlayer>("%AnimationPlayer2").Play("animation_2");
        }
		));
		CutSceneCustomizes.Add((4, () =>
		{
			_labelNumber = 3;
            Vector2 startText3Position = Labels[3].Position;
            Vector2 startText4Position = Labels[4].Position;
            Labels[3].Position -= new Vector2(0, 150); 
            Labels[4].Position -= new Vector2(0, -150);
            Labels[3].ChangeAlphaModulate();
            Labels[4].ChangeAlphaModulate();
            TextureRects[2].ChangeAlphaModulate();
            Tween tween = CreateTween();
			tween.TweenProperty(TextureRects[2], "modulate:a", 1, 1);
            tween.TweenInterval(1);
            tween.TweenProperty(Labels[3], "modulate:a", 1, 1);
			tween.Parallel().TweenProperty(Labels[3], "position:y", startText3Position.Y, 2).SetTrans(Tween.TransitionType.Sine);
			tween.Parallel().TweenMethod(new Callable(this, nameof(PrintText)), 0, Text[3].Length, Text[3].Length * 0.1);
			tween.TweenInterval(1);
            tween.TweenProperty(Labels[4], "modulate:a", 1, 1);
            tween.Parallel().TweenProperty(Labels[4], "position:y", startText4Position.Y, 2).SetTrans(Tween.TransitionType.Sine);
            tween.Parallel().TweenMethod(new Callable(this, nameof(PrintText4)), 0, Text[4].Length, Text[4].Length * 0.1);
            Tween tween2 = CreateTween();
			tween2.TweenInterval(7);
			tween2.TweenProperty(TextureRects[2], "scale", new Vector2(30, 30), 3).SetTrans(Tween.TransitionType.Expo);
			tween2.Parallel().TweenProperty(TextureRects[2], "rotation", Mathf.DegToRad(720), 3).SetTrans(Tween.TransitionType.Expo);
            Tween tween3 = CreateTween();
            tween3.TweenInterval(6);
            tween.TweenCallback(new Callable(this, nameof(ActivateAnimation3)));
            ChangeVisible(3);
            Labels[4].Visible = true;
			TextureRects[2].Visible = true;
        }
		));
		CutSceneCustomizes.Add((5, () =>
		{
			Global.Settings.CutScene = false;
			ConductivePath conductive = new ConductivePath()
			{
				Path = "Chapter1/StartingTrail",
				EndPosition = new Vector2(240, 350),
			};
			AddChild(conductive);
			conductive.Interaction();
		}
		));
		Global.CutSceneManager.OutputCutScene(0, 1);
	}

	public void PrintText(float symbolsCount)
	{
		Labels[_labelNumber].Text = Text[_labelNumber][0..(int)MathF.Floor(symbolsCount)];
	}

    public void PrintText4(float symbolsCount)
    {
        Labels[4].Text = Text[4][0..(int)MathF.Floor(symbolsCount)];
    }

	public void ActivateAnimation3() =>
            GetNode<AnimationPlayer>("%AnimationPlayer3").Play("animation_3");

    public void ChangeVisible(int number)
	{
		foreach (Label label in Labels)
			label.Visible = false;
		Labels[number].Visible = true;
	}
}
