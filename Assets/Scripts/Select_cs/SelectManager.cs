using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectManager : MonoBehaviour
{
    public void OnRasenKaidanSelected()
    {
        SceneManager.LoadScene("PlayScene");
    }
    public void GoToBPMSetting()
    {
        SceneManager.LoadScene("BPMSettingScene");
    }
    public void GoToSelect()
    {
        SceneManager.LoadScene("BPMSettingScene");
    }
}
