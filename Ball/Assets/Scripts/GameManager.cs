using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int coinCount;
    public Text coinText;
    public Text success;
    public Text score;

    // 목표지점 골인 시 게임 일시정지 및 Success문구와 Score 표시
    public void SuccessGame()
    {
        coinText.text = "";
        success.text = "Success!";
        score.text = "Score : " + coinCount;
        Time.timeScale = 0;
    }

    public void GetCoin()
    {
        coinCount++;
        coinText.text = "Score : " + coinCount;
        //Debug.Log("동전: " + coinCount);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Game");
    }

    

    public void  RedCoinStart()
    {
        DestroyObstacles();
    }

    public void DestroyObstacles()
    {
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        for (int i = 0; i < obstacles.Length; i++)
        {
            Destroy(obstacles[i]);
        }
    }
}
