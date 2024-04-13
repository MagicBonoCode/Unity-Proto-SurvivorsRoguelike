using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_DamageTextWorldSpace : UI_Base
{
    private enum Texts
    {
        Text_Amount,
    }

    private Text _amountText;

    public override void Init()
    {
        Bind<Text>(typeof(Texts));

        _amountText = GetText((int)Texts.Text_Amount);
    }

    public void SetDamageText(long amount)
    {
        _amountText.text = string.Format("{0:n0}", amount);
    }

    public void OnDestroyDamageText()
    {
        Managers.Resource.Destroy(gameObject);
    }
}
