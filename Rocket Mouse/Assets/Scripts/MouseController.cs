using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class MouseController : MonoBehaviour
{
    public float jetpackForce;
    public float forwardMovementSpeed;
    public Transform groundCheckTranssform;
    public LayerMask groundCheckLayerMask;
    public ParticleSystem jetpack;
    public Button buttonRestart;
    public Button buttonReturnMenu;
    public AudioClip coinCollectSound;
    public AudioSource jetpackAudio;
    public AudioSource footstepsAudio;
    public AudioSource backgroundMusic;
    public ParallaxScroll parallaxScroll;
    public TMP_Text textCoins;
    public TMP_Text levelUpTxt;
    public TMP_Text feverTxt;
    public TMP_Text LevelTxt;


    private Rigidbody2D rb;
    private bool grounded;
    private Animator animator;
    private bool dead = false;
    private uint coins = 0;
    private int level;
    private float levelUpTime;
    public bool isFever;
    private float volume;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        textCoins.text = "0";
        level = 1;
        LevelTxt.text = "LEVEL " + level;
        levelUpTime = 0;
        isFever = false;
        bool isMute = System.Convert.ToBoolean(PlayerPrefs.GetInt("Mute"));
        if (isMute)
            volume = 0;
        else
            volume = PlayerPrefs.GetFloat("Volume");
        backgroundMusic.volume = volume;

        StartCoroutine(FeverTime());
    }

    private void FixedUpdate()
    {
        bool jetpackActive = Input.GetButton("Fire1");
        if (jetpackActive && !dead)
            rb.AddForce(jetpackForce * Vector2.up);
        
        levelUpTime += Time.fixedDeltaTime;

        if (levelUpTime > 20)
            StartCoroutine(LevelUp());

        if (!dead)
        {
            if (isFever)
                rb.velocity = new Vector2(
                    forwardMovementSpeed + 5.0f, rb.velocity.y);
            else
                rb.velocity = new Vector2(
                    forwardMovementSpeed, rb.velocity.y);
        }

        UpdateGroundedStatus();
        AdjustJetpack(jetpackActive);
        DisplayRestartButton();
        AdjustFootstepsAndJetpackSound(jetpackActive);
        parallaxScroll.offset = transform.position.x;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Coins")
            CollectCoin(collision);
        else
            HitByLaser(collision);
    }

    private void CollectCoin(Collider2D coinCollider)
    {
        coins++;
        textCoins.text = coins.ToString();
        Destroy(coinCollider.gameObject);
        // ����� Ŭ�� ���Ͽ��� ���� ����� ��� (��� ��ġ ���� ����)
        AudioSource.PlayClipAtPoint(coinCollectSound, transform.position, volume);
    }

    private void HitByLaser(Collider2D laserCollider)
    {
        if (!dead)
        {
            // ���� ������Ʈ�� �ƴ϶� �ٸ� ���۳�Ʈ�� ���� ���۳�Ʈ ȣ�� ����
            AudioSource laser = laserCollider.GetComponent<AudioSource>();
            laser.volume = volume;
            laser.Play();
        }

        dead = true;
        animator.SetBool("dead", true);
    }

    private void AdjustJetpack(bool jetpackActive)
    {
        var emission = jetpack.emission;
        emission.enabled = !grounded;
        emission.rateOverTime = jetpackActive ? 300.0f : 75.0f;
    }

    private void UpdateGroundedStatus()
    {
        grounded = Physics2D.OverlapCircle( // ������ ���� �׷� Ư�� Layer�� ��ġ���� �Ǻ�
            groundCheckTranssform.position, // �� �߽�
            0.1f,                           // ������
            groundCheckLayerMask            // ���̾� ����
            );
        animator.SetBool("grounded", grounded);
    }

    private void DisplayRestartButton()
    {
        bool active = buttonRestart.gameObject.activeSelf;
        if (grounded && dead && !active)
        {
            buttonRestart.gameObject.SetActive(true);
            buttonReturnMenu.gameObject.SetActive(true);
        }
    }

    public void OnClickedRestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnClickedReturnMenuButton()
    {
        SceneManager.LoadScene("Menu");
    }

    private void AdjustFootstepsAndJetpackSound(bool jetpackActive)
    {
        footstepsAudio.enabled = !dead && grounded;
        footstepsAudio.volume = volume;
        jetpackAudio.enabled = !dead && !grounded;
        jetpackAudio.volume = jetpackActive ? volume : volume * 0.5f;
    }

    IEnumerator FeverTime()
    {
        while (!dead)
        {
            yield return new WaitForSeconds(60.0f);
            isFever = true;
            feverTxt.gameObject.SetActive(true);
            yield return new WaitForSeconds(10.0f);
            isFever = false;
            feverTxt.gameObject.SetActive(false);
        }
        
    }

    IEnumerator LevelUp()
    {
        level++;
        LevelTxt.text = "LEVEL " + level;
        levelUpTime = 0;
        forwardMovementSpeed++;
        levelUpTxt.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        levelUpTxt.gameObject.SetActive(false);
    }
}
