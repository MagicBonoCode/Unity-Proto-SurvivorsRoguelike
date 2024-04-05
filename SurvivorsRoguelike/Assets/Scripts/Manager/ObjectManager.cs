﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectManager
{
    public Player Player { get; private set; }
    public HashSet<NormalMonster> NormalMonsters { get; private set; } = new HashSet<NormalMonster>();
    public HashSet<Projectile> Projectiles { get; private set; } = new HashSet<Projectile>();
    public HashSet<Gem> Gems { get; } = new HashSet<Gem>();

    public T Spawn<T>(Vector3 position) where T : BaseObject
    {
        Type type = typeof(T);
        if (type == typeof(Player))
        {
            GameObject gameObject = Managers.Resource.Instantiate("Player.prefab");
            gameObject.transform.position = position;

            Player player = gameObject.GetComponent<Player>();
            Player = player;
            return player as T;
        }
        else if (type == typeof(NormalMonster))
        {
            GameObject gameObject = Managers.Resource.Instantiate("Monster.prefab");
            gameObject.transform.position = position;

            NormalMonster normalMonster = gameObject.GetComponent<NormalMonster>();
            NormalMonsters.Add(normalMonster);
            return normalMonster as T;
        }
        else if (type == typeof(Projectile))
        {
            GameObject gameObject = Managers.Resource.Instantiate("Bullet.prefab");
            gameObject.transform.position = position;

            Projectile projectile = gameObject.GetComponent<Projectile>();
            Projectiles.Add(projectile);
            return projectile as T;
        }
        else if (type == typeof(Gem))
        {
            GameObject gameObject = Managers.Resource.Instantiate("Gem.prefab");
            gameObject.transform.position = position;

            Gem gem = gameObject.GetComponent<Gem>();
            Gems.Add(gem);

            Managers.Grid.Add(gameObject);

            return gem as T;
        }

        return null;
    }

    public void Despawn<T>(T obj) where T : BaseObject
    {
        Type type = typeof(T);
        if (type == typeof(Player))
        {
            Player = null;
            Managers.Resource.Destroy(obj.gameObject);
        }
        else if (type == typeof(NormalMonster))
        {
            NormalMonsters.Remove(obj as NormalMonster);
            Managers.Resource.Destroy(obj.gameObject);
        }
        else if (type == typeof(Projectile))
        {
            Projectiles.Remove(obj as Projectile);
            Managers.Resource.Destroy(obj.gameObject);
        }
        else if (type == typeof(Gem))
        {
            Gems.Remove(obj as Gem);
            Managers.Resource.Destroy(obj.gameObject);

            Managers.Grid.Remove(obj.gameObject);
        }
    }

    public void DespawnPlayer()
    {
        if (Player != null)
        {
            Despawn<Player>(Player);
        }
    }

    public void DespawnAllMonsters()
    {
        foreach (var monster in NormalMonsters)
        {
            Despawn<NormalMonster>(monster);
        }
    }

    public void DespawnAllProjectiles()
    {
        foreach (var projectile in Projectiles)
        {
            Despawn<Projectile>(projectile);
        }
    }

    public void DespawnAllGems()
    {
        foreach (var gem in Gems)
        {
            Despawn<Gem>(gem);
        }
    }

    public void Clear()
    {
        DespawnAllMonsters();
        DespawnAllProjectiles();
        DespawnAllGems();
        DespawnPlayer();
    }
}