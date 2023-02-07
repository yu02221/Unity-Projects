using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public int poolSize;
    public List<GameObject> enemyObjectPool;

    // �ּ� �ð�
    public float minTime;
    // �ִ� �ð�
    public float maxTime;

    public float minSpawnPosition;
    public float maxSpawnPosition;

    // ���� �ð�
    public float createTime;
    // �� ����
    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;


    public float startWait;

    private GameManager gm;

    private void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();

        // ���� ������ �ð��� ���� (1 ~ 5��)
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
        // �ڷ�ƾ ����
        StartCoroutine(SpawnEnemys());
    }

    // �ڷ�ƾ �Լ�
    IEnumerator SpawnEnemys()
    {
        // startWait ��ŭ ��ٸ�
        yield return new WaitForSeconds(startWait);
        // ���� �ð� ���� �� ����
        while (true)
        {
            CreateEnemey();
            // ���� ������ �� �� �����ð��� �ٽ� ����
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
