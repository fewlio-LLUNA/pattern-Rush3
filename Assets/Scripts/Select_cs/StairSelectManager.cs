using UnityEngine;
using UnityEngine.SceneManagement;

public class StairSelectManager : MonoBehaviour
{
    // 左ボタン → 別ジャンル（例：Trill）へ遷移
    public void OnLeftButton()
    {
        SceneManager.LoadScene("OtherSelectScene"); // 作成しておく
    }

    // 右ボタン → 別ジャンル（例：Scale）へ遷移
    public void OnRightButton()
    {
        SceneManager.LoadScene("VerticalSelectScene"); // 作成しておく
    }

    // BPM設定画面へ
    public void OnBPMButton()
    {
        SceneManager.LoadScene("BPMSettingScene");
    }
}
