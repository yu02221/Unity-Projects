using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    // ��� ���͸���
    public Material bgMaterial;
    // ��ũ�� �ӵ�
    public float scrollSpeed = 1.0f;

    private void Update()
    {
        Vector2 direction = Vector2.up;
        // ���͸��� ��ġ�� �̵�
        bgMaterial.mainTextureOffset += direction * scrollSpeed * Time.deltaTime;
    }
}
