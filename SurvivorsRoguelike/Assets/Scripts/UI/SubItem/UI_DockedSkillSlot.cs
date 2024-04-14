using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_DockedSkillSlot : UI_Base
{
    private enum Images
    {
        Image_Icon,
    }

    private Image _iconImage;

    public override void Init()
    {
        Bind<Image>(typeof(Images));

        _iconImage = GetImage((int)Images.Image_Icon); 

        GetComponent<RectTransform>().localScale = Vector3.one;
    }

    public void SetDockedSkillSlot(Sprite icon)
    { 
        _iconImage.sprite = icon;
    }
}
