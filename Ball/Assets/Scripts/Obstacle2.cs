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

        // position�� ������ �� �ϳ��� ���� ���δ� ���� �Ұ� -> new Vector3 �̿�
        // transform.position.x = newXPositon; -> error
        // ������� x��ǥ�� newXPosition ������ ����
        // Vector3�� �μ����� ������ float������ �ڵ� ����ȯ�� �Ͼ. double���� ���Ͼ.
        transform.position = new Vector3(0, newYPositon, newZPositon);

        // ������� Ground�� ���� ���� �����ߴ��� Ȯ��
        if (transform.position.z < -24.0f)
        {
            // delta ���� �ٲ� ������ ��ȯ
            delta = 0.02f;
            gamma = -0.0033f;
        }
        // ������� Ground�� ������ ���� �����ߴ��� Ȯ��
        else if (transform.position.z > 24.0f)
        {
            // delta ���� �ٲ� ������ ��ȯ
            delta = -0.02f;
            gamma = 0.0033f;
        }

    }
}
