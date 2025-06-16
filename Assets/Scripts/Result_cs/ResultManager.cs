using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultManager : MonoBehaviour
{
    public TextMeshProUGUI patternTitleText;
    public TextMeshProUGUI bpmText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI perfectText;
    public TextMeshProUGUI greatText;
    public TextMeshProUGUI goodText;
    public TextMeshProUGUI missText;
    public TextMeshProUGUI maxComboText;

    public void Start()
    {
        // ÉfÅ[É^ÇUIÇ…îΩâf
        patternTitleText.text = ResultData.patternName;
        bpmText.text = ResultData.bpm.ToString("F1");
        scoreText.text = ResultData.score.ToString("N0");
        perfectText.text = ResultData.perfectCount.ToString();
        greatText.text = ResultData.greatCount.ToString();
        goodText.text = ResultData.goodCount.ToString();
        missText.text = ResultData.missCount.ToString();
        maxComboText.text = ResultData.maxCombo.ToString();
    }

    public void OnRetryButtonPressed()
    {
        SceneManager.LoadScene("PlayScene");
    }
    public void OnHomeButtonPressed()
    {
        SceneManager.LoadScene("SelectScene");
    }
}
