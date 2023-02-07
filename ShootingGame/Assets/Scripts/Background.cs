using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    // 배경 매터리얼
    public Material bgMaterial;
    // 스크롤 속도
    public float scrollSpeed = 1.0f;

    private void Update()
    {
        Vector2 direction = Vector2.up;
        // 매터리얼 위치를 이동
        bgMaterial.mainTextureOffset += direction * scrollSpeed * Time.deltaTime;
    }
}
