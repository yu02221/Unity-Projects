using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject bulletObj;

    private float timeCount;
    private float interval;
    private Vector3 offset1;
    private Vector3 offset2;
    private bool shootLeft;

    private void Start()
    {
        timeCount = 0;
        interval = 2.0f;
        offset1 = new Vector3(-0.11f, 1.8f, 0);
        offset2 = new Vector3(0.38f, 1.8f, 0);
        shootLeft = false;
    }

    private void Update()
    {
        timeCount += Time.deltaTime;

        if (timeCount > interval)
        {
            if (shootLeft)
            {
                Instantiate(bulletObj, transform.position + offset1,
                    Quaternion.identity);
                shootLeft = false;
            }
            else
            {
                Instantiate(bulletObj, transform.position + offset2,
                    Quaternion.identity);
                shootLeft = true;
            }
            
            timeCount = 0;
        }
        
    }
}
