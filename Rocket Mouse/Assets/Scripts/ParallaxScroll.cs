using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScroll : MonoBehaviour
{
    public Renderer background; // Mesh Renderer 컴퍼넌트가 들어감
    public Renderer foreground;
    public float backgroundSpeed = 0.01f;
    public float foregroundSpeed = 0.03f;
    public float offset = 0.0f;

    private void Update()
    {   // timeSinceLevelLoad : 씬이 로드된 후 흐른 시간 (계속 증가)
        float backgroundOffset = offset * backgroundSpeed;
        float foregroundOffset = offset * foregroundSpeed;

        background.material.mainTextureOffset = new Vector2(backgroundOffset, 0);
        foreground.material.mainTextureOffset = new Vector2(foregroundOffset, 0);
    }
}
