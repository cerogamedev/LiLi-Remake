using UnityEngine;
using System.Collections;

public class FleeingAI : MonoBehaviour
{

    public float moveSpeed = 5.0f;
    public float fleeDistance = 10.0f;
    public float fleeSpeed = 10.0f;
    public float rotSpeed = 100.0f;
    public GameObject player;

    void Update()
    {
        Vector3 direction = transform.position - player.transform.position;
        if (direction.magnitude < fleeDistance)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(direction), rotSpeed * Time.deltaTime);
            transform.position += transform.forward * fleeSpeed * Time.deltaTime;
        }
        else
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(direction), rotSpeed * Time.deltaTime);
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }
    }
}
