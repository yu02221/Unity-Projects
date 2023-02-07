using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    // 목표지점 도달 시 GameManager의 Success 메소드 실행
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Ball")
        {
            GameManager gm = GameObject.Find("GameManager")
                .GetComponent<GameManager>();
            gm.SuccessGame();
        }
    }
}
