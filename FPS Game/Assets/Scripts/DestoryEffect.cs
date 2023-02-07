using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryEffect : MonoBehaviour
{
    public float destroyTime = 1.5f;

    public AudioSource bombSound;

    private void Start()
    {
        bombSound.Play();

        Destroy(gameObject, destroyTime);
    }
}
