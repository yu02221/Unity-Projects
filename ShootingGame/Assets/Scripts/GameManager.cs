using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public GameObject gameOverText;
    public GameObject restartText;
    public GameObject gameRestart;

    public GameObject explosionFactory;
    public GameObject player;

    public Text LifeCountText;
    public bool isGameOver;

    public int lifeCount;

    private void Start()
    {
        isGameOver = false;
        SetLifeCount();
        // 傈贸府扁
#if UNITY_ANDROID
        GameObject.Find("Joystick canvas XYBZ").SetActive(true);
#elif UNITY_EDITOR || UNITY_STANDALONE
        GameObject.Find("Joystick canvas XYBZ").SetActive(false);
#endif
    }
    private void Update()
    {
#if UNITY_STANDALONE
        if (isGameOver == true)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                GameRestart();
            }
        }
#endif
    }
    public void GameOver()
    {
        GameObject explosion = Instantiate(explosionFactory);
        explosion.transform.position = player.transform.position;

        isGameOver = true;
        gameOverText.SetActive(true);
#if UNITY_STANDALONE
        restartText.SetActive(true);
#elif UNITY_ANDROID
        gameRestart.SetActive(true);
#endif
    }

    public void GameRestart()
    {
        SceneManager.LoadScene("Shooting");
    }

    public void SetLifeCount()
    {
        LifeCountText.text = "巢篮 格见 : " + lifeCount;
    }
}
