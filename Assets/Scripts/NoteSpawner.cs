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

    void Start()
    {
        interval = 60f / bpm / 4f; // 16分音符（1拍=1/4小節）
        StartCoroutine(SpawnNotes());
    }

    IEnumerator SpawnNotes()
    {
        yield return new WaitForSeconds(2f); // 最初の1小節分（2秒）

        float elapsed = 0f;
        int index = 0;
        float totalDuration = 30f;

        while (elapsed + interval <= totalDuration)
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
    }
}
