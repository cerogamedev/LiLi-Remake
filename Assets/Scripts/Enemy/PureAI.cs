using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PureAI : MonoBehaviour
{
    public float movementSpeed;
    private bool isWalking = true;
    [SerializeField] GameObject child;
    [SerializeField] GameObject childPos;
    float range;

    private Animator anim;
    private BoxCollider2D _collider;
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        range = Random.Range(2, 4);
    }

    void Update()
    {
        if (isWalking)
        {
            if (IsFacingRight())
                rb.velocity = new Vector2(movementSpeed, 0f);
            else
                rb.velocity = new Vector2(-movementSpeed, 0f);
        }
        else
            rb.velocity = new Vector2(0, 0);
        range -= Time.deltaTime;
        if (range <= 0)
        {
            Borning();
            range = Random.Range(4, 8);
        }

    }
    private bool IsFacingRight()
    {
        return transform.localScale.x > Mathf.Epsilon;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Order-Pure"))
        {
            transform.localScale = new Vector2(-(Mathf.Sign(rb.velocity.x)), transform.localScale.y);


        }
    }

    public void Borning()
    {
        isWalking = false;
        anim.SetTrigger("Born");
    }
    public void PureChild()
    {
        Instantiate(child, childPos.transform.position, Quaternion.identity);
        anim.Play("Pure-walk");
        isWalking = true;
    }
}
