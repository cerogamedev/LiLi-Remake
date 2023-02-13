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



    void Start()
    {
        CheckPointSystem.CheckPointX = 0;
    }

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
        SetLeftToBottomBound(CheckPointSystem.CheckPointX - 2, CheckPointSystem.CheckPointX + 12, -2, 2);

        if (target.position.x > (CheckPointSystem.CheckPointX - 2) && target.position.x < (CheckPointSystem.CheckPointX + 10))
        {
            Vector3 newPos = new Vector3(target.position.x + a, 0 + yOffset, -10f);

            transform.position = Vector3.Slerp(transform.position, newPos, FollowSpeed * Time.deltaTime);

        }


    }
    public void SetLeftToBottomBound(float leftLimit, float rightLimit, float bottomLimit, float topLimit)
    {
        transform.position = new Vector3
    (
    Mathf.Clamp(transform.position.x, leftLimit, rightLimit),
    Mathf.Clamp(transform.position.y, bottomLimit, topLimit)
    );
    }
}
