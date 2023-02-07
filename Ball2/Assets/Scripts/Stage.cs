using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    float speed = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float zRotation = transform.localEulerAngles.z;
        float xRotation = transform.localEulerAngles.x;

        zRotation = zRotation - (Input.GetAxis("Horizontal") * speed);
        xRotation = xRotation + (Input.GetAxis("Vertical") * speed);
        
        
        if(zRotation > 30.0f && zRotation < 90.0f)
        {
            zRotation = 30.0f;
        }
        if (xRotation > 30.0f && xRotation < 90.0f)
        {
            xRotation = 30.0f;
        }
        if (zRotation < 330.0f && zRotation > 270.0f)
        {
            zRotation = 330.0f;
        }
        if (xRotation < 330.0f && xRotation > 270.0f)
        {
            xRotation = 330.0f;
        }

        transform.localEulerAngles = new Vector3(xRotation, 0, zRotation);

    }
}
