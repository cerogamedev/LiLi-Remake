using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Butterflypet : MonoBehaviour
{
    public GameObject sLetter;
    public static float petTrigger;
    public float petTriggerKey;

    [SerializeField] private GameObject[] waypoints;
    private int currentWaypointIndex = 0;

    [SerializeField] private float speed = 8f;
    private GameObject player;

    void Start()
    {
        sLetter.SetActive(false);

        petTrigger = 0;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if (petTriggerKey == 1 && Input.GetKeyDown(KeyCode.Space))
        {
            petTrigger = 1;
            player.GetComponent<Gripple>().enabled = true;
        }

        if (petTrigger == 1)
        {
            if (Vector2.Distance(waypoints[currentWaypointIndex].transform.position, transform.position) < .1f)
            {
                currentWaypointIndex++;
                if (currentWaypointIndex >= waypoints.Length)
                {
                    currentWaypointIndex = 0;
                }
            }
            transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position, Time.deltaTime * speed);
            sLetter.SetActive(false);

        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {


        petTriggerKey = 1;

        if (petTrigger == 0)
        {
            sLetter.SetActive(true);
        }



    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        sLetter.SetActive(false);
        petTriggerKey = 0;
    }


}