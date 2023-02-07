using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayManager : MonoBehaviour
{
    public bool playEnd;
    public float limitTime;
    public int enemyCount;

    public Text timeLabel;
    public Text enemyLabel;
    public GameObject finalGUI;
    public Text finalMessage;
    public Text finalScoreLabel;

    public Text playerName;
    public Animator timePlusAnim;

    private void Start()
    {
        enemyLabel.text = string.Format("Enemy {0}", enemyCount);
        timeLabel.text = string.Format("Time {0:N2}", limitTime);

        playerName.text = PlayerPrefs.GetString("UserName");
    }

    private void Update()
    {
        if (!playEnd)
        {
            if (limitTime > 0)
                limitTime -= Time.deltaTime;

            if (limitTime < 0)
            {
                limitTime = 0;
                GameOver();
            }
            timeLabel.text = string.Format("Time {0:N2}", limitTime);
        }
    }

    public void Clear()
    {
        if (!playEnd)
        {
            Time.timeScale = 0;
            playEnd = true;
            finalMessage.text = "Clear!!";

            PlayerController pc = GameObject.Find("Player").GetComponent<PlayerController>();
            float score = 12345f + limitTime * 123f + pc.hp * 123f;
            finalScoreLabel.text = string.Format("{0:N0}", score);

            finalGUI.SetActive(true);

            BestCheck(score);
        }
    }

    public void GameOver()
    {
        if (!playEnd)
        {
            Time.timeScale = 0;
            playEnd = true;
            finalMessage.text = "Fail...";
            float score = 1234f - enemyCount * 123f;
            finalScoreLabel.text = string.Format("{0:N0}", score);
            finalGUI.SetActive(true);

            BestCheck(score);

            PlayerController pc = GameObject.Find("Player").GetComponent<PlayerController>();
            pc.playerState = PlayerState.Dead;
        }
    }

    public void Replay()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("MainPlay");
    }

    public void Quit()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Title");
    }

    public void EnemyDie()
    {
        enemyCount--;
        enemyLabel.text = string.Format("Enemy {0}", enemyCount);

        limitTime += 5;
        timePlusAnim.SetTrigger("EnemyDie");

        if (enemyCount <= 0)
            Clear();
    }

    private void BestCheck(float score)
    {
        float bestScore1 = PlayerPrefs.GetFloat("BestScore1");
        float bestScore2 = PlayerPrefs.GetFloat("BestScore2");
        float bestScore3 = PlayerPrefs.GetFloat("BestScore3");

        if (score > bestScore1)
        {
            PlayerPrefs.SetFloat("BestScore3", PlayerPrefs.GetFloat("BestScore2"));
            PlayerPrefs.SetString("BestPlayer3", PlayerPrefs.GetString("BestPlayer2"));
            PlayerPrefs.SetFloat("BestScore2", PlayerPrefs.GetFloat("BestScore1"));
            PlayerPrefs.SetString("BestPlayer2", PlayerPrefs.GetString("BestPlayer1"));
            PlayerPrefs.SetFloat("BestScore1", score);
            PlayerPrefs.SetString("BestPlayer1", PlayerPrefs.GetString("UserName"));
        }
        else if (score > bestScore2)
        {
            PlayerPrefs.SetFloat("BestScore3", PlayerPrefs.GetFloat("BestScore2"));
            PlayerPrefs.SetString("BestPlayer3", PlayerPrefs.GetString("BestPlayer2"));
            PlayerPrefs.SetFloat("BestScore2", score);
            PlayerPrefs.SetString("BestPlayer2", PlayerPrefs.GetString("UserName"));
        }
        else if (score > bestScore3)
        {
            PlayerPrefs.SetFloat("BestScore3", score);
            PlayerPrefs.SetString("BestPlayer3", PlayerPrefs.GetString("UserName"));
        }

        PlayerPrefs.Save();
    }
}
