using UnityEngine;
using System.Collections;

public class ShakingDissapearPlatform : MonoBehaviour
{
    public float shakeDuration = 0.5f;
    public float shakeMagnitude = 0.1f;
    public float disappearDuration = 3f;
    public float appearDuration = 1f;
    public float appearDelay = 5f;
    public GameObject player;

    private Vector3 initialPosition;
    private bool isShaking = false;
    private bool isDisappearing = false;
    private bool isAppearing = false;
    private float disappearTime = 0f;
    private float appearTime = 0f;

    void Start()
    {
        initialPosition = transform.position;
        StartCoroutine(DisappearAndAppear());
    }

    void Update()
    {
        if (isShaking)
        {
            transform.position = initialPosition + Random.insideUnitSphere * shakeMagnitude;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == player)
        {
            isShaking = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject == player)
        {
            isShaking = false;
        }
    }

    IEnumerator DisappearAndAppear()
    {
        while (true)
        {
            if (!isDisappearing && !isAppearing)
            {
                isDisappearing = true;
                disappearTime = Time.time + disappearDuration;
            }
            if (isDisappearing && Time.time < disappearTime)
            {
                float disappearPercent = (disappearTime - Time.time) / disappearDuration;
                Color color = GetComponent<SpriteRenderer>().color;
                color.a = disappearPercent;
                GetComponent<SpriteRenderer>().color = color;
            }
            if (isDisappearing && Time.time >= disappearTime)
            {
                isDisappearing = false;
                isAppearing = true;
                appearTime = Time.time + appearDelay;
            }
            if (isAppearing && Time.time < appearTime)
            {
                yield return new WaitForSeconds(appearDuration);
                float appearPercent = (Time.time - appearTime + appearDelay) / appearDuration;
                Color color = GetComponent<SpriteRenderer>().color;
                color.a = appearPercent;
                GetComponent<SpriteRenderer>().color = color;
            }
            if (isAppearing && Time.time >= appearTime)
            {
                isAppearing = false;
            }
            yield return null;
        }
    }
}
