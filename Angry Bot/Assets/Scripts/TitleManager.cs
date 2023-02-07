using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public InputField nameInput;
    public GameObject bestDate;
    public Text bestUserDate;
    public GameObject nameRequired;


    private void Update()
    {
        if (!string.IsNullOrEmpty(nameInput.text))
            nameRequired.SetActive(false);
    }
    public void GoPlay()
    {
        if (string.IsNullOrEmpty(nameInput.text))
        {
            nameRequired.SetActive(true);
            return;
        }
        PlayerPrefs.SetString("UserName", nameInput.text);
        PlayerPrefs.Save();
        SceneManager.LoadScene("MainPlay");
    }

    public void BestScore()
    {
        string name1 = PlayerPrefs.GetString("BestPlayer1");
        float score1 = PlayerPrefs.GetFloat("BestScore1");
        string name2 = PlayerPrefs.GetString("BestPlayer2");
        float score2 = PlayerPrefs.GetFloat("BestScore2");
        string name3 = PlayerPrefs.GetString("BestPlayer3");
        float score3 = PlayerPrefs.GetFloat("BestScore3");

        if (!string.IsNullOrEmpty(name1))
        {
            bestUserDate.text = string.Format("1st - {0} : {1:N0}", name1, score1);
            bestDate.SetActive(true);
        }
        if (!string.IsNullOrEmpty(name2))
        {
            bestUserDate.text += string.Format("\n2nd - {0} : {1:N0}", name2, score2);
        }
        if (!string.IsNullOrEmpty(name3))
        {
            bestUserDate.text += string.Format("\n3rd - {0} : {1:N0}", name3, score3);
        }
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
