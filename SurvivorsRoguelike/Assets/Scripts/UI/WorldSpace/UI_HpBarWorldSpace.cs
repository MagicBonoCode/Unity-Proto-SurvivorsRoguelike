using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_HpBarWorldSpace : UI_Base
{
    private enum Sliders
    {
        Slider_HpBar,
    }

    [SerializeField] private BasePawn _owner;

    private Slider _hpBarSlider;

    public override void Init()
    {
        Bind<Slider>(typeof(Sliders));

        _hpBarSlider = GetSlider((int)Sliders.Slider_HpBar);
    }

    private void LateUpdate()
    {
        if(_owner == null)
        {
            return;
        }

        float amount = _owner.Hp / (float)_owner.MaxHp;
        _hpBarSlider.value = amount;
    }
}
