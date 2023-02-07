using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    // �̱� �� ����
    public static ScoreManager Instance = null;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    // ���� ���� UI
    public Text currentScoreUI;
    // ���� ����
    private int currentScore;

    // �ְ� ���� UI
    public Text bestScoreUI;
    // �ְ� ����
    private int bestScore;

    public void SetScore(int value)
    {
        // �������� ++
        currentScore = value;
        // �������� ���
        currentScoreUI.text = "���� ���� : " + currentScore;
        // �������� > �ְ������̸� �ְ����� ���� �� ���
        if (currentScore > bestScore)
        {
            bestScore = currentScore;
            bestScoreUI.text = "�ְ� ���� : " + bestScore;
            // "Best Score"��� Ű�� �ְ� ������ ����
            PlayerPrefs.SetInt("Best Score", bestScore);
            // Set ������ ȣ�� �� ���������� Save�� �ؾ� �����
            PlayerPrefs.Save();
        }
    }

    public int GetScore()
    {
        return currentScore;
    }

    private void Start()
    {
        currentScore = 0;
        currentScoreUI.text = "���� ���� : " + currentScore;
        // "Best Score"��� Ű�� ����� ���� �ҷ��� bestScore�� ���� �� ���
        bestScore = PlayerPrefs.GetInt("Best Score", 0);
        bestScoreUI.text = "�ְ� ���� : " + bestScore;
    }
}
