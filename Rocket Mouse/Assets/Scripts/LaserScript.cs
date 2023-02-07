using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserScript : MonoBehaviour
{
    public Sprite laserOnSprite;
    public Sprite laserOffSprite;
    public float interval = 0.5f;
    public float rotationSpeed;

    private bool isLaserOn = true;
    private float timeUntilNextToggle;
    SpriteRenderer spriteRenderer;
    private Collider2D collider2d;

    private void Start()
    {
        timeUntilNextToggle = interval;
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider2d = GetComponent<Collider2D>();
    }

    private void FixedUpdate()
    {
        timeUntilNextToggle -= Time.fixedDeltaTime;
        if (timeUntilNextToggle <= 0)
        {
            isLaserOn = !isLaserOn;
            collider2d.enabled = isLaserOn;

            spriteRenderer.sprite = isLaserOn ? laserOnSprite : laserOffSprite;

            timeUntilNextToggle = interval;
        }

        transform.RotateAround(
            transform.position, // Áß½ÉÁ¡
            Vector3.forward,
            rotationSpeed * Time.fixedDeltaTime);
    }
}
