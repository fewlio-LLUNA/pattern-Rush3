using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class BPMSettingManager : MonoBehaviour
{
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

    public void OnBackButtonPressed()
    {
        SceneManager.LoadScene("SelectScene");
    }

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
}
