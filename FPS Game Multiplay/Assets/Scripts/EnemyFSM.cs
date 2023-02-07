using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using Fusion;
using System.Threading.Tasks;

public class EnemyFSM : NetworkBehaviour
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
    GameObject nearPlayer = null;
    public float attackDistance = 2f;
    public float moveSpeed = 5f;
    CharacterController cc;
    float currentTime = 0;
    float attackDelay = 2f;
    public int attackPower = 3;
    Vector3 originPos;
    Quaternion originRot;
    public float moveDistance = 20f;

    [Networked] public int hp { get; set; } = 15;
    int maxHp = 15;
    public Slider hpSlider;

    Animator anim;

    NavMeshAgent smith;

    public override void Spawned()
    {
        m_State = EnemyState.Idle;

        cc = GetComponent<CharacterController>();

        originPos = transform.position;
        originRot = transform.rotation;
        
        anim = GetComponentInChildren<Animator>();

        smith = GetComponent<NavMeshAgent>();
    }

    public override void FixedUpdateNetwork()
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
        foreach (var player in GameManager.gm.players)
        {
            if (Vector3.Distance(transform.position, player.transform.position) < findDistance)
            {
                m_State = EnemyState.Move;
                print("상태 전환: Idle -> Move");

                anim.SetTrigger("idleToMove");
            }
        }
    }

    void Move()
    {
        if (Vector3.Distance(transform.position, originPos) > moveDistance)
        {
            m_State = EnemyState.Return;
            print("상태 전환: Move -> Return");

            return;
        }

        float minDistance = float.MaxValue;
        foreach (var player in GameManager.gm.players)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);
            if (minDistance > distance)
            {
                minDistance = distance;
                nearPlayer = player;
            }
        }

        if (minDistance > attackDistance)
        {
            smith.stoppingDistance = attackDistance;
            smith.destination = nearPlayer.transform.position;
        }
        else
        {
            m_State = EnemyState.Attack;
            print("상태 전환: Move -> Attack");

            currentTime = attackDelay;

            anim.SetTrigger("moveToAttackDelay");

            smith.isStopped = true;
            smith.ResetPath();
        }
    }

    void Attack()
    {
        if (Vector3.Distance(transform.position, nearPlayer.transform.position) < attackDistance)
        {
            currentTime += Runner.DeltaTime;
            if (currentTime > attackDelay)
            {
                //nearPlayer.GetComponent<PlayerMove>().DamageAction(attackPower);
                print("공격");
                currentTime = 0;

                anim.SetTrigger("startAttack");
            }
        }
        else
        {
            m_State = EnemyState.Move;
            print("상태 전환: Attack -> Move");
            currentTime = 0;

            anim.SetTrigger("attackToMove");
        }
    }

    public void AttackAction()
    {
        nearPlayer.GetComponent<PlayerMove>().DamageAction(attackPower);
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
            print("상태 전환: Return -> Idle");

            anim.SetTrigger("moveToIdle");
        }
    }

    void Damaged()
    {
        StartCoroutine(DamageProcess());
    }

    IEnumerator DamageProcess()
    {
        yield return new WaitForSeconds(1.0f);

        m_State = EnemyState.Move;
        print("상태 전환: Damaged -> Move");
    }

    void Die()
    {
        StopAllCoroutines();

        StartCoroutine(DieProcess());
    }

    IEnumerator DieProcess()
    {
        cc.enabled = false;

        yield return new WaitForSeconds(2f);
        print("소멸!");
        Destroy(gameObject);
    }

    public async void HitEnemy(int hitPower)
    {
        if (m_State == EnemyState.Damaged || m_State == EnemyState.Die || m_State == EnemyState.Return)
            return;

        if (!Object.HasStateAuthority)
        {
            Object.RequestStateAuthority();
            while (Object.HasStateAuthority == false)
            {
                await Task.Delay(100);
            }
        }

        hp -= hitPower;

        smith.isStopped = true;
        smith.ResetPath();

        if (hp > 0)
        {
            m_State = EnemyState.Damaged;
            print("상태 전환: Any state -> Damaged");

            anim.SetTrigger("damaged");
            Damaged();
        }
        else
        {
            m_State = EnemyState.Die;
            print("상태 전환: Any state -> Die");

            anim.SetTrigger("die");
            Die();
        }
    }
}
