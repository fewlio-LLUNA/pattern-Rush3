using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class JacketButtonHandler : MonoBehaviour
{
    public PatternData patternData;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        SelectedPattern.pattern = patternData.pattern;
        SelectedPattern.patternName = patternData.patternName;
        SceneTransitionManager.Instance.LoadSceneAndSetSavePoint("PlayScene"); 
    }
}
