using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointSystem : MonoBehaviour
{
    public static float CheckPointX;
    public static float CheckPointY;
    public GameObject glow;



    SmoothCam smoothcam;


    void Start()
    {
        CheckPointX = 0;
        CheckPointY = 0;
        glow.gameObject.SetActive(false);
    }

    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //checkpoint
            CheckPointX = transform.position.x;
            CheckPointY = transform.position.y;
            glow.gameObject.SetActive(true);
            //camera boundries
            
        }
    }
}
