using UnityEngine;
using System.Collections.Generic;
using System.Linq;

#region --- SEの定義 ---

// SEの種類を定義
public enum SeType
{
    Start,      // Startボタン、ジャケットボタン
    Setting,    // オプションボタン
    Speed,      // ノーツスピード変更
    Save,       // BPM保存ボタン
    Transition  // その他の画面遷移
}

// SEの種類とAudioClipを紐づけるクラス
[System.Serializable]
public class SeSoundMapping
{
    public SeType seType;
    public AudioClip audioClip;
}

#endregion

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [SerializeField] private List<SeSoundMapping> seClips;
    private AudioSource seSource;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            seSource = gameObject.AddComponent<AudioSource>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySe(SeType seType)
    {
        var clip = seClips.FirstOrDefault(m => m.seType == seType)?.audioClip;
        if (clip != null)
        {
            seSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning("SEが見つかりません: " + seType);
        }
    }
}
