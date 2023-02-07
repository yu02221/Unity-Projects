using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    // 싱글 톤 패턴
    public static ScoreManager Instance = null;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    // 현재 점수 UI
    public Text currentScoreUI;
    // 현재 점수
    private int currentScore;

    // 최고 점수 UI
    public Text bestScoreUI;
    // 최고 점수
    private int bestScore;

    public void SetScore(int value)
    {
        // 현재점수 ++
        currentScore = value;
        // 현재점수 출력
        currentScoreUI.text = "현재 점수 : " + currentScore;
        // 현재점수 > 최고점수이면 최고점수 갱신 및 출력
        if (currentScore > bestScore)
        {
            bestScore = currentScore;
            bestScoreUI.text = "최고 점수 : " + bestScore;
            // "Best Score"라는 키로 최고 점수를 저장
            PlayerPrefs.SetInt("Best Score", bestScore);
            // Set ㅎ마수 호출 후 최종적으로 Save를 해야 저장됨
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
        currentScoreUI.text = "현재 점수 : " + currentScore;
        // "Best Score"라는 키로 저장된 값을 불러와 bestScore에 저장 후 출력
        bestScore = PlayerPrefs.GetInt("Best Score", 0);
        bestScoreUI.text = "최고 점수 : " + bestScore;
    }
}
