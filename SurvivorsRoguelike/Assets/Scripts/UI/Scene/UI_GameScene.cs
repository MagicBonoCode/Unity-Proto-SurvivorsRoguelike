using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_GameScene : UI_BaseScene
{
    private enum GameObjects
    {
    }

    private enum Texts
    {
        Text_Exp,
        Text_PlayTime,
    }

    private enum Sliders
    {
        Slider_EXP,
    }

    private Text _expText;
    private Text _playTimeText;
    private Slider _expSlider;

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects));
        Bind<Text>(typeof(Texts));
        Bind<Slider>(typeof(Sliders));

        Managers.Event.RemoveEvent("EvUpdateExp", UpdateExpSlider);
        Managers.Event.AddEvent("EvUpdateExp", UpdateExpSlider);
        Managers.Event.RemoveEvent("EvInitializeGameSettings", UpdateExpSlider);
        Managers.Event.AddEvent("EvInitializeGameSettings", UpdateExpSlider);

        _expText = GetText((int)Texts.Text_Exp);
        _playTimeText = GetText((int)Texts.Text_PlayTime);
        _expSlider = GetSlider((int)Sliders.Slider_EXP);

        UpdatePlayTime();
        UpdateExpSlider();
    }

    private void LateUpdate()
    {
        UpdatePlayTime();
    }

    private void UpdateExpSlider()
    {
        if (Managers.Object.Player == null)
        {
            return;
        }

        GameScene gameScene = Managers.Scene.GetCurrentScene<GameScene>();
        float amount = gameScene.Exp / (float)gameScene.MaxExp;
        _expSlider.value = amount;
        _expText.text = string.Format("lv.{0}  {1} / {2}", gameScene.Level, gameScene.Exp, gameScene.MaxExp);
    }

    private void UpdatePlayTime()
    {
        GameScene gameScene = Managers.Scene.GetCurrentScene<GameScene>();
        TimeSpan timeSpan = TimeSpan.FromSeconds(gameScene.GameTimer);
        _playTimeText.text = string.Format("{0:00}:{1:00}", (int)timeSpan.TotalMinutes, timeSpan.Seconds);
    }
}
