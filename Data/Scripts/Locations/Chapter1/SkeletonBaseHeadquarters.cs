using Godot;
using System;

public partial class SkeletonBaseHeadquarters : Chapter1Location
{
    public override void _Ready()
    {
        base._Ready();
        Global.Music.PlayMusic("Skeleton.ogg");
        CutSceneCustomizes.Add((1, () =>
        {
            Tween tween = CreateTween();
            tween.TweenProperty(GetNode("%MenuDark"), "CurrentDarkPower", 1.6, 3);
            tween.TweenCallback(new Callable(this, nameof(OpenChangeLocation)));
        }
        ));
        switch (Global.CutSceneData.GetChoice(5, 1))
        {
            case 2:
                switch (Global.CutSceneData.GetChoice(5, 4))
                {
                    case 5:
                        switch (Global.CutSceneData.GetChoice(5, 21))
                        {
                            case 11:
                                switch (Global.CutSceneData.GetChoice(5, 22))
                                {
                                    case 17:
                                        Global.CutSceneManager.OutputCutScene(5, 18);
                                        break;
                                    case -1:
                                    case 13:
                                        Global.CutSceneManager.OutputCutScene(5, 22);
                                        break;
                                }
                                break;
                            case -1:
                            case 13:
                                Global.CutSceneManager.OutputCutScene(5, 21);
                                break;
                        }
                        break;
                    case 11:
                        switch (Global.CutSceneData.GetChoice(5, 22))
                        {
                            case 17:
                                Global.CutSceneManager.OutputCutScene(5, 18);
                                break;
                            case -1:
                            case 13:
                                Global.CutSceneManager.OutputCutScene(5, 22);
                                break;
                        }
                        break;
                    case -1:
                        Global.CutSceneManager.OutputCutScene(5, 4);
                        break;
                }
                break;
            case 3:
                switch (Global.CutSceneData.GetChoice(5, 7))
                {
                    case 9:
                        switch (Global.CutSceneData.GetChoice(5, 21))
                        {
                            case 11:
                                switch (Global.CutSceneData.GetChoice(5, 22))
                                {
                                    case 17:
                                        Global.CutSceneManager.OutputCutScene(5, 18);
                                        break;
                                    case -1:
                                    case 13:
                                        Global.CutSceneManager.OutputCutScene(5, 22);
                                        break;
                                }
                                break;
                            case 13:
                            case -1:
                                Global.CutSceneManager.OutputCutScene(5, 21);
                                break;
                        }
                        break;
                    case 8:
                    case 12:
                        switch (Global.CutSceneData.GetChoice(5, 14))
                        {
                            case 20:
                                switch (Global.CutSceneData.GetChoice(5, 21))
                                {
                                    case 16:
                                        switch (Global.CutSceneData.GetChoice(5, 21))
                                        {
                                            case 11:
                                                switch (Global.CutSceneData.GetChoice(5, 22))
                                                {
                                                    case 17:
                                                        Global.CutSceneManager.OutputCutScene(5, 18);
                                                        break;
                                                    case -1:
                                                    case 13:
                                                        Global.CutSceneManager.OutputCutScene(5, 22);
                                                        break;
                                                }
                                                break;
                                            case 13:
                                            case -1:
                                                Global.CutSceneManager.OutputCutScene(5, 21);
                                                break;
                                        }
                                        break;
                                    case 15:
                                        Global.CutSceneManager.OutputCutScene(5, 14);
                                        break;
                                }
                                break;
                            case 16:
                                switch (Global.CutSceneData.GetChoice(5, 21))
                                {
                                    case 11:
                                        switch (Global.CutSceneData.GetChoice(5, 22))
                                        {
                                            case 17:
                                                Global.CutSceneManager.OutputCutScene(5, 18);
                                                break;
                                            case -1:
                                            case 13:
                                                Global.CutSceneManager.OutputCutScene(5, 22);
                                                break;
                                        }
                                        break;
                                    case 13:
                                    case -1:
                                        Global.CutSceneManager.OutputCutScene(5, 21);
                                        break;
                                }
                                break;
                            case 15:
                            case -1:
                                Global.CutSceneManager.OutputCutScene(5, 14);
                                break;
                        }
                        break;
                    case -1:
                        Global.CutSceneManager.OutputCutScene(5, 7);
                        break;
                }
                break;
            case -1:
                Global.CutSceneManager.OutputCutScene(5, 1);
                break;
        }
    }

    public void OpenChangeLocation()
    {
        ConductivePath conductive = new ConductivePath()
        {
            Path = "Chapter1/SkeletonBase",
            EndPosition = new Vector2(832, -272),
        };
        AddChild(conductive);
        ((CameraNPC)Global.SceneObjects.Npcs.Find(x => x.ID == 3)).ChangeEnabled(false);
        Global.Variables.CutScene = false;
        conductive.Interaction();
    }
}
