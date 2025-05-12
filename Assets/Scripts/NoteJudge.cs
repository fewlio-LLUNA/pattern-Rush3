using UnityEngine;

public class NoteJudge : MonoBehaviour
{
    public Transform judgeLine; // Å©1Ç¬ÇÃJudgeLineÇ…ïœçX
    public KeyCode[] keys = { KeyCode.A, KeyCode.D, KeyCode.J, KeyCode.L };
    public float[] judgeThresholds = { 0.02083f, 0.04167f, 0.08333f }; // ïb

    public UIManager uiManager;

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
        GameObject nearestNote = null;
        float minTimeDiff = float.MaxValue;

        foreach (GameObject note in notes)
        {
            NoteMover mover = note.GetComponent<NoteMover>();
            if (mover.laneIndex != laneIndex) continue;

            float timeDiff = Mathf.Abs(note.transform.position.y - judgeLine.position.y) / mover.speed;
            if (timeDiff < minTimeDiff)
            {
                minTimeDiff = timeDiff;
                nearestNote = note;
            }
        }

        if (nearestNote != null)
        {
            string result;
            if (minTimeDiff < judgeThresholds[0])
                result = "Perfect";
            else if (minTimeDiff < judgeThresholds[1])
                result = "Great";
            else if (minTimeDiff < judgeThresholds[2])
                result = "Good";
            else
                result = "Miss";

            Destroy(nearestNote);
            uiManager.DisplayJudgement(result);
        }
    }
}
