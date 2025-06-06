using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectManager : MonoBehaviour
{
    public void OnRasenKaidanSelected()
    {
        SceneManager.LoadScene("PlayScene");
    }
}
