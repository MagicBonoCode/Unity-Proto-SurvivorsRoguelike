using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_GameOverPopup : UI_BasePopup
{
    private enum Buttons
    {
        Button_Replay,
        Button_CharacterSelect,
    }

    private Button _replayButton;
    private Button _characterSelectButton;

    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Buttons));

        _replayButton = GetButton((int)Buttons.Button_Replay);
        _characterSelectButton = GetButton((int)Buttons.Button_CharacterSelect);

        BindEvent(_replayButton.gameObject, OnClickReplayButton);
        BindEvent(_characterSelectButton.gameObject, OnClickCharacterSelectButton);
    }

    private void OnClickReplayButton(PointerEventData data)
    {
        Managers.Scene.GetCurrentScene<GameScene>().ReplayGame();
        ClosePopupUI();
    }

    private void OnClickCharacterSelectButton(PointerEventData data)
    {
        Managers.UI.ShowPopupUI<UI_CharacterSelectPopup>();
    }
}
