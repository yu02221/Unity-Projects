using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float tilt;
    GameManager gm;

    Vector3 dir;
    // 폭발 효과 이펙트
    public GameObject explosionFactory;
    public GameObject hitEffect;

    private void OnEnable()
    {
        gm = GameObject.Find("GameManager")
                .GetComponent<GameManager>();

        // 0 ~ 9 까지 정수 중에 하나를 랜덤으로 구함
        int randValue = Random.Range(0, 10);
        // 랜덤 값이 3보다 작으면(30%) 플레이어 방향으로 이동
        if (randValue < 3)
        {
            // 플레이어 오브젝트를 찾아서 타겟에 저장
            GameObject target = GameObject.Find("Player");
            // 방향 벡터를 계산 (target - enemy)
            dir = target.transform.position - transform.position;
            // 벡터의 크기를 1로 정규화
            dir.Normalize();
        }
        // 그렇지 않으면(70%) 아래 방향으로 이동
        else
        {
            dir = Vector3.down;
        }

        transform.rotation = Quaternion.Euler(0.0f, dir.x * -tilt, 0.0f);
    }
    private void Update()
    {
        // 매 프레임마다 dir 방향으로 speed의 속도로 이동
        transform.position += dir * speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 폭발 효과 생성
        GameObject explosion = Instantiate(explosionFactory);
        // 폭발 효과 위치 변경
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
                // 사용이 끝난 오브젝트는 풀에 돌려놓기
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
