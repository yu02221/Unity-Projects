using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFallow : MonoBehaviour
{
    public GameObject targetObject;
    private float distanceToTarget;

    private void Start()
    {
        distanceToTarget = transform.position.x - targetObject.transform.position.x;
    }

    private void Update()
    {
        float targeObjectX = targetObject.transform.position.x;
        transform.position = new Vector3(targeObjectX + distanceToTarget, 
            transform.position.y, transform.position.z);
    }
}
