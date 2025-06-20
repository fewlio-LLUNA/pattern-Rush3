using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// SceneTransitionManagerクラス：シーン移動を管理する記憶係の設計図
public class SceneTransitionManager : MonoBehaviour
{
    public static SceneTransitionManager Instance { get; private set; }
    private Stack<string> sceneHistory = new Stack<string>();

    // ▼▼▼【追加①】セーブポイントを覚えておくための変数を追加▼▼▼
    private string savePointScene;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// 新しいシーンに移動する時の命令
    /// </summary>
    public void LoadScene(string sceneName)
    {
        string currentScene = SceneManager.GetActiveScene().name;
        sceneHistory.Push(currentScene);
        SceneManager.LoadScene(sceneName);
    }

    /// <summary>
    /// 一つ前のシーンに戻る時の命令
    /// </summary>
    public void LoadPreviousScene()
    {
        if (sceneHistory.Count > 0)
        {
            string previousScene = sceneHistory.Pop();
            SceneManager.LoadScene(previousScene);
        }
        else
        {
            Debug.LogWarning("これ以上戻る場所のメモがありませーん！");
        }
    }

    /// <summary>
    /// 今いる場所を「セーブポイント」として記憶してから、新しいシーンに移動する
    /// </summary>
    public void LoadSceneAndSetSavePoint(string sceneName)
    {
        // 今いるシーンの名前をセーブポイントとして記憶
        savePointScene = SceneManager.GetActiveScene().name;
        // 普通にシーン移動（履歴にもちゃんと残す）
        LoadScene(sceneName);
    }

    /// <summary>
    /// 記憶しておいた「セーブポイント」のシーンに戻る
    /// </summary>
    public void LoadSavePoint()
    {
        // セーブポイントが記録されていれば
        if (!string.IsNullOrEmpty(savePointScene))
        {
            // 履歴は一旦リセットして、セーブポイントに直接飛ぶ
            // （こうしないと、戻った先からさらに戻る時におかしくなる可能性があるため）
            sceneHistory.Clear();
            SceneManager.LoadScene(savePointScene);
        }
        else
        {
            Debug.LogWarning("戻るべきセーブポイントがありません。");
            // 例えばタイトルに戻るなどの処理を入れても良い
            // LoadScene("TopScene");
        }
    }
}