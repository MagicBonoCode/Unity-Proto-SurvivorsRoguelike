using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_IntroScene : UI_BaseScene
{
    private enum Buttons
    {
        Button_Start,
    }

    private Button _startButton;

    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Buttons));

        _startButton = GetButton((int)Buttons.Button_Start);

        BindEvent(_startButton.gameObject, OnClickStartButton);
    }

    private void OnClickStartButton(PointerEventData data)
    {
        Managers.UI.ShowPopupUI<UI_CharacterSelectPopup>();
    }
}
