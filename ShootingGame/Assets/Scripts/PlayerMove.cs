using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed;
    public float xMin, xMax, yMin, yMax;
    public float tilt;

    private void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(h, v, 0);

        // Translate : 인수로 주어진 벡터만큼 움직임
        transform.position += dir * speed * Time.deltaTime;

        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, xMin, xMax),
            Mathf.Clamp(transform.position.y, yMin, yMax),
            transform.position.z);

        transform.rotation = Quaternion.Euler(0.0f, h * -tilt, 0.0f);
    }
}
