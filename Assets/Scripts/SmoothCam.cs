using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCam : MonoBehaviour
{
    private float dirX = 0f;
    private float a;

    public float FollowSpeed = 2f;
    public float yOffset = 1.3f;
    public Transform target;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float dirX = Input.GetAxisRaw("Horizontal");

        if (dirX > 0)
        {
            a = +5;
        }
        if (dirX < 0)
        {
            a = -5;
        }
        Vector3 newPos = new Vector3(target.position.x + a, 0 + yOffset, -10f);
        transform.position = Vector3.Slerp(transform.position, newPos, FollowSpeed * Time.deltaTime);
    }
}
