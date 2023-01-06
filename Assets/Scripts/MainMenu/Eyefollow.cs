using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eyefollow : MonoBehaviour
{
    public GameObject player;
    void Start()
    {

    }

    void Update()
    {
        EyeFollow();
    }
    void EyeFollow()
    {
        Vector3 playerpos = player.transform.position;

        Vector2 direction = new Vector2(playerpos.x - transform.position.x, playerpos.y - transform.position.y);
        transform.up = direction;
    }
}
