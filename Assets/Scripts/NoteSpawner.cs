using System.Collections;
using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    public GameObject[] notePrefabs;
    public Transform[] spawnPoints;

    public GameObject barLinePrefab; // ← 追加
    public Transform barLineSpawnPoint; // ← 追加（Canvas上で中央に設定）

    public float bpm = 120f;
    public float noteSpeed = 10f;

    private float noteInterval;
    private float barLineInterval;

    private int[] pattern = { 0, 1, 2, 3, 2, 1 };

    void Start()
    {
        noteInterval = 60f / bpm / 4f;       // 16分音符
        barLineInterval = 60f / bpm * 4f;    // 1小節（4拍）

        StartCoroutine(SpawnAll());
    }

    IEnumerator SpawnAll()
    {
        yield return new WaitForSeconds(2f);

        int index = 0;
        float elapsed = 0f;
        float totalDuration = 30f;

        int beatCount = 0;

        while (elapsed < totalDuration)
        {
            // ノーツを生成（pattern配列から）
            int lane = pattern[index % pattern.Length];
            GameObject note = Instantiate(notePrefabs[lane], spawnPoints[lane].position, Quaternion.identity);

            var mover = note.GetComponent<NoteMover>();
            if (mover != null)
            {
                mover.speed = noteSpeed;
                mover.laneIndex = lane;
            }

            // 1小節毎にBarLine
            if (beatCount % 16 == 0)
            {
                GameObject barLine = Instantiate(barLinePrefab, barLineSpawnPoint.position, Quaternion.identity);

                var barMover = barLine.GetComponent<NoteMover>();
                if (barMover != null)
                {
                    barMover.speed = noteSpeed;
                }
            }

            index++;
            beatCount++;
            elapsed += noteInterval;
            yield return new WaitForSeconds(noteInterval);
        }
    }

}
