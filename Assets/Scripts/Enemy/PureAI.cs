using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PureAI : MonoBehaviour
{
    public float walkableRange;

    private Animator anim;
    private BoxCollider2D _collider;
    private Rigidbody2D rb;
    public SpriteRenderer sprite;
    Vector2 lastPos;
    void Start()
    {
        
    }


    void Update()
    {
        Vector2 currentPos = transform.position;
        Vector2 deltaPos = currentPos - lastPos;
        if (deltaPos.x <= 0)
            sprite.flipX = false;
        else
            sprite.flipX = true;

        lastPos = transform.position;
    }

}
