using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCam : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] float timeOffset;
    [SerializeField] Vector2 posOffset;

    public float leftOrder, rightOrder;

    private Vector3 velocity;

    private void Update()
    {
        Vector3 startPos = transform.position;
        Vector3 endPos = player.transform.position;

        endPos.x += posOffset.x;
        endPos.y += posOffset.y;
        endPos.z = -10f;

        transform.position = Vector3.Lerp(startPos, endPos, timeOffset * Time.deltaTime);

        SetLeftToBottomBound(CheckPointSystem.CheckPointX - leftOrder, CheckPointSystem.CheckPointX + rightOrder, -4, +8);
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
