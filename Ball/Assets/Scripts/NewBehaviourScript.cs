using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private void Start()
    {
        int age = 30;
        if (age < 20 || age >= 65)
        {
            Debug.Log("���δ���Դϴ�.");
        }
        else
        {
            Debug.Log("�������Դϴ�.");
        }
    }
}
