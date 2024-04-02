using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UI_BaseScene : UI_Base
{
    public override void Init()
    {
        Managers.UI.SetCanvas(gameObject, false);
    }
}
