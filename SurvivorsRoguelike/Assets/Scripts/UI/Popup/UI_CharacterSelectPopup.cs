using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_CharacterSelectPopup : UI_BasePopup
{
    private enum Buttons
    {
        Button_Ok,
    }

    private Button _okButton;

    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Buttons));

        _okButton = GetButton((int)Buttons.Button_Ok);

        BindEvent(_okButton.gameObject, OnClickOkButton);
    }

    private void OnClickOkButton(PointerEventData data)
    {
        Managers.Scene.LoadScene(Define.Scene.GameScene);
    }
}
