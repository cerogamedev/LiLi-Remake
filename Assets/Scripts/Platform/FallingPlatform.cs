using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    private Rigidbody2D rb;
    Vector2 initialPosition;
    bool platformMovingBack;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        initialPosition = transform.position;
    }
    private void Update()
    {
        if (platformMovingBack)
            transform.position = Vector2.MoveTowards(transform.position, initialPosition, 20f * Time.deltaTime);
        if (transform.position.y == initialPosition.y)
            platformMovingBack = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("LiLi") && !platformMovingBack)
            Invoke("DropPlatform", .5f);
    }
    void DropPlatform()
    {
        rb.isKinematic = false;
        Invoke("GetPlatformBack", 1f);
    }
    void GetPlatformBack()
    {
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;
        platformMovingBack = true;
    }
}
