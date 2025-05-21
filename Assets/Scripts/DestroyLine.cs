using UnityEngine;

public class DestroyLine : MonoBehaviour
{
    public UIManager uiManager;

    private ScoreManager scoreManager;

    private void Start()
    {
        scoreManager = ScoreManager.Instance;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Note"))
        {
            Destroy(other.gameObject);
            scoreManager.AddJudgement("Miss"); // ← Miss数カウントとコンボリセット
            uiManager.DisplayJudgement("Miss"); // ← UI表示（変更なし）
        }
    }
}
