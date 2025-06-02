using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEndManager : MonoBehaviour
{
    [SerializeField] private NoteSpawner noteSpawner;

    private void Start()
    {
        // 自動参照（念のため）
        if (noteSpawner == null)
        {
            noteSpawner = Object.FindFirstObjectByType<NoteSpawner>();
        }
    }
    public void EndGame()
    {
        // データ保存
        ResultData.patternName = noteSpawner.patternName;
        ResultData.bpm = noteSpawner.bpm;

        var scoreManager = ScoreManager.Instance;

        // スコア計算をここで呼ぶ！
        scoreManager.CalculateFinalScore();

        // データ保存
        ResultData.score = scoreManager.GetScore();
        ResultData.perfectCount = scoreManager.GetPerfect();
        ResultData.greatCount = scoreManager.GetGreat();
        ResultData.goodCount = scoreManager.GetGood();
        ResultData.missCount = scoreManager.GetMiss();
        ResultData.maxCombo = scoreManager.GetMaxCombo();

        // 結果画面へ
        SceneManager.LoadScene("ResultScene");
    }
}
