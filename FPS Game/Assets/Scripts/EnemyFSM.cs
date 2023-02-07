using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class EnemyFSM : MonoBehaviour
{
    enum EnemyState
    {
        Idle,
        Move,
        Attack,
        Return,
        Damaged,
        Die
    }

    EnemyState m_State;

    public float findDistance = 8f;
    Transform player;
    public float attackDistance = 2f;
    public float moveSpeed = 5f;
    CharacterController cc;
    float currentTime = 0;
    float attackDelay = 2f;
    public int attackPower = 3;
    Vector3 originPos;
    Quaternion originRot;
    public float moveDistance = 20f;

    public int hp = 15;
    int maxHp = 15;
    public Slider hpSlider;

    Animator anim;

    NavMeshAgent smith;

    GameManager gm;

    public AudioSource audioSrc;
    public AudioClip hitSound;
    public AudioClip dieSound;


    private void Start()
    {
        m_State = EnemyState.Idle;
        player = GameObject.Find("Player").transform;

        gm = GameObject.Find("GameManager").GetComponent<GameManager>();

        cc = GetComponent<CharacterController>();

        originPos = transform.position;
        originRot = transform.rotation;
        
        anim = GetComponentInChildren<Animator>();

        smith = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        switch (m_State)
        {
            case EnemyState.Idle:
                Idle();
                break;
            case EnemyState.Move:
                Move();
                break;
            case EnemyState.Attack:
                Attack();
                break;
            case EnemyState.Return:
                Return();
                break;
            case EnemyState.Damaged:
                //Damaged();
                break;
            case EnemyState.Die:
                //Die();
                break;
        }
        hpSlider.value = (float)hp / maxHp;
    }

    void Idle()
    {
        if (Vector3.Distance(transform.position, player.position) < findDistance)
        {
            m_State = EnemyState.Move;
            print("���� ��ȯ: Idle -> Move");

            anim.SetTrigger("idleToMove");
        }
    }

    void Move()
    {
        if (Vector3.Distance(transform.position, originPos) > moveDistance)
        {
            m_State = EnemyState.Return;
            print("���� ��ȯ: Move -> Return");
        }
        else if (Vector3.Distance(transform.position, player.position) > attackDistance)
        {
            /*
            Vector3 dir = (player.position - transform.position).normalized;
            cc.Move(dir * moveSpeed * Time.deltaTime);
            transform.forward = dir;
            */

            smith.stoppingDistance = attackDistance;
            smith.destination = player.position;
        }
        else
        {
            m_State = EnemyState.Attack;
            print("���� ��ȯ: Move -> Attack");

            currentTime = attackDelay;

            anim.SetTrigger("moveToAttackDelay");

            smith.isStopped = true;
            smith.ResetPath();
        }
    }

    void Attack()
    {
        if (Vector3.Distance(transform.position, player.position) < attackDistance)
        {
            currentTime += Time.deltaTime;
            if (currentTime > attackDelay)
            {
                //player.GetComponent<PlayerMove>().DamageAction(attackPower);
                print("����");
                currentTime = 0;

                anim.SetTrigger("startAttack");
            }
        }
        else
        {
            m_State = EnemyState.Move;
            print("���� ��ȯ: Attack -> Move");
            currentTime = 0;

            anim.SetTrigger("attackToMove");
        }
    }

    public void AttackAction()
    {
        player.GetComponent<PlayerMove>().DamageAction(attackPower);
    }

    void Return()
    {
        if (Vector3.Distance(transform.position, originPos) > 0.1f)
        {
            /*
            Vector3 dir = (originPos - transform.position).normalized;
            cc.Move(dir * moveSpeed * Time.deltaTime);
            transform.forward = dir;
            */
            smith.destination = originPos;
            smith.stoppingDistance = 0;
        }
        else
        {
            smith.isStopped = true;
            smith.ResetPath();

            transform.position = originPos;
            transform.rotation = originRot;

            hp = maxHp;
            m_State = EnemyState.Idle;
            print("���� ��ȯ: Return -> Idle");

            anim.SetTrigger("moveToIdle");
        }
    }

    void Damaged()
    {
        audioSrc.clip = hitSound;
        audioSrc.Play();
        StartCoroutine(DamageProcess());
    }

    IEnumerator DamageProcess()
    {
        yield return new WaitForSeconds(1.0f);

        m_State = EnemyState.Move;
        print("���� ��ȯ: Damaged -> Move");
    }

    void Die()
    {
        audioSrc.clip = dieSound;
        audioSrc.Play();

        StopAllCoroutines();

        StartCoroutine(DieProcess());
    }

    IEnumerator DieProcess()
    {
        cc.enabled = false;
        gm.killCount++;
        gm.killCountText.text = gm.killCount + " Kill";
        yield return new WaitForSeconds(2f);
        print("�Ҹ�!");
        Destroy(gameObject);
    }

    public void HitEnemy(int hitPower)
    {
        if (m_State == EnemyState.Damaged || m_State == EnemyState.Die || m_State == EnemyState.Return)
            return;

        hp -= hitPower;

        smith.isStopped = true;
        smith.ResetPath();

        if (hp > 0)
        {
            m_State = EnemyState.Damaged;
            print("���� ��ȯ: Any state -> Damaged");

            anim.SetTrigger("damaged");
            Damaged();
        }
        else
        {
            m_State = EnemyState.Die;
            print("���� ��ȯ: Any state -> Die");

            anim.SetTrigger("die");
            Die();
        }
    }
}
