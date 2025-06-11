using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    private int perfectCount = 0, greatCount = 0, goodCount = 0, missCount = 0;
    private int currentCombo = 0, maxCombo = 0;
    private int totalScore = 0;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void AddJudgement(string result)
    {
        switch (result)
        {
            case "Perfect":
                perfectCount++;
                currentCombo++;
                break;
            case "Great":
                greatCount++;
                currentCombo++;
                break;
            case "Good":
                goodCount++;
                currentCombo++;
                break;
            case "Miss":
                missCount++;
                currentCombo = 0;  // コンボリセット
                break;
        }

        if (currentCombo > maxCombo)
        {
            maxCombo = currentCombo;
        }
    }

    public void CalculateFinalScore()
    {
        int totalNotes = perfectCount + greatCount + goodCount + missCount;
        if (totalNotes == 0) return;

        int baseScore = 10_000_000;
        int missPenalty = Mathf.CeilToInt((float)baseScore / totalNotes);
        int goodPenalty = missPenalty / 2;

        int scorePerGreat = baseScore / totalNotes;
        int scorePerPerfect = scorePerGreat + 1;
        int scorePerGood = scorePerGreat - goodPenalty;
        int scorePerMiss = scorePerGreat - missPenalty;  // これは基本的に 0 になる

        totalScore =
            perfectCount * scorePerPerfect +
            greatCount * scorePerGreat +
            goodCount * scorePerGood +
            missCount * scorePerMiss;

        if (totalScore < 0) totalScore = 0;
    }

    public int GetScore() => totalScore;
    public int GetMaxCombo() => maxCombo;
    public int GetCurrentCombo() => currentCombo;  // ←追加

    public int GetPerfect() => perfectCount;
    public int GetGreat() => greatCount;
    public int GetGood() => goodCount;
    public int GetMiss() => missCount;
}
