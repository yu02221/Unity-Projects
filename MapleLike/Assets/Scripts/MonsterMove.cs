using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMove : MonoBehaviour
{
    public float jumpPower;

    private Rigidbody2D rb2d;
    private SpriteRenderer sr;
    private float timeCount;
    private float jumpTime;
    private float jumpDir;

    private void Start()
    {
        timeCount = 0;
        jumpTime = Random.Range(1.0f, 3.0f);
        jumpDir = Random.Range(-0.5f, 0.5f);
        rb2d = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }
    private void FixedUpdate()
    {
        timeCount += Time.deltaTime;

        if (timeCount >= jumpTime)
        {
            jumpTime = Random.Range(1.0f, 3.0f);
            jumpDir = Random.Range(-0.5f, 0.5f);

            if (jumpDir < 0)
                sr.flipX = true;
            else
                sr.flipX = false;

            Vector2 jumping = new Vector2(jumpDir, 1);
            rb2d.AddForce(jumping * jumpPower, ForceMode2D.Impulse);
            timeCount = 0;
        }
    }
}
