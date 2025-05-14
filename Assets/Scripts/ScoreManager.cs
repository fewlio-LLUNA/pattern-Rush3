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
                currentCombo = 0;
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

        int great = 10_000_000 / totalNotes;
        int perfect = great + 1;

        int missPenalty = Mathf.CeilToInt(10_000_000f / totalNotes);
        int goodPenalty = missPenalty / 2;

        int good = 10_000 - goodPenalty;
        int miss = 0;

        totalScore =
            perfectCount * perfect +
            greatCount * great +
            goodCount * good +
            missCount * miss;
    }

    public int GetScore() => totalScore;
    public int GetMaxCombo() => maxCombo;

    public int GetPerfect() => perfectCount;
    public int GetGreat() => greatCount;
    public int GetGood() => goodCount;
    public int GetMiss() => missCount;
}
