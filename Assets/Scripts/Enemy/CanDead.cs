using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanDead : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    private CircleCollider2D cc2d;
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        cc2d = GetComponent<CircleCollider2D>();
    }

 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "AttackHitBox")
        {
            anim.Play("Death");
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            cc2d.enabled = !cc2d.enabled;
            Death();

        }
    }
    public void Death()
    {
        Destroy(this.gameObject);
    }
}
