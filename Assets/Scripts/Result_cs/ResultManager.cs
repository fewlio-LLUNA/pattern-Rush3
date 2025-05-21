using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResultManager : MonoBehaviour
{
    public TextMeshProUGUI musicNameText;
    public TextMeshProUGUI bpmText;

    public TextMeshProUGUI perfectText;
    public TextMeshProUGUI greatText;
    public TextMeshProUGUI goodText;
    public TextMeshProUGUI missText;

    public TextMeshProUGUI maxComboText;
    public TextMeshProUGUI scoreText;

    void Start()
    {
        // データ取得
        ResultData data = ResultData.Instance;

        musicNameText.text = "譜面名：螺旋階段";
        bpmText.text = "BPM：" + data.bpm;

        perfectText.text = data.perfectCount.ToString();
        greatText.text = data.greatCount.ToString();
        goodText.text = data.goodCount.ToString();
        missText.text = data.missCount.ToString();
        maxComboText.text = data.maxCombo.ToString();
        scoreText.text = data.score.ToString("N0");
    }

    public void OnClickRetry()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("PlayScene");
    }
}
