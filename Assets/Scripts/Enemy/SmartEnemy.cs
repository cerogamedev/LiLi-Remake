using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartEnemy : MonoBehaviour
{
    public float moveSpeed;
    public int maxLives;
    public int currentLives;
    public float attackRadius;
    public float healDuration;
    public Transform[] healPoints;

    private bool isAttacking;
    private bool isHealing;
    private Transform player;
    private Animator anim;

    void Start()
    {
        currentLives = maxLives;
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        isAttacking = false;
        isHealing = false;
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (currentLives > 0)
        {
            if (distanceToPlayer <= attackRadius)
            {
                isAttacking = true;
                anim.SetBool("isAttacking", true);
            }
            else
            {
                isAttacking = false;
                anim.SetBool("isAttacking", false);
                Move();
            }

            if (isHealing)
            {
                return;
            }
            else if (currentLives < maxLives)
            {
                StartCoroutine(Heal());
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
    }

    IEnumerator Heal()
    {
        isHealing = true;
        anim.SetBool("isHealing", true);

        // Pick a random healing point
        int index = Random.Range(0, healPoints.Length);
        Transform healPoint = healPoints[index];

        // Move to the healing point
        while (transform.position != healPoint.position)
        {
            transform.position = Vector2.MoveTowards(transform.position, healPoint.position, moveSpeed * Time.deltaTime);
            yield return null;
        }

        // Heal for the specified duration
        yield return new WaitForSeconds(healDuration);
        currentLives = maxLives;

        // Move back to the player
        while (transform.position != player.position)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
            yield return null;
        }

        isHealing = false;
        anim.SetBool("isHealing", false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && isAttacking)
        {
            other.GetComponent<Death>().Die();
        }
    }

    public void TakeDamage()
    {
        currentLives--;

        if (currentLives <= 0)
        {
            anim.SetTrigger("death");
        }
        else
        {
            anim.SetTrigger("hit");
        }
    }

    public void ResetAttack()
    {
        isAttacking = false;
        anim.SetBool("isAttacking", false);
    }
}
