using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class BPMSettingManager : MonoBehaviour
{
    // 追加①：冒頭に追記
    [Header("ノーツ速度データ（ScriptableObject）")]
    public NoteSpeedData noteSpeedData;

    [Header("ノーツ速度表示UI")]
    public TMP_Text speedDisplayText;
    public float speedStep = 0.1f;

    [Header("BPMデータ（ScriptableObject）")]
    public BPMData bpmData; // InspectorでBPMDataアセットを設定

    [Header("入力フィールド")]
    public TMP_InputField bpmInputField;
    public TMP_InputField convertBPMInputField;

    [Header("ドロップダウン")]
    public TMP_Dropdown noteDivisionDropdown;

    [Header("UI")]
    public TMP_Text warningText;

    private void Start()
    {
        // PlayerPrefsからBPMを読み込む（保存されていれば）
        if (PlayerPrefs.HasKey("BPM"))
        {
            bpmData.bpm = PlayerPrefs.GetFloat("BPM");
        }

        bpmInputField.text = bpmData.bpm.ToString("F1");
        warningText.text = "";

        if (PlayerPrefs.HasKey("NoteSpeed"))
        {
            noteSpeedData.noteSpeedMultiplier = PlayerPrefs.GetFloat("NoteSpeed");
        }

        UpdateSpeedDisplay();
    }

    public void OnSaveBPMButtonPressed()
    {
        string input = bpmInputField.text;
        if (TryParseValidBPM(input, out float bpm))
        {
            bpmData.bpm = bpm;

            // 保存
            PlayerPrefs.SetFloat("BPM", bpm);
            PlayerPrefs.Save();

            warningText.text = "BPMを保存しました。";
        }
        else
        {
            warningText.text = "BPMは 0.0〜1000.0 の数字で入力してください。";
        }
    }

    public void OnConvertButtonPressed()
    {
        string bpmInput = convertBPMInputField.text;

        // ドロップダウンから数値部分（例: "16分音符" → 16）を抽出
        int selectedNote;
        if (int.TryParse(noteDivisionDropdown.options[noteDivisionDropdown.value].text.Replace("分音符", ""), out selectedNote))
        {
            if (TryParseValidBPM(bpmInput, out float baseBPM))
            {
                float converted = baseBPM * selectedNote / 16f;
                bpmData.bpm = converted;

                // 保存
                PlayerPrefs.SetFloat("BPM", converted);
                PlayerPrefs.Save();

                bpmInputField.text = converted.ToString("F1");
                warningText.text = $"変換成功: {converted:F1} BPM を保存しました。";
            }
            else
            {
                warningText.text = "変換元のBPMが不正です。";
            }
        }
        else
        {
            warningText.text = "ノート種別の選択が不正です。";
        }
    }

    //public void OnBackButtonPressed()
    //{
    //    SceneManager.LoadScene("SelectScene");
    //}

    private bool TryParseValidBPM(string input, out float bpm)
    {
        if (float.TryParse(input, out bpm))
        {
            if (bpm >= 0f && bpm <= 1000f)
                return true;
        }
        bpm = 0;
        return false;
    }

    public void OnSpeedPlusButtonPressed()
    {
        if (noteSpeedData.noteSpeedMultiplier < 6.0f)
        {
            noteSpeedData.noteSpeedMultiplier += speedStep;
            noteSpeedData.noteSpeedMultiplier = Mathf.Min(6.0f, noteSpeedData.noteSpeedMultiplier);
            SaveNoteSpeed();
        }
    }

    public void OnSpeedMinusButtonPressed()
    {
        if (noteSpeedData.noteSpeedMultiplier > 0.1f)
        {
            noteSpeedData.noteSpeedMultiplier -= speedStep;
            noteSpeedData.noteSpeedMultiplier = Mathf.Max(0.1f, noteSpeedData.noteSpeedMultiplier);
            SaveNoteSpeed();
        }
    }

    private void SaveNoteSpeed()
    {
        PlayerPrefs.SetFloat("NoteSpeed", noteSpeedData.noteSpeedMultiplier);
        PlayerPrefs.Save();
        UpdateSpeedDisplay();
        warningText.text = $"ノーツ速度を保存しました（{noteSpeedData.noteSpeedMultiplier:F1}）";
    }

    private void UpdateSpeedDisplay()
    {
        if (speedDisplayText != null)
        {
            speedDisplayText.text = $"{noteSpeedData.noteSpeedMultiplier:F1}";
        }
    }
}
