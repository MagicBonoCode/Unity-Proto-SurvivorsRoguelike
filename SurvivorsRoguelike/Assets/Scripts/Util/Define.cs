using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Define
{
    public enum Scene
    {
        Default,
        IntroScene,
        GameScene,
    }

    public enum GameSceneState
    { 
        Default,
        Play,
        Stop,
        PlayerDead,
    }

    public enum UIEvent
    {
        Default,
        Click,
    }

    public enum ObjectType
    {
        Default,
        Player,
        Monster,
        Projectile,
        Gem,
    }

    public enum PawnState
    {
        Default,
        Idle,
        Moving,
        Dead,
        Attacking,
    }

    public enum SpriteSortingOrder
    {
        Default,
        Player = 9,
        Monster =10,
    }
}
