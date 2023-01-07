using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceSingleton : MonoBehaviour
{
    public static AudioSourceSingleton instance;

    public void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
