using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScroll : MonoBehaviour
{
    public Renderer background; // Mesh Renderer ���۳�Ʈ�� ��
    public Renderer foreground;
    public float backgroundSpeed = 0.01f;
    public float foregroundSpeed = 0.03f;
    public float offset = 0.0f;

    private void Update()
    {   // timeSinceLevelLoad : ���� �ε�� �� �帥 �ð� (��� ����)
        float backgroundOffset = offset * backgroundSpeed;
        float foregroundOffset = offset * foregroundSpeed;

        background.material.mainTextureOffset = new Vector2(backgroundOffset, 0);
        foreground.material.mainTextureOffset = new Vector2(foregroundOffset, 0);
    }
}
