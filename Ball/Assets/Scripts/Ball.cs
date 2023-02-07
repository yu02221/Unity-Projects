using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Ball 클래스 선언
public class Ball : MonoBehaviour
{
    private void Update()
    {
        // GetKeyDown은 키를 누르는 순간에 한 번만 true반환
        bool pushSpace = Input.GetKeyDown(KeyCode.Space);

        if (pushSpace)
        {
            Rigidbody rigid = GetComponent<Rigidbody>();
            //AddForce : 특정 방향으로 힘을 가하는 메소드
            // 방향과 힘의 세기가 필요 -> 벡터 사용
            rigid.AddForce(Vector3.up * 300);
            //rigid.AddForce(new Vector3(0, 1, 0) * 300); 과 똑같은 코드
        }
    }
}
