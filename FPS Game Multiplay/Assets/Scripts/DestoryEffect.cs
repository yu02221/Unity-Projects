using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class DestoryEffect : NetworkBehaviour
{
    public float destroyTime = 1.5f;

    float currentTime = 0;

    private void Update()
    {
        if (currentTime > destroyTime)
            Runner.Despawn(Object);

        currentTime += Time.deltaTime;
    }
}
