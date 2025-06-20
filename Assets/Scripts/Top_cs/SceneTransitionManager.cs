using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// SceneTransitionManagerクラス：シーン移動を管理する記憶係の設計図
public class SceneTransitionManager : MonoBehaviour
{
    // ▼▼▼ 記憶係はゲーム内に１人だけにするための魔法 ▼▼▼
    public static SceneTransitionManager Instance { get; private set; }

    // ▼▼▼ 記憶係が使う「行った場所メモ帳（スタック）」▼▼▼
    private Stack<string> sceneHistory = new Stack<string>();

    private void Awake()
    {
        // --- ゲームが始まった時の記憶係の自己紹介 ---
        // 「まだ僕みたいな記憶係がいなければ、僕がその役目をやります！」
        if (Instance == null)
        {
            Instance = this;
            // 「そして僕は特別なので、他の部屋に行っても消えません！」
            DontDestroyOnLoad(gameObject);
        }
        // 「あれ、もう記憶係がいるみたいだ。じゃあ僕は自己紹介だけして消えますね」
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
        // 1. 今いる部屋の名前をメモ帳の一番上に書く
        string currentScene = SceneManager.GetActiveScene().name;
        sceneHistory.Push(currentScene);

        // 2. 指定された新しい部屋にテレポート！
        SceneManager.LoadScene(sceneName);
    }

    /// <summary>
    /// 一つ前のシーンに戻る時の命令
    /// </summary>
    public void LoadPreviousScene()
    {
        // 1. メモ帳に何か書いてあるかな？
        if (sceneHistory.Count > 0)
        {
            // 2. メモ帳の一番上のページを破って、そこに書いてある部屋にテレポート！
            string previousScene = sceneHistory.Pop();
            SceneManager.LoadScene(previousScene);
        }
        else
        {
            // メモ帳が空っぽだったら何もしない（最初の画面なので）
            Debug.LogWarning("これ以上戻る場所のメモがありませーん！");
        }
    }
}