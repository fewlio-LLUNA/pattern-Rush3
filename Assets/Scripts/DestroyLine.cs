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
        // ノーツ処理
        if (other.CompareTag("Note"))
        {
            NoteMover mover = other.GetComponent<NoteMover>();
            if (mover != null)
            {
                if (mover.wasJudged)
                {
                    // すでに判定されていたらスキップ
                    return;
                }

                mover.wasJudged = true;
                scoreManager.AddJudgement("Miss");
                uiManager.DisplayJudgement("Miss");
            }

            Destroy(other.gameObject);
            return;
        }

        // 小節線（BarLine）もDestroy
        if (other.CompareTag("BarLine"))
        {
            Destroy(other.gameObject);
        }
    }
}
