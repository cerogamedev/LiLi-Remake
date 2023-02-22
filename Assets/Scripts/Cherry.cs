using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cherry : MonoBehaviour
{
    public static int cherryInt;
    private Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
        cherryInt = 0;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            TriggerAnim();
    }
    public void TriggerAnim()
    {
        anim.SetTrigger("explode");
    }
    public void NumberRiser()
    {
        cherryInt += 1;
        Destroy(this.gameObject);
    }
}
