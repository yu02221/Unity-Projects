using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;

    private Vector3 offset;

    private void Start()
    {
        offset = transform.position - player.transform.position;
        // Debug.Log(offset);
    }

    // LateUpdate : Update가 끝나고 난 뒤 실행
    // 공이 먼저 움직이고 카메라가 따라가야 하므로 LateUpdate 사용
    private void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }
}
