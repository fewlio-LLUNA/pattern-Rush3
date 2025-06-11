using System.Collections;
using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    [Header("BPM設定（ScriptableObject）")]
    public BPMData bpmData; // Inspector で BPMData.asset をアタッチ

    [Header("ノーツ設定")]
    public GameObject[] notePrefabs;
    public Transform[] spawnPoints;

    [Header("小節線設定")]
    public GameObject barLinePrefab;
    public Transform barLineSpawnPoint;

    [Header("その他設定")]
    public string patternName = "spiral staircase";
    public float noteSpeed = 10f;

    public float bpm;               // ScriptableObjectから取得する
    private float noteInterval;     // 16分音符間隔（秒）
    private float barLineInterval;  // 小節間隔（秒）
    private float endWaitTime;      // 最後に待機する時間（秒）

    private int[] pattern = { 0, 1, 2, 3, 2, 1 }; // 折り返し階段パターン

    private bool allNotesSpawned = false;
    private bool gameEnded = false;
    private float endTimer = 0f;

    void Start()
    {
        // BPM を ScriptableObject から取得
        bpm = bpmData.bpm;

        // BPM に基づく時間間隔計算
        noteInterval = 60f / bpm / 4f;       // 16分音符
        barLineInterval = 60f / bpm * 4f;    // 小節（4拍）
        endWaitTime = 60f / bpm * 4f;        // 終了待機も4拍

        StartCoroutine(SpawnAll());
    }

    IEnumerator SpawnAll()
    {
        // 初期の2秒は無音
        yield return new WaitForSeconds(2f);

        int index = 0;
        float elapsed = 0f;
        float totalDuration = 30f;
        int beatCount = 0;

        while (elapsed < totalDuration)
        {
            // パターンに従ってノーツ生成
            int lane = pattern[index % pattern.Length];
            GameObject note = Instantiate(notePrefabs[lane], spawnPoints[lane].position, Quaternion.identity);
            note.tag = "Note";

            // ノーツの移動設定
            var mover = note.GetComponent<NoteMover>();
            if (mover != null)
            {
                mover.speed = noteSpeed;
                mover.laneIndex = lane;
            }

            // 小節線を定期的に生成（16拍ごと＝4小節）
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

        // 全ノーツ生成完了フラグ
        allNotesSpawned = true;
    }

    void Update()
    {
        // 全ノーツ生成後、すべてのノーツが消えるのを待ってリザルトへ
        if (allNotesSpawned && !gameEnded)
        {
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
