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
            Debug.Log("할인대상입니다.");
        }
        else
        {
            Debug.Log("정상요금입니다.");
        }
    }
}
