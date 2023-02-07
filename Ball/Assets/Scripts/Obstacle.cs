using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    // delta 값의 크기에 따라 움직이는 속도가 달라짐. 절대값이 작을 수록 천천히 움직임
    float delta = -0.02f;

    // name 오브젝트와 장애물과의 거리를 구해주는 메소드
    void TestMethod(string name)
    {
        // Vector3.Distance : 두 점 사이의 거리를 반환
        float distance = Vector3.Distance(
            GameObject.Find(name).transform.position,   //이름이 name인 오브젝트의 위치
            transform.position);                        //Obstacle의 위치
    }

    private void Update()
    {
        float newXPositon = transform.localPosition.x + delta;

        transform.localPosition = new Vector3(
            newXPositon, 
            transform.localPosition.y, 
            transform.localPosition.z);
        
        if(transform.localPosition.x < -3.5f)
        {
            delta = 0.02f;
        }
        else if (transform.localPosition.x > 3.5f)
        {
            delta = -0.02f;
        }
        
    }

    // 어떤 물체가 부딛히면 자동 호출
    private void OnCollisionEnter(Collision collision)
    {
        //장애물의 위치에서 충돌체의 위치를 뺀 값을 계산해 튕겨나갈 방향을 정함
        Vector3 direction = collision.gameObject.transform.position - transform.position;
        direction = direction.normalized * 1000;
        //충돌체의 Rigidbody를 가져옴
        Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
        rb.AddForce(direction);
    }
}
