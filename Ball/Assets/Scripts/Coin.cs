using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "Ball")
        {
            GameManager gm = GameObject.Find("GameManager")
                .GetComponent<GameManager>();
            gm.GetCoin();

            // Destroy(col.gameObject);    // Ball
            Destroy(gameObject);        // Coin
            // �ڱ� �ڽ��� �� �������� ���ִ°� ������
        }
    }
    private void Update()
    {
        // ������ ��� ȸ���ϵ��� ����
        float zRotation = transform.localEulerAngles.z;
        zRotation = zRotation - 1.0f;
        transform.localEulerAngles = new Vector3(
            transform.localEulerAngles.x,
            transform.localEulerAngles.y,
            zRotation);
    }
}
