using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject stoneObj;

    float timeCount;
    float interval;

    private void Start()
    {
        timeCount = 0;
        interval = Random.Range(2.0f, 4.0f);
    }

    private void Update()
    {
        // ���� ���� �� ���ݱ��� �帥 �ð�
        timeCount += Time.deltaTime;    // Time.deltaTime : ���� update�� ȣ��� �� ������ �ð�

        if (timeCount > interval)
        {
            // Instantiate : ������Ʈ�� �����ϴ� �޼ҵ� (������ ������, ������ ��ġ, ������ ȸ����)
            Instantiate(stoneObj, transform.position, Quaternion.identity); // Quaternion.identity : (0, 0, 0)�� ȸ����
            timeCount = 0;
            interval = Random.Range(2.0f, 4.0f);
        }
    }
}
