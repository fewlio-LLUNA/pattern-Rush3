using System.Collections;
using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    public GameObject[] notePrefabs;
    public Transform[] spawnPoints;

    public float bpm = 120f;
    public float noteSpeed = 10f;

    private float interval;
    private int[] pattern = { 0, 1, 2, 3, 2, 1 };
    private int[] finalPattern = { 0, 1, 2, 3, 2, 1, 0 };

    void Start()
    {
        interval = 60f / bpm / 4f; // 16分音符
        StartCoroutine(SpawnNotes());
    }

    IEnumerator SpawnNotes()
    {
        yield return new WaitForSeconds(2f); // 最初の1小節はメトロノームのみ

        float elapsed = 0f;
        int index = 0;
        float totalDuration = 30f;

        while (elapsed + interval * finalPattern.Length <= totalDuration)
        {
            int lane = pattern[index % pattern.Length];
            GameObject note = Instantiate(notePrefabs[lane], spawnPoints[lane].position, Quaternion.identity);

            NoteMover mover = note.GetComponent<NoteMover>();
            if (mover != null)
            {
                mover.speed = noteSpeed;
                mover.laneIndex = lane;
            }

            index++;
            elapsed += interval;
            yield return new WaitForSeconds(interval);
        }

        // 最後に 1,2,3,4,3,2,1 を追加
        foreach (int lane in finalPattern)
        {
            GameObject note = Instantiate(notePrefabs[lane], spawnPoints[lane].position, Quaternion.identity);

            NoteMover mover = note.GetComponent<NoteMover>();
            if (mover != null)
            {
                mover.speed = noteSpeed;
                mover.laneIndex = lane;
            }

            yield return new WaitForSeconds(interval);
        }
    }
}
