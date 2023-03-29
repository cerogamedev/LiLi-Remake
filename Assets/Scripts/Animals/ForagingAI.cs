using UnityEngine;
using System.Collections;

public class ForagingAI : MonoBehaviour
{

    public float moveSpeed = 3.0f;
    public float rotSpeed = 100.0f;
    public float forageDistance = 5.0f;
    public GameObject[] foodSources;

    private GameObject currentFood;

    void Start()
    {
        currentFood = GetClosestFood();
    }

    void Update()
    {
        if (currentFood == null)
        {
            currentFood = GetClosestFood();
        }
        else if (Vector3.Distance(transform.position, currentFood.transform.position) <= forageDistance)
        {
            Destroy(currentFood);
            currentFood = GetClosestFood();
        }
        else
        {
            Quaternion targetRotation = Quaternion.LookRotation(currentFood.transform.position - transform.position);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotSpeed * Time.deltaTime);
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }
    }

    GameObject GetClosestFood()
    {
        GameObject closestFood = null;
        float closestDistance = Mathf.Infinity;
        foreach (GameObject foodSource in foodSources)
        {
            float distance = Vector3.Distance(transform.position, foodSource.transform.position);
            if (distance < closestDistance)
            {
                closestFood = foodSource;
                closestDistance = distance;
            }
        }
        return closestFood;
    }
}
