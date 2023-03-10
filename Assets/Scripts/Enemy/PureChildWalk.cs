using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PureChildWalk : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool isWalking = true;
    public float movementSpeed;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isWalking)
        {
            if (IsFacingRight())
                rb.velocity = new Vector2(movementSpeed, 0f);
            else
                rb.velocity = new Vector2(-movementSpeed, 0f);
        }
    }
    private bool IsFacingRight()
    {
        return transform.localScale.x > Mathf.Epsilon;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        transform.localScale = new Vector2(-(Mathf.Sign(rb.velocity.x)), transform.localScale.y);

    }
    
    
}
