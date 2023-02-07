using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunFX : MonoBehaviour
{
    public Light gunLight;

    private void Update()
    {
        gunLight.range = Random.Range(4.0f, 10.0f);
        transform.localScale = Vector3.one * Random.Range(2.0f, 4.0f);
        transform.localEulerAngles = new Vector3(270.0f, 0, Random.Range(0, 90.0f));
    }
}
