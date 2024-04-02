using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Define
{
    public enum Scene
    {
        None,
        IntroScene,
        GameScene,
    }

    public enum UIEvent
    {
        None,
        Click,
    }

    public enum ObjectType
    {
        None,
        Player,
        Monster,
        Projectile,
        Gem,
    }

    public enum PawnState
    {
        None,
        Idle,
        Moving,
        Dead,
        Attacking,
    }

    public enum SpriteSortingOrder
    {
        None,
        Player = 9,
        Monster =10,
    }
}
