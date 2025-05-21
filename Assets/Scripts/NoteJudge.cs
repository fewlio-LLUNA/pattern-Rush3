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
            if (mover.laneIndex != laneIndex || mover.wasJudged) continue; // 既に判定済ならスキップ

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

            bestCandidate.GetComponent<NoteMover>().wasJudged = true;

            Destroy(bestCandidate);

            // 先にスコアカウントを更新する
            scoreManager.AddJudgement(result);
            uiManager.DisplayJudgement(result);
        }
        else if (noteExistsInLane)
        {
            foreach (GameObject note in notes)
            {
                NoteMover mover = note.GetComponent<NoteMover>();
                if (mover.laneIndex == laneIndex && !mover.wasJudged)
                {
                    float yDiff = Mathf.Abs(note.transform.position.y - judgeLine.position.y);
                    float timeDiff = yDiff / mover.speed;

                    if (timeDiff >= judgeThresholds[3])
                    {
                        mover.wasJudged = true; // ← Miss判定であっても「判定済み」にすることが重要
                        scoreManager.AddJudgement("Miss");
                        uiManager.DisplayJudgement("Miss");
                        return;
                    }
                }
            }
        }
    }
}
