using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fusion;

public class PlayerMove : NetworkBehaviour
{
    public float moveSpeed = 7f;
    NetworkCharacterControllerPrototype netCC;
    public float jumpPower = 10f;
    public bool isJumping = false;
    [Networked] private NetworkButtons _buttonsPrevious { get; set; }

    public int hp = 20;
    int maxHp = 20;
    public Slider hpSlider;

    public GameObject hitEffect;

    Animator anim;

    public Transform camPosition;

    public override void Spawned()
    {
        netCC = GetComponent<NetworkCharacterControllerPrototype>();
        anim = GetComponentInChildren<Animator>();

        if (Object.HasInputAuthority)
        {
            GameManager.gm.player = this;

            CamFollow cf = Camera.main.GetComponent<CamFollow>();
            cf.target = camPosition;

            hpSlider = GameManager.gm.hpSlider;
        }
        hitEffect = GameManager.gm.hitEffect;
    }

    public override void FixedUpdateNetwork()
    {
        if (GameManager.gm.gState != GameManager.GameState.Run)
            return;

        if (GetInput(out NetworkInputData data))
        {
            netCC.Move(data.dir * moveSpeed * Runner.DeltaTime);
            if (data.Buttons.WasPressed(_buttonsPrevious, PlayerButtons.Jump))
                netCC.Jump();

            _buttonsPrevious = data.Buttons;
        }

        anim.SetFloat("moveMotion", netCC.Velocity.magnitude);

        if (hpSlider != null)
            hpSlider.value = (float)hp / maxHp;
    }

    public void DamageAction(int damage)
    {
        hp -= damage;

        if (Object.HasStateAuthority && hp > 0)
        {
            StartCoroutine(PlayHitEffect());
        }
    }

    IEnumerator PlayHitEffect()
    {
        hitEffect.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        hitEffect.SetActive(false);
    }
}
