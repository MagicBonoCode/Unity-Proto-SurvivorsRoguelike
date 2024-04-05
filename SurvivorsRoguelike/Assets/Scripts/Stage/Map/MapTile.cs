using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTile : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag != "Area" || Managers.Object.Player.PawnState == Define.PawnState.Dead)
        {
            return;
        }

        Vector3 dir = collision.transform.position - transform.position;

        float dirX = dir.x < 0 ? -1 : 1;
        float dirY = dir.y < 0 ? -1 : 1;

        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
        {
            transform.Translate(Vector3.right * dirX * 40);
        }
        else
        { 
            transform.Translate(Vector3.up * dirY * 40);
        }
    }
}
