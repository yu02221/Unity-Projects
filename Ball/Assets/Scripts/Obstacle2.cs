using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle2 : MonoBehaviour
{
    float delta = -0.02f;
    float gamma = 0.0033f;

    private void Update()
    {
        float newZPositon = transform.position.z + delta;
        float newYPositon = transform.position.y + gamma;

        // position을 변경할 때 하나의 값만 따로는 변경 불가 -> new Vector3 이용
        // transform.position.x = newXPositon; -> error
        // 원기둥의 x좌표를 newXPosition 값으로 변경
        // Vector3의 인수에서 정수는 float형으로 자동 형변환이 일어남. double형은 안일어남.
        transform.position = new Vector3(0, newYPositon, newZPositon);

        // 원기둥이 Ground의 왼쪽 끝에 도달했는지 확인
        if (transform.position.z < -24.0f)
        {
            // delta 값을 바꿔 방향을 전환
            delta = 0.02f;
            gamma = -0.0033f;
        }
        // 원기둥이 Ground의 오른쪽 끝에 도달했는지 확인
        else if (transform.position.z > 24.0f)
        {
            // delta 값을 바꿔 방향을 전환
            delta = -0.02f;
            gamma = 0.0033f;
        }

    }
}
