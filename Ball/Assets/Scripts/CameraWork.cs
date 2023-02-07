using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraWork : MonoBehaviour
{
    public GameObject ball;

    private void Start()
    {
        // Find : GameObject�� �̸��� ���� ã�� ���
        // ball = GameObject.Find("Ball");

    }

    private void Update()
    {
        //Debug.Log("I am Camera. And ball is at " + ball.transform.position.z);
        transform.position = new Vector3(
            0,
            ball.transform.position.y + 6,
            ball.transform.position.z - 14);
    }
}
