using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Ball Ŭ���� ����
public class Ball : MonoBehaviour
{
    private void Update()
    {
        // GetKeyDown�� Ű�� ������ ������ �� ���� true��ȯ
        bool pushSpace = Input.GetKeyDown(KeyCode.Space);

        if (pushSpace)
        {
            Rigidbody rigid = GetComponent<Rigidbody>();
            //AddForce : Ư�� �������� ���� ���ϴ� �޼ҵ�
            // ����� ���� ���Ⱑ �ʿ� -> ���� ���
            rigid.AddForce(Vector3.up * 300);
            //rigid.AddForce(new Vector3(0, 1, 0) * 300); �� �Ȱ��� �ڵ�
        }
    }
}
