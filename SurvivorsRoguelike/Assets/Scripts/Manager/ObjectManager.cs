using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager
{
    public Player Player { get; private set; }
    public HashSet<NormalMonster> NormalMonsters { get; private set; } = new HashSet<NormalMonster>();
    public HashSet<Projectile> Projectiles { get; private set; } = new HashSet<Projectile>();
    public HashSet<Gem> Gems { get; } = new HashSet<Gem>();

    public T Spawn<T>(Vector3 position, Transform parent = null) where T : BaseObject
    {
        Type type = typeof(T);
        if (type == typeof(Player))
        {
            GameObject gameObject = Managers.Resource.Instantiate("Player.prefab", parent);
            gameObject.transform.position = position;

            Player player = gameObject.GetComponent<Player>();
            Player = player;
            return player as T;
        }
        else if (type == typeof(NormalMonster))
        {
            GameObject gameObject = Managers.Resource.Instantiate("Monster.prefab", parent);
            gameObject.transform.position = position;

            NormalMonster normalMonster = gameObject.GetComponent<NormalMonster>();
            NormalMonsters.Add(normalMonster);
            return normalMonster as T;
        }
        else if (type == typeof(Projectile))
        {
            GameObject gameObject = Managers.Resource.Instantiate("Bullet.prefab", parent);
            gameObject.transform.position = position;

            Projectile projectile = gameObject.GetComponent<Projectile>();
            Projectiles.Add(projectile);
            return projectile as T;
        }
        else if (type == typeof(Gem))
        {
            GameObject gameObject = Managers.Resource.Instantiate("Gem.prefab", parent);
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
            Despawn(Player);
        }
    }

    public void DespawnAllMonsters()
    {
        foreach (var monster in NormalMonsters)
        {
            Managers.Resource.Destroy(monster.gameObject);
        }
        NormalMonsters.Clear();
    }

    public void DespawnAllProjectiles()
    {
        foreach (var projectile in Projectiles)
        {
            Managers.Resource.Destroy(projectile.gameObject);
        }
        Projectiles.Clear();
    }

    public void DespawnAllGems()
    {
        foreach (var gem in Gems)
        {
            Managers.Resource.Destroy(gem.gameObject);
            Managers.Grid.Remove(gem.gameObject);
        }
        Gems.Clear();
    }

    public void Clear()
    {
        DespawnAllMonsters();
        DespawnAllProjectiles();
        DespawnAllGems();
        DespawnPlayer();
    }
}
