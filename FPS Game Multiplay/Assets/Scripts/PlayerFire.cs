using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fusion;

public class PlayerFire : NetworkBehaviour
{
    enum WeaponMode
    {
        Normal,
        Sniper
    }
    WeaponMode wMode;
    bool zoomMode = false;
    public Text wModeText;

    public GameObject firePosition;
    public GameObject bombFactory;
    public float throwPower = 15f;
    public GameObject bulletEffect;
    ParticleSystem ps;
    public int weaponPower = 5;

    Animator anim;

    public GameObject[] eff_Flash;

    public GameObject weapon01;
    public GameObject weapon02;
    public GameObject crosshair01;
    public GameObject crosshair02;
    public GameObject weapon01_R;
    public GameObject weapon02_R;
    public GameObject crosshair02_zoom;

    [Networked] private NetworkButtons _buttonsPrevious { get; set; }

    [Networked(OnChanged = nameof(OnFireBullet))]
    public NetworkBool fireToggle { get; set; }
    [Networked] public Vector3 bulletEffectPosition { get; set; }
    [Networked] public Vector3 bulletEffectNormal { get; set; }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            wMode = WeaponMode.Normal;

            Camera.main.fieldOfView = 60f;

            wModeText.text = "Normal Mode";

            weapon01.SetActive(true);
            weapon02.SetActive(false);
            crosshair01.SetActive(true);
            crosshair02.SetActive(false);
            weapon01_R.SetActive(true);
            weapon02_R.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            wMode = WeaponMode.Sniper;

            wModeText.text = "Sniper Mode";

            weapon01.SetActive(false);
            weapon02.SetActive(true);
            crosshair01.SetActive(false);
            crosshair02.SetActive(true);
            weapon01_R.SetActive(false);
            weapon02_R.SetActive(true);
        }
    }

    public override void Spawned()
    {
        wModeText = GameManager.gm.wModeText;
        weapon01 = GameManager.gm.weapon01;
        weapon02 = GameManager.gm.weapon02;
        crosshair01 = GameManager.gm.crosshair01;
        crosshair02 = GameManager.gm.crosshair02;
        weapon01_R = GameManager.gm.weapon01_R;
        weapon02_R = GameManager.gm.weapon02_R;
        crosshair02_zoom = GameManager.gm.crosshair02_zoom;

        ps = bulletEffect.GetComponent<ParticleSystem>();
        anim = GetComponentInChildren<Animator>();

        wMode = WeaponMode.Normal;
    }

    public override void FixedUpdateNetwork()
    {
        if (GameManager.gm.gState != GameManager.GameState.Run)
            return;

        if (GetInput(out NetworkInputData data))
        {
            if (data.Buttons.WasPressed(_buttonsPrevious, PlayerButtons.Fire1))
            {
                switch (wMode)
                {
                    case WeaponMode.Normal:
                        var bomb = Runner.Spawn(
                            bombFactory,
                            firePosition.transform.position,
                            Quaternion.identity);

                        Rigidbody rb = bomb.GetComponent<Rigidbody>();

                        rb.AddForce(Camera.main.transform.forward * throwPower, ForceMode.Impulse);
                        break;
                    case WeaponMode.Sniper:
                        if (!zoomMode)
                        {
                            Camera.main.fieldOfView = 15f;
                            zoomMode = true;
                            crosshair02_zoom.SetActive(true);
                            crosshair02.SetActive(false);
                        }
                        else
                        {
                            Camera.main.fieldOfView = 60f;
                            zoomMode = false;
                            crosshair02_zoom.SetActive(false);
                            crosshair02.SetActive(true);
                        }
                        break;
                    default:
                        break;
                }
            }

            if (data.Buttons.WasPressed(_buttonsPrevious, PlayerButtons.Fire0))
            {
                if (anim.GetFloat("moveMotion") == 0)
                {
                    anim.SetTrigger("attack");
                }

                Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
                RaycastHit hitInfo;
                if (Physics.Raycast(ray, out hitInfo))
                {
                    if (hitInfo.transform.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                    {
                        EnemyFSM eFSM = hitInfo.transform.GetComponent<EnemyFSM>();
                        eFSM.HitEnemy(weaponPower);
                    }
                    else
                    {
                        bulletEffectPosition = hitInfo.point;
                        bulletEffectNormal = hitInfo.normal;
                        fireToggle = !fireToggle;
                    }
                }

                StartCoroutine(ShootEffectOn(0.05f));
            }
            _buttonsPrevious = data.Buttons;
        }
    }

    void DisplayBulletEffect()
    {
        bulletEffect.transform.position = bulletEffectPosition;
        bulletEffect.transform.forward = bulletEffectNormal;
        ps.Play();
    }

    private static void OnFireBullet(Changed<PlayerFire> playerFire)
    {
        playerFire.Behaviour.DisplayBulletEffect();
    }

    IEnumerator ShootEffectOn(float duration)
    {
        int num = Random.Range(0, eff_Flash.Length);
        eff_Flash[num].SetActive(true);
        yield return new WaitForSeconds(duration);
        eff_Flash[num].SetActive(false);
    }
}
