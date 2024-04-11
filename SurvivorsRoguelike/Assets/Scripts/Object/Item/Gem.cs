using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : BaseObject
{
    public int Value { get; private set; } = 1; // TODO: Move to data

    protected override bool Init()
    {
        if (base.Init() == false)
        {
            return false;
        }

        ObjectType = Define.ObjectType.Gem;

        return true;
    }
}
