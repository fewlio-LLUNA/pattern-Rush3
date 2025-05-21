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
            NoteMover mover = other.GetComponent<NoteMover>();
            if (mover != null && mover.wasJudged)
            {
                // すでに叩かれていたらスキップ
                return;
            }

            mover.wasJudged = true;

            Destroy(other.gameObject);
            scoreManager.AddJudgement("Miss");
            uiManager.DisplayJudgement("Miss");
        }
    }
}
