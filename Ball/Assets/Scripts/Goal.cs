using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    // ��ǥ���� ���� �� GameManager�� Success �޼ҵ� ����
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
