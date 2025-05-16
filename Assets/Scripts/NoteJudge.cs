using UnityEngine;

public class NoteJudge : MonoBehaviour
{
    public Transform judgeLine;
    public KeyCode[] keys = { KeyCode.A, KeyCode.D, KeyCode.J, KeyCode.L };

    // 判定閾値（秒）: Perfect, Great, Good, Miss
    private readonly float[] judgeThresholds = { 0.02083f, 0.04167f, 0.08333f, 0.10416f };

    public UIManager uiManager;
    public ScoreManager scoreManager;

    void Update()
    {
        for (int i = 0; i < keys.Length; i++)
        {
            if (Input.GetKeyDown(keys[i]))
            {
                JudgeNoteInLane(i);
            }
        }
    }

    void JudgeNoteInLane(int laneIndex)
    {
        GameObject[] notes = GameObject.FindGameObjectsWithTag("Note");
        GameObject bestCandidate = null;
        float bestTimeDiff = float.MaxValue;
        bool noteExistsInLane = false;

        foreach (GameObject note in notes)
        {
            NoteMover mover = note.GetComponent<NoteMover>();
            if (mover.laneIndex != laneIndex) continue;

            noteExistsInLane = true;

            float timeDiff = Mathf.Abs(note.transform.position.y - judgeLine.position.y) / mover.speed;

            if (timeDiff < judgeThresholds[3] && timeDiff < bestTimeDiff)
            {
                bestTimeDiff = timeDiff;
                bestCandidate = note;
            }
        }

        if (bestCandidate != null)
        {
            string result;

            if (bestTimeDiff < judgeThresholds[0])
                result = "Perfect";
            else if (bestTimeDiff < judgeThresholds[1])
                result = "Great";
            else if (bestTimeDiff < judgeThresholds[2])
                result = "Good";
            else
                result = "Miss";

            Destroy(bestCandidate);
            uiManager.DisplayJudgement(result);
            scoreManager.AddJudgement(result);
        }
        else if (noteExistsInLane)
        {
            // 判定圏内ノーツなし、Miss判定
            uiManager.DisplayJudgement("Miss");
            scoreManager.AddJudgement("Miss");
        }
        // ノーツ自体がなければ何もしない
    }
}
