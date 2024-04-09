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
        Text_PlayTime,
    }

    private enum Sliders
    {
        Slider_EXP,
    }

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
        Managers.Event.RemoveEvent("EvReplayGame", UpdateExpSlider);
        Managers.Event.AddEvent("EvReplayGame", UpdateExpSlider);

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

        Player player = Managers.Object.Player;
        float amount = (float)player.EXP / 100;
        _expSlider.value = amount;
    }

    private void UpdatePlayTime()
    {
        if (Managers.Scene.CurrentScene is GameScene gameScene)
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(gameScene.GameTimer);
            _playTimeText.text = string.Format("{0:00}:{1:00}", (int)timeSpan.TotalMinutes, timeSpan.Seconds);
        }
    }
}
