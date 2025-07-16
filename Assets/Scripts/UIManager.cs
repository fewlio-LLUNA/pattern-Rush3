using System.Collections;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI perfectText, greatText, goodText, missText;
    public TextMeshProUGUI judgementText, comboText;

    private void Update()
    {
        // コンボ数を常時表示
        int currentCombo = ScoreManager.Instance.GetCurrentCombo();
        if (currentCombo > 0)
        {
            comboText.text = currentCombo.ToString();
            comboText.gameObject.SetActive(true);
        }
        else
        {
            comboText.gameObject.SetActive(false);
        }
    }

    public void DisplayJudgement(string result)
    {
        switch (result)
        {
            case "Perfect":
                perfectText.text = ScoreManager.Instance.GetPerfect().ToString();
                judgementText.color = new Color32(255, 210, 98, 255); 
                break;
            case "Great":
                greatText.text = ScoreManager.Instance.GetGreat().ToString();
                judgementText.color = new Color32(255, 142, 97, 255); 
                break;
            case "Good":
                goodText.text = ScoreManager.Instance.GetGood().ToString();
                judgementText.color = new Color32(98, 187, 217, 255); 
                break;
            case "Miss":
                missText.text = ScoreManager.Instance.GetMiss().ToString();
                judgementText.color = new Color32(165, 165, 165, 255);
                break;
        }

        StopAllCoroutines();
        StartCoroutine(ShowJudgement(result));
    }

    private IEnumerator ShowJudgement(string result)
    {
        judgementText.text = result;
        judgementText.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        judgementText.gameObject.SetActive(false);
        // comboText は Update で制御されるので非表示にしない
    }
}
