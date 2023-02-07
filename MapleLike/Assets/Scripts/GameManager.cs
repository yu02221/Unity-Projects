using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float respawnTime;
    public GameObject monsterObj;
    public float beackGroundLeft;
    public float beackGroundRight;
    public float beackGroundTop;
    public float beackGroundBottom;

    private float timeCounter;
    private int monsterCount;
    private int maxMonster;


    private void Start()
    {
        SceneManager.LoadScene("Map1");
        beackGroundLeft = -13.6f;
        beackGroundRight = -5.7f;
        beackGroundTop = 5.0f;
        beackGroundBottom = 1.0f;

        StartSceen();
    }
    private void Update()
    {
        timeCounter += Time.deltaTime;
        monsterCount = (int)GameObject.FindGameObjectsWithTag("Monster").Length;

        Transform[] points = GameObject.Find("SpawnPoint").
            GetComponentsInChildren<Transform>();
        maxMonster = points.Length;
        if (timeCounter >= 10.0)
        {
            for (int i = 0; i < maxMonster - monsterCount; i++)
            {
                Instantiate(monsterObj, points[i].position, Quaternion.identity);
            }
            timeCounter = 0;
        }
    }

    public void ChangeMap()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "Map1")
        {
            SceneManager.LoadScene("Map2");
            beackGroundLeft = 3.2f;
            beackGroundRight = 20.3f;
            beackGroundTop = 5.3f;
        }
        if (scene.name == "Map2")
        {
            SceneManager.LoadScene("Map1");
            beackGroundLeft = -13.6f;
            beackGroundRight = -5.7f;
            beackGroundTop = 5.0f;
        }
        StartSceen();
    }

    private void StartSceen()
    {
        timeCounter = respawnTime - 2.0f;
    }
}
