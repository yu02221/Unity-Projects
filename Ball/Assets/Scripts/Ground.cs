using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    private void Update()
    {
        //transform.rotation.z�� �ϸ� ���װ� �� �� -> localEulerAngles ���
        float zRotation = transform.localEulerAngles.z;
        // ����ȭ��ǥ/a ������ -1, ������ȭ��ǥ/d ������ +1
        zRotation = zRotation - (Input.GetAxis("Horizontal")*0.1f);
        //Debug.Log(Input.GetAxis("Horizontal"));


        transform.localEulerAngles = new Vector3(
            transform.localEulerAngles.x, 
            transform.localEulerAngles.y, 
            zRotation);

        if(Input.touchCount > 0 || Input.GetMouseButton(0)){
            Debug.Log("mouse down : " + Input.mousePosition);
            if ( Input.mousePosition.x < Screen.width / 2)
            {   // ȭ�� ���� Ŭ��
                transform.localEulerAngles = new Vector3(
                    10, 0, transform.localEulerAngles.z + 0.1f);
            }
            else
            {   // ȭ�� ������ Ŭ��
                transform.localEulerAngles = new Vector3(
                    10, 0, transform.localEulerAngles.z - 0.1f);
            } 
        }
    }
}
