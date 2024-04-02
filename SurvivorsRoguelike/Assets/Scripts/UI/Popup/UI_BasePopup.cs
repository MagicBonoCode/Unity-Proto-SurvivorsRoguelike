using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UI_BasePopup : UI_Base
{
    public override void Init()
    {
        Managers.UI.SetCanvas(gameObject, true);
    }

    public virtual void ClosePopupUI()
    {
        Managers.UI.ClosePopupUI();
    }
}
