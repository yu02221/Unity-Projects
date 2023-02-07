using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PlayerState
{
    Idle,
    WalkForward,
    WalkLeft,
    WalkRight,
    Run,
    Attack,
    Dead,
}

public class PlayerController : MonoBehaviour
{
    public PlayerState playerState;
    public Vector3 lookDirection;
    public float speed;
    public float walkSpeed;
    public float runSpeed;
    public float rotSpeed;

    private Animation anim;
    public AnimationClip idleAni;
    public AnimationClip walkForwardAni;
    public AnimationClip walkLeftAni;
    public AnimationClip walkRightAni;
    public AnimationClip runAni;

    private AudioSource audioSrc;
    public GameObject bullet;
    public Transform shotPoint;
    public GameObject shotFx;
    public AudioClip shotSound;

    public Slider lifeBar;
    public float maxHp;
    public float hp;

    private void Start()
    {
        anim = GetComponent<Animation>();
        audioSrc = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (playerState != PlayerState.Dead)
        {
            KeyboardInput();
            LookUpdate();
        }

        AnimationUpdate();
    }

    public IEnumerator Shot()
    {
        GameObject bulletObj = Instantiate(
            bullet,
            shotPoint.position,
            Quaternion.LookRotation(shotPoint.forward));

        Physics.IgnoreCollision(
            bulletObj.GetComponent<Collider>(),
            GetComponent<Collider>());

        audioSrc.clip = shotSound;
        audioSrc.Play();

        shotFx.SetActive(true);

        playerState = PlayerState.Attack;
        speed = 0.0f;

        yield return new WaitForSeconds(0.15f);
        shotFx.SetActive(false);

        yield return new WaitForSeconds(0.15f);
        playerState = PlayerState.Idle;
    }

    void AnimationUpdate()
    {
        switch (playerState)
        {
            case PlayerState.Idle:
                anim.CrossFade(idleAni.name, 0.2f);
                break;
            case PlayerState.WalkForward:
                anim.CrossFade(walkForwardAni.name, 0.2f);
                break;
            case PlayerState.WalkLeft:
                anim.CrossFade(walkLeftAni.name, 0.2f);
                break;
            case PlayerState.WalkRight:
                anim.CrossFade(walkRightAni.name, 0.2f);
                break;
            case PlayerState.Run:
                anim.CrossFade(runAni.name, 0.2f);
                break;
            case PlayerState.Attack:
                anim.CrossFade(idleAni.name, 0.2f);
                break;
            case PlayerState.Dead:
                anim.CrossFade(idleAni.name, 0.2f);
                break;
        }
    }

    void KeyboardInput()
    {
        float xx = Input.GetAxisRaw("Vertical");
        float zz = Input.GetAxisRaw("Horizontal");


        if (playerState != PlayerState.Attack)
        {
            if (xx != 0 || zz != 0)
            {
                lookDirection = (xx * Vector3.forward) + (zz * Vector3.right);
                speed = walkSpeed;
                anim[runAni.name].speed = 1.0f;
                playerState = PlayerState.WalkForward;

                if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                {
                    speed = runSpeed;
                    playerState = PlayerState.Run;
                    anim[runAni.name].speed = runSpeed / walkSpeed;
                }
            }
            else if (Input.GetKey(KeyCode.Q))
            {
                speed = walkSpeed;
                playerState = PlayerState.WalkLeft;
            }
            else if (Input.GetKey(KeyCode.E))
            {
                speed = walkSpeed;
                playerState = PlayerState.WalkRight;
            }
            else if (playerState != PlayerState.Idle)
            {
                playerState = PlayerState.Idle;
                speed = 0.0f;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && playerState != PlayerState.Dead)
            StartCoroutine("Shot");
    }

    void LookUpdate()
    {
        Vector3 dir;

        if (playerState == PlayerState.WalkLeft)
            dir = -Vector3.right;
        else if (playerState == PlayerState.WalkRight)
            dir = Vector3.right;
        else
        {
            dir = Vector3.forward;
            Quaternion r = Quaternion.LookRotation(lookDirection);
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation, r, rotSpeed * Time.deltaTime);
        }

        transform.Translate(dir * speed * Time.deltaTime);
    }

    public void Hurt(float damage)
    {
        if (hp > 0)
        {
            hp -= damage;
            lifeBar.value = hp / maxHp;
        }

        if (hp <= 0)
        {
            speed = 0;
            playerState = PlayerState.Dead;

            PlayManager pm = GameObject.Find("PlayManager").GetComponent<PlayManager>();
            pm.GameOver();
        }
    }
}
