using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Animator startButton;
    public Animator settingsButton;
    public Animator settingsDlg;
    public Animator contentPanel;
    public Animator gearImage;
    public AudioSource backgroundMusic;
    public Slider volumeSdr;
    public Toggle soundTgl;

    private void Start()
    {
        backgroundMusic.volume = PlayerPrefs.GetFloat("Volume");
        volumeSdr.value = PlayerPrefs.GetFloat("Volume");
        backgroundMusic.mute = System.Convert.ToBoolean(
            PlayerPrefs.GetInt("Mute"));
        soundTgl.isOn = System.Convert.ToBoolean(
            PlayerPrefs.GetInt("Mute"));
    }
    public void VolumeSetting()
    {
        PlayerPrefs.SetFloat("Volume", backgroundMusic.volume);
        // º¼·ý Á¶Àý½Ã ¹ÂÆ® ÇØÁ¦
        if (backgroundMusic.mute == true)
        {
            PlayerPrefs.SetInt("Mute", System.Convert.ToInt16(false));
            soundTgl.isOn = false;
            backgroundMusic.mute = false;
        }
        PlayerPrefs.Save();
    }

    public void MuteSetting()
    {
        bool isMute = backgroundMusic.mute;
        PlayerPrefs.SetInt("Mute", System.Convert.ToInt16(isMute));
        PlayerPrefs.Save();
    }
    public void OpenSettings()
    {
        startButton.SetBool("isHidden", true);
        settingsButton.SetBool("isHidden", true);
        settingsDlg.SetBool("isHidden", false);
    }

    public void CloseSettings()
    {
        startButton.SetBool("isHidden", false);
        settingsButton.SetBool("isHidden", false);
        settingsDlg.SetBool("isHidden", true);
    }

    public void ToggleSlideMenu()
    {
        bool isHidden = contentPanel.GetBool("isHidden");
        contentPanel.SetBool("isHidden", !isHidden);
        gearImage.SetBool("isHidden", !isHidden);

    }

    public void StartGame()
    {
        SceneManager.LoadScene("Main");
    }

}
