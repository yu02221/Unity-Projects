using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float tilt;
    GameManager gm;

    Vector3 dir;
    // ���� ȿ�� ����Ʈ
    public GameObject explosionFactory;
    public GameObject hitEffect;

    private void OnEnable()
    {
        gm = GameObject.Find("GameManager")
                .GetComponent<GameManager>();

        // 0 ~ 9 ���� ���� �߿� �ϳ��� �������� ����
        int randValue = Random.Range(0, 10);
        // ���� ���� 3���� ������(30%) �÷��̾� �������� �̵�
        if (randValue < 3)
        {
            // �÷��̾� ������Ʈ�� ã�Ƽ� Ÿ�ٿ� ����
            GameObject target = GameObject.Find("Player");
            // ���� ���͸� ��� (target - enemy)
            dir = target.transform.position - transform.position;
            // ������ ũ�⸦ 1�� ����ȭ
            dir.Normalize();
        }
        // �׷��� ������(70%) �Ʒ� �������� �̵�
        else
        {
            dir = Vector3.down;
        }

        transform.rotation = Quaternion.Euler(0.0f, dir.x * -tilt, 0.0f);
    }
    private void Update()
    {
        // �� �����Ӹ��� dir �������� speed�� �ӵ��� �̵�
        transform.position += dir * speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // ���� ȿ�� ����
        GameObject explosion = Instantiate(explosionFactory);
        // ���� ȿ�� ��ġ ����
        explosion.transform.position = transform.position;

        if (collision.gameObject.CompareTag("Bullet"))
        {
            ScoreManager.Instance.SetScore(ScoreManager.Instance.GetScore() + 1);

            collision.gameObject.SetActive(false);
            if (gm.isGameOver == false)
            {
                GameObject playerObj = GameObject.Find("Player");
                if (playerObj == null)
                {
                    Destroy(collision.gameObject);
                }
                PlayerFire playerFire = playerObj.GetComponent<PlayerFire>();
                // ����� ���� ������Ʈ�� Ǯ�� ��������
                if (collision.gameObject.name.Contains("Big"))
                {
                    playerFire.bigBulletObjPool.Add(collision.gameObject);
                }
                else
                {
                    playerFire.bulletObjectPool.Add(collision.gameObject);
                }
            }
        }
        else if (collision.gameObject.name.Contains("Player"))
        {
            GameObject hit = Instantiate(hitEffect);
            hit.transform.position = transform.position;

            gm.lifeCount--;
            gm.SetLifeCount();
            if (gm.lifeCount <= 0)
            {
                gm.GameOver();
                Destroy(collision.gameObject);
            }
                
        }
        gameObject.SetActive(false);
        EnemyManager enemyManager = GameObject.Find("EnemyManager")
                .GetComponent<EnemyManager>();
        enemyManager.enemyObjectPool.Add(gameObject);
    }
}
