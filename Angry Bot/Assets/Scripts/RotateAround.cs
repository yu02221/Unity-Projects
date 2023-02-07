using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour
{
    public Transform target;
    public float speed;     // 5

    private void Update()
    {
        transform.RotateAround(target.position, Vector3.down, speed * Time.deltaTime);
    }
}
