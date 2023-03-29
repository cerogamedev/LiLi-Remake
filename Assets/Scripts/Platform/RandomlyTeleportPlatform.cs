using UnityEngine;
using System.Collections;

public class RandomlyTeleportPlatform : MonoBehaviour
{
    public float teleportCooldown = 5f; // cooldown time for teleportation
    public Transform[] teleportPoints; // array of possible teleport locations
    private float teleportTimer = 0f; // timer for teleport cooldown

    void Start()
    {
        teleportTimer = teleportCooldown;
    }

    void Update()
    {
        teleportTimer -= Time.deltaTime;
        if (teleportTimer <= 0f)
        {
            Teleport();
        }
    }

    void Teleport()
    {
        transform.position = GetRandomTeleportPoint();
        teleportTimer = teleportCooldown;
    }

    Vector3 GetRandomTeleportPoint()
    {
        return teleportPoints[Random.Range(0, teleportPoints.Length)].position;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Teleport();
        }
    }


}
