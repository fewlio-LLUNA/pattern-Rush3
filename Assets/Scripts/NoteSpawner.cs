using System.Collections;
using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    public GameObject[] notePrefabs;
    public Transform[] spawnPoints;

    public GameObject barLinePrefab;
    public Transform barLineSpawnPoint;

    public string patternName = "螺旋階段";
    public float bpm = 120f;
    public float noteSpeed = 10f;

    private float noteInterval;
    private float barLineInterval;

    private int[] pattern = { 0, 1, 2, 3, 2, 1 };

    private bool allNotesSpawned = false;
    private bool gameEnded = false;
    private float endTimer = 0f;
    private float endWaitTime;

    void Start()
    {
        noteInterval = 60f / bpm / 4f;       // 16分音符
        barLineInterval = 60f / bpm * 4f;    // 1小節（4拍）
        endWaitTime = (60f / bpm) * 4f;      // 4拍待機

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
            int lane = pattern[index % pattern.Length];
            GameObject note = Instantiate(notePrefabs[lane], spawnPoints[lane].position, Quaternion.identity);
            note.tag = "Note"; // ← ここ重要：タグを設定！

            var mover = note.GetComponent<NoteMover>();
            if (mover != null)
            {
                mover.speed = noteSpeed;
                mover.laneIndex = lane;
            }

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

        // 全ノーツ生成完了
        allNotesSpawned = true;
    }

    void Update()
    {
        if (allNotesSpawned && !gameEnded)
        {
            // タグ付きノーツが全て消えたかを確認
            if (GameObject.FindGameObjectsWithTag("Note").Length == 0)
            {
                endTimer += Time.deltaTime;
                if (endTimer >= endWaitTime)
                {
                    gameEnded = true;
                    var gameEndManager = FindFirstObjectByType<GameEndManager>();
                    if (gameEndManager != null)
                    {
                        gameEndManager.EndGame();
                    }
                    else
                    {
                        Debug.LogWarning("GameEndManager が見つかりません！");
                    }
                }
            }
        }
    }
}
