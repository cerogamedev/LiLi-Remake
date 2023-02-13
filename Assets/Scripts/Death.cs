using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Death : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

    }

    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            Die();
        }
    }
    private void Die()
    {
        rb.bodyType = RigidbodyType2D.Static;
        anim.SetTrigger("Death");

    }
    private void ReBorn()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        transform.position = new Vector2(CheckPointSystem.CheckPointX, CheckPointSystem.CheckPointY);
        anim.SetTrigger("Reborn");

    }
}
