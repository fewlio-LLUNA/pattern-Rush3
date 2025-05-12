using System.Collections;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI perfectText, greatText, goodText, missText;
    public TextMeshProUGUI judgementText;

    private int perfectCount = 0, greatCount = 0, goodCount = 0, missCount = 0;

    public void DisplayJudgement(string result)
    {
        switch (result)
        {
            case "Perfect":
                perfectCount++;
                perfectText.text = "Perfect: " + perfectCount;
                break;
            case "Great":
                greatCount++;
                greatText.text = "Great: " + greatCount;
                break;
            case "Good":
                goodCount++;
                goodText.text = "Good: " + goodCount;
                break;
            case "Miss":
                missCount++;
                missText.text = "Miss: " + missCount;
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
    }
}
