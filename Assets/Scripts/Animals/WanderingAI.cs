using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class WanderingAI : MonoBehaviour
{

    public float moveSpeed = 3.0f;
    public float rotSpeed = 100.0f;
    public float moveTime = 2.0f;
    public float waitTime = 1.0f;
    public float wanderRange = 10.0f;

    private float moveTimer = 0.0f;
    private float waitTimer = 0.0f;
    private Vector3 targetPosition;

    void Start()
    {
        targetPosition = GetRandomPosition();
    }

    void Update()
    {
        if (moveTimer > 0)
        {
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
            moveTimer -= Time.deltaTime;
            if (moveTimer <= 0)
            {
                waitTimer = waitTime;
            }
        }
        if (waitTimer > 0)
        {
            waitTimer -= Time.deltaTime;
            if (waitTimer <= 0)
            {
                targetPosition = GetRandomPosition();
                moveTimer = moveTime;
            }
        }
        if (Vector3.Distance(transform.position, targetPosition) <= 0.1f)
        {
            targetPosition = GetRandomPosition();
        }
        Quaternion targetRotation = Quaternion.LookRotation(targetPosition - transform.position);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotSpeed * Time.deltaTime);
    }

    Vector3 GetRandomPosition()
    {
        Vector3 randomDirection = Random.insideUnitSphere * wanderRange;
        randomDirection += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, wanderRange, 1);
        return hit.position;
    }
}
