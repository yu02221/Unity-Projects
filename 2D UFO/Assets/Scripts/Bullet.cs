using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Vector3 target;

    private void Start()
    {

        target = new Vector3(transform.position.x,
            transform.position.y + 24.0f,
            transform.position.z);
    }
    private void Update()
    {
        transform.position = Vector3.MoveTowards(
            transform.position, target, 0.05f);
        if(transform.position == target)
        {
            Destroy(gameObject);
        }
    }
}
