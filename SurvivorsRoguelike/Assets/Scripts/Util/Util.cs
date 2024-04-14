using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Util : MonoBehaviour
{
    public static GameObject FindChild(GameObject gameObject, string name = null, bool recursive = false)
    {
        Transform transform = FindChild<Transform>(gameObject, name, recursive);
        if (transform == null)
        {
            return null;
        }

        return transform.gameObject;
    }

    public static T FindChild<T>(GameObject gameObject, string name = null, bool recursive = false) where T : Object
    {
        if (gameObject == null)
        {
            return null;
        }

        if (recursive == false)
        {
            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                Transform transform = gameObject.transform.GetChild(i);
                if (string.IsNullOrEmpty(name) || transform.name == name)
                {
                    T component = transform.GetComponent<T>();
                    if (component != null)
                    {
                        return component;
                    }
                }
            }
        }
        else
        {
            foreach (T component in gameObject.GetComponentsInChildren<T>())
            {
                if (string.IsNullOrEmpty(name) || component.name == name)
                {
                    return component;
                }
            }
        }

        return null;
    }

    public static Vector2 GenerateMonsterSpawnPosition(Vector2 characterPosition, float minDistance = 10.0f, float maxDistance = 20.0f)
	{
		float angle = Random.Range(0, 360) * Mathf.Deg2Rad;
		float distance = Random.Range(minDistance, maxDistance);

		float xDist = Mathf.Cos(angle) * distance;
		float yDist = Mathf.Sin(angle) * distance;

		Vector2 spawnPosition = characterPosition + new Vector2(xDist, yDist);

		return spawnPosition;
	}

    public static int RandomDamage(int damage)
    {
        int minDamage = (int)(damage * 0.8f);
        int maxDamage = (int)(damage * 1.2f);

        damage = Random.Range(minDamage, maxDamage + 1);

        return damage;
    }
}
