using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : BaseObject
{
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
