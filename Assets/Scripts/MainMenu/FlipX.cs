using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipX : MonoBehaviour
{
    // Start is called before the first frame update

    public SpriteRenderer sprite;

    Vector2 lastPos;
    void Update()
    {
        Vector2 currentPos = transform.position;
        Vector2 deltaPos = currentPos - lastPos;
        if (deltaPos.x >= 0)
            sprite.flipX = false;
        else
            sprite.flipX = true;

        lastPos = transform.position;
    }

}
