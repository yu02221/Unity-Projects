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
            // 자기 자신은 맨 마지막에 없애는게 안전함
        }
    }
    private void Update()
    {
        // 코인이 계속 회전하도록 변경
        float zRotation = transform.localEulerAngles.z;
        zRotation = zRotation - 1.0f;
        transform.localEulerAngles = new Vector3(
            transform.localEulerAngles.x,
            transform.localEulerAngles.y,
            zRotation);
    }
}
