using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float maxSpeed;
    public float jumpPower;
    public float superJumpPower;
    public GameObject attack;
    public GameObject monster;

    private Rigidbody2D rb2d;
    private SpriteRenderer sr;
    private GameManager gm;
    private float moveHorizontal;
    private bool superJump;
    private bool onGround;
    private bool onPlatform;
    private int direction;
    private float cooltime;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        gm = GameObject.Find("GameManager")
                .GetComponent<GameManager>();

        

        superJump = false;
        direction = -1;
        cooltime = 0;
    }
    private void FixedUpdate()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        if (moveHorizontal > 0)
        {
            sr.flipX = true;
            direction = 1;
        }
        else if (moveHorizontal < 0)
        {
            sr.flipX = false;
            direction = -1;
        }

        if (onGround)
        {
            MoveOnGround();
        }

    }
    private void Update()
    {
        if (Input.GetButtonUp("Horizontal"))
        {
            rb2d.velocity = new Vector2(0, rb2d.velocity.y);

        }

        cooltime += Time.deltaTime;

        if (Input.GetButtonDown("Jump") && superJump && !onGround)
        {
            SuperJump();
        }
        if (Input.GetButton("Jump") && onGround)
        {
            if (onPlatform && Input.GetKey(KeyCode.DownArrow))
                DownJump();
            else
                NomalJump();
        }
        
        if (Input.GetButtonDown("Fire1") && cooltime >= 0.5f)
        {
            Attack();
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground")  
            && rb2d.velocity.y <= 0.1f && rb2d.velocity.y >= -0.1f)
        {
            onGround = true;
            superJump = false;
        }
        if (col.gameObject.CompareTag("Platform")
            && rb2d.velocity.y <= 0.1f && rb2d.velocity.y >= -0.1f)
        {
            onPlatform = true;
            onGround = true;
            superJump = false;
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Potal"))
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                gm.ChangeMap();
            }
        }
    }

    private void SuperJump()
    {
        Vector2 superJumping;
        if (Input.GetKey(KeyCode.UpArrow))
        {
            superJumping = new Vector2(0, 1.8f);
        }
        else
        {
            superJumping = new Vector2(direction, 0.4f);
        }
        rb2d.AddForce(superJumping * superJumpPower, ForceMode2D.Impulse);
        superJump = false;
    }

    private void DownJump()
    {
        gameObject.layer = 9;
        rb2d.AddForce(Vector2.down, ForceMode2D.Impulse);
        
        onGround = false;
        onPlatform = false;
        superJump = true;
        StartCoroutine(ReturnLayer());
    }
    IEnumerator ReturnLayer()
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.layer = 6;
    }

    private void NomalJump()
    {
        Vector2 jumping = new Vector2(moveHorizontal * 0.2f, 1);
        rb2d.AddForce(jumping * jumpPower, ForceMode2D.Impulse);
        onGround = false;
        superJump = true;
    }

    private void Attack()
    {
        Vector3 pos;
        Quaternion rot;
        if (direction > 0)
        {
            pos = new Vector3(1.8f, 0.3f, 0);
            rot = new Quaternion(0, 180, 0, 0);
        }
        else
        {
            pos = new Vector3(-1.8f, 0.3f, 0);
            rot = new Quaternion(0, 0, 0, 0);
        }

        Instantiate(attack, transform.position + pos, rot);
        cooltime = 0;
    }

    private void MoveOnGround()
    {
        Vector2 movement = new Vector2(moveHorizontal, 0);
        rb2d.AddForce(movement * speed, ForceMode2D.Impulse);

        if (rb2d.velocity.x > maxSpeed)
            rb2d.velocity = new Vector2(maxSpeed, rb2d.velocity.y);

        else if (rb2d.velocity.x < maxSpeed * (-1))
            rb2d.velocity = new Vector2(maxSpeed * (-1), rb2d.velocity.y);
    }

}
