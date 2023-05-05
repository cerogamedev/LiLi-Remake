using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Telescope : MonoBehaviour
{
    public GameObject Parallax;
    public bool _insideCheck;
    public GameObject QuestionMark;
    void Start()
    {
        SmoothCam.leftOrder = -1;
        SmoothCam.rightOrder = 44;
        QuestionMark.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && _insideCheck == true)
        {
            Camera.main.orthographicSize = 16;
            Parallax.transform.localScale = new Vector3(0.98f, 1, 1);
            SmoothCam.leftOrder = -26;
            SmoothCam.rightOrder = 44;
        }
        else
        {


        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Camera.main.orthographicSize = 7;
            Parallax.transform.localScale = new Vector3(0.42f, .43f, 1);
            SmoothCam.leftOrder = -1;
            SmoothCam.rightOrder = 44;
            _insideCheck = false;
            QuestionMark.SetActive(false);
        }


    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _insideCheck = true;
            QuestionMark.SetActive(true);
        }

    }
}
