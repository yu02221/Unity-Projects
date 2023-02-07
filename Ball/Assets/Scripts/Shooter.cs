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
        // 게임 시작 후 지금까지 흐른 시간
        timeCount += Time.deltaTime;    // Time.deltaTime : 다음 update가 호출될 때 까지의 시간

        if (timeCount > interval)
        {
            // Instantiate : 오브젝트를 생성하는 메소드 (생성할 프리팹, 생성할 위치, 생성시 회전값)
            Instantiate(stoneObj, transform.position, Quaternion.identity); // Quaternion.identity : (0, 0, 0)인 회전값
            timeCount = 0;
            interval = Random.Range(2.0f, 4.0f);
        }
    }
}
