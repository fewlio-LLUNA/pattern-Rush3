using System.Collections;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI perfectText, greatText, goodText, missText;
    public TextMeshProUGUI judgementText, comboText;

    public void DisplayJudgement(string result)
    {
        ScoreManager.Instance.AddJudgement(result);

        switch (result)
        {
            case "Perfect":
                perfectText.text = ScoreManager.Instance.GetPerfect().ToString();
                break;
            case "Great":
                greatText.text = ScoreManager.Instance.GetGreat().ToString();
                break;
            case "Good":
                goodText.text = ScoreManager.Instance.GetGood().ToString();
                break;
            case "Miss":
                missText.text = ScoreManager.Instance.GetMiss().ToString();
                break;
        }

        StopAllCoroutines();
        StartCoroutine(ShowJudgement(result));
    }

    private IEnumerator ShowJudgement(string result)
    {
        judgementText.text = result;
        judgementText.gameObject.SetActive(true);

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

        yield return new WaitForSeconds(0.5f);

        judgementText.gameObject.SetActive(false);
        comboText.gameObject.SetActive(false);
    }

}
