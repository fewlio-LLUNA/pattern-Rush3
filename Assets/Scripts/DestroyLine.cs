using UnityEngine;

public class DestroyLine : MonoBehaviour
{
    // UIManagerをインスペクターで設定できるようにする
    public UIManager uiManager;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Note"))
        {
            Destroy(other.gameObject);
            uiManager.DisplayJudgement("Miss");
        }
    }
}
