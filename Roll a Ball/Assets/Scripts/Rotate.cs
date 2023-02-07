using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float rotSpeed;
    private void Update()
    {
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime * rotSpeed);
    }
}
