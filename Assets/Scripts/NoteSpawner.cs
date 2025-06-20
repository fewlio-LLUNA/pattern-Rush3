using System.Collections;
using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    [Header("BPM設定（ScriptableObject）")]
    public BPMData bpmData;

    [Header("ノーツ設定")]
    public GameObject[] notePrefabs;
    public Transform[] spawnPoints;

    [Header("小節線設定")]
    public GameObject barLinePrefab;
    public Transform barLineSpawnPoint;

    [Header("その他設定")]
    public string patternName = "spiral staircase";
    public float noteSpeed = 10f;

    public float bpm;
    private float noteInterval;
    private float barLineInterval;
    private float endWaitTime;

    private NoteStep[] pattern;  // 変更点①：NoteStep配列に変更

    private bool allNotesSpawned = false;
    private bool gameEnded = false;
    private float endTimer = 0f;

    void Start()
    {
        bpm = bpmData.bpm;

        noteInterval = 60f / bpm / 4f;
        barLineInterval = 60f / bpm * 4f;
        endWaitTime = 60f / bpm * 4f;

        // 変更点②：pattern取得とnullチェック
        if (SelectedPattern.pattern != null && SelectedPattern.pattern.Length > 0)
        {
            pattern = SelectedPattern.pattern;
            patternName = SelectedPattern.patternName;
        }
        else
        {
            Debug.LogError("SelectedPattern.pattern が設定されていません！");
            pattern = new NoteStep[0]; // 空配列で回避
        }

        StartCoroutine(SpawnAll());
    }

    IEnumerator SpawnAll()
    {
        yield return new WaitForSeconds(2f);

        int index = 0;
        float elapsed = 0f;
        float totalDuration = 30f;
        int beatCount = 0;

        while (elapsed < totalDuration && pattern.Length > 0)
        {
            // 変更点③：複数レーン対応
            NoteStep currentStep = pattern[index % pattern.Length];
            foreach (int lane in currentStep.lanes)
            {
                if (lane >= 0 && lane < notePrefabs.Length)
                {
                    GameObject note = Instantiate(notePrefabs[lane], spawnPoints[lane].position, Quaternion.identity);
                    note.tag = "Note";

                    var mover = note.GetComponent<NoteMover>();
                    if (mover != null)
                    {
                        mover.speed = noteSpeed;
                        mover.laneIndex = lane;
                    }
                }
                else
                {
                    Debug.LogWarning($"レーン {lane} は存在しません。");
                }
            }

            // 小節線（16ステップごと）
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

        allNotesSpawned = true;
    }

    void Update()
    {
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
                }
            }
        }
    }
}
