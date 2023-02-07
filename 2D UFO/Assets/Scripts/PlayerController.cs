using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public TextMeshProUGUI countText;
    public TextMeshProUGUI winText;
    public TextMeshProUGUI scoreText;

    private Rigidbody2D rb2d;
    private int count;
    private int score;
    private bool gameOvered;

    private void Start()
    {
        gameOvered = false;
        rb2d = GetComponent<Rigidbody2D>();
        count = 0;
        score = 0;
        winText.text = "";

        SetText();
    }

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        rb2d.AddForce(movement * speed);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PickUp"))
        {
            count++;
            score += 10;
            SetText();
            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("Bullet"))
        {
            score -= 10;
            SetText();
            Destroy(collision.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            score -= 5;
            SetText();
        }
    }

    private void SetText()
    {

        countText.text = "Count : " + count;
        scoreText.text = "Score : " + score;
        if (count >= 8 && !gameOvered)
        {
            winText.text = "You Win!\nScore : " + score;
            gameOvered = true;
        }
        else if (score < 0 && !gameOvered)
        {
            winText.text = "Game Over!";
            gameOvered = true;
        }
    }
}
