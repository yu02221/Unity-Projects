using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public GameObject target;   // player
    public float distance;  // 5
    public float height;    // 8
    public float speed;     // 2

    private Vector3 pos;

    private void Update()
    {
        pos = new Vector3(
            target.transform.position.x,
            height,
            target.transform.position.z - distance);

        //Vector3.MoveTowards()

        transform.position = Vector3.Lerp(
            transform.position,
            pos,
            speed * Time.deltaTime);
    }
}
