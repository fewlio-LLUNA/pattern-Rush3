using UnityEngine;

public class TopSceneUIManager : MonoBehaviour
{
    // スタートボタンから呼び出す：StairSelectSceneへ遷移
    public void OnStartButton()
    {
        SceneTransitionManager.Instance.LoadScene("StairSelectScene");
    }

    // EXITボタンから呼び出す：アプリケーション終了
    public void OnExitButton()
    {
        // Unityエディタでは止まらないので、エディタ確認用ログ
        Debug.Log("アプリケーションを終了します。");

        // ビルドされたアプリケーションの場合に終了する
        Application.Quit();
    }
}
