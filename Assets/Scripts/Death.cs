using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Death : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private BoxCollider2D _collider;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        _collider = GetComponent<BoxCollider2D>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap") || collision.gameObject.CompareTag("Bug"))
        {
            Die();
        }
    }
    private void Die()
    {
        rb.bodyType = RigidbodyType2D.Static;
        _collider.enabled = !_collider.enabled;
        anim.SetTrigger("Death");

    }
    public void ReBorn()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        transform.position = new Vector2(CheckPointSystem.CheckPointX, CheckPointSystem.CheckPointY);
        anim.SetTrigger("Reborn");
        _collider.enabled = !_collider.enabled;

    }
}
