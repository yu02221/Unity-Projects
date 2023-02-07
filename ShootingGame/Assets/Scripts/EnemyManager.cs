using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public int poolSize;
    public List<GameObject> enemyObjectPool;

    // 최소 시간
    public float minTime;
    // 최대 시간
    public float maxTime;

    public float minSpawnPosition;
    public float maxSpawnPosition;

    // 일정 시간
    public float createTime;
    // 적 공장
    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;


    public float startWait;

    private GameManager gm;

    private void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();

        // 적을 랜덤한 시간에 생성 (1 ~ 5초)
        createTime = Random.Range(minTime, maxTime);

        enemyObjectPool = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            int enemyNum = Random.Range(1, 4);
            GameObject enemy = null;
            switch (enemyNum)
            {
                case 1:
                    enemy = Instantiate(enemy1);
                    break;
                case 2:
                    enemy = Instantiate(enemy2);
                    break;
                case 3:
                    enemy = Instantiate(enemy3);
                    break;
                default:
                    enemy = Instantiate(enemy1);
                    break;
            }
            enemyObjectPool.Add(enemy);
            enemy.SetActive(false);
        }
        // 코루틴 시작
        StartCoroutine(SpawnEnemys());
    }

    // 코루틴 함수
    IEnumerator SpawnEnemys()
    {
        // startWait 만큼 기다림
        yield return new WaitForSeconds(startWait);
        // 일정 시간 마다 적 생성
        while (true)
        {
            CreateEnemey();
            // 적을 생성한 후 적 생성시간을 다시 설정
            createTime = Random.Range(minTime, maxTime);
            yield return new WaitForSeconds(createTime);

            if (gm.isGameOver == true)
                break;
        }
    }
    private void CreateEnemey()
    {
        float spawnPoint = Random.Range(minSpawnPosition, maxSpawnPosition);
        if (enemyObjectPool.Count > 0)
        {
            GameObject enemy = enemyObjectPool[0];
            enemy.transform.position = new Vector3(
                spawnPoint, transform.position.y, transform.position.z);
            enemy.SetActive(true);
            enemyObjectPool.Remove(enemy);
        }
        else
        {
            int enemyNum = Random.Range(1, 4);
            GameObject enemy = null; ;
            switch (enemyNum)
            {
                case 1:
                    enemy = Instantiate(enemy1);
                    break;
                case 2:
                    enemy = Instantiate(enemy2);
                    break;
                case 3:
                    enemy = Instantiate(enemy3);
                    break;
                default:
                    enemy = Instantiate(enemy1);
                    break;
            }
            enemy.transform.position = new Vector3(
                spawnPoint, transform.position.y, transform.position.z);
        }
    }
}
