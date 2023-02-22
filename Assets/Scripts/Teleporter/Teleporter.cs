using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField] private Transform destination;

    private Animator anim;
    private bool playerIn = false;
    public PlayerTeleporter playerteleporter;
    private void Start()
    {
        anim = GetComponent<Animator>();

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerIn = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerIn = false;
        }
    }
    private void Update()
    {
        if (playerIn && Input.GetKeyDown(KeyCode.E))
            anim.SetTrigger("PortalStart");
    }
    public Transform GetDestination()
    {
        return destination;
    }
    void TriggerPortal()
    {
        playerteleporter.TriggerPicard();
    }
    
}
