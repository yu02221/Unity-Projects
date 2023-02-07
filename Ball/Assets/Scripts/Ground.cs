using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    private void Update()
    {
        //transform.rotation.z로 하면 버그가 잘 남 -> localEulerAngles 사용
        float zRotation = transform.localEulerAngles.z;
        // 왼쪽화살표/a 누르면 -1, 오른쪽화살표/d 누르면 +1
        zRotation = zRotation - (Input.GetAxis("Horizontal")*0.1f);
        //Debug.Log(Input.GetAxis("Horizontal"));


        transform.localEulerAngles = new Vector3(
            transform.localEulerAngles.x, 
            transform.localEulerAngles.y, 
            zRotation);

        if(Input.touchCount > 0 || Input.GetMouseButton(0)){
            Debug.Log("mouse down : " + Input.mousePosition);
            if ( Input.mousePosition.x < Screen.width / 2)
            {   // 화면 왼쪽 클릭
                transform.localEulerAngles = new Vector3(
                    10, 0, transform.localEulerAngles.z + 0.1f);
            }
            else
            {   // 화면 오른쪽 클릭
                transform.localEulerAngles = new Vector3(
                    10, 0, transform.localEulerAngles.z - 0.1f);
            } 
        }
    }
}
