using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    // delta ���� ũ�⿡ ���� �����̴� �ӵ��� �޶���. ���밪�� ���� ���� õõ�� ������
    float delta = -0.02f;

    // name ������Ʈ�� ��ֹ����� �Ÿ��� �����ִ� �޼ҵ�
    void TestMethod(string name)
    {
        // Vector3.Distance : �� �� ������ �Ÿ��� ��ȯ
        float distance = Vector3.Distance(
            GameObject.Find(name).transform.position,   //�̸��� name�� ������Ʈ�� ��ġ
            transform.position);                        //Obstacle�� ��ġ
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

    // � ��ü�� �ε����� �ڵ� ȣ��
    private void OnCollisionEnter(Collision collision)
    {
        //��ֹ��� ��ġ���� �浹ü�� ��ġ�� �� ���� ����� ƨ�ܳ��� ������ ����
        Vector3 direction = collision.gameObject.transform.position - transform.position;
        direction = direction.normalized * 1000;
        //�浹ü�� Rigidbody�� ������
        Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
        rb.AddForce(direction);
    }
}
