using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro; // TextMeshPro を使っている場合

public class SelectManager : MonoBehaviour
{
    public GenreData[] genres;  // Inspectorに登録
    public GameObject buttonPrefab;
    public Transform buttonParent;
    public TextMeshProUGUI genreTitle;

    private int currentGenreIndex = 0;

    void Start()
    {
        ShowGenre(currentGenreIndex);
    }

    public void ShowGenre(int index)
    {
        // ボタンを全削除
        foreach (Transform child in buttonParent)
        {
            Destroy(child.gameObject);
        }

        var genre = genres[index];
        genreTitle.text = genre.genreName;

        foreach (var patternData in genre.patterns)
        {
            var btn = Instantiate(buttonPrefab, buttonParent);
            btn.GetComponentInChildren<TextMeshProUGUI>().text = patternData.patternName;

            btn.GetComponent<Button>().onClick.AddListener(() =>
            {
                // 譜面データを記録してPlaySceneへ
                SelectedPattern.pattern = patternData.pattern;
                SelectedPattern.patternName = patternData.patternName;
                SceneManager.LoadScene("PlayScene");
            });
        }
    }

    public void OnLeftButton() => ShowGenre(--currentGenreIndex < 0 ? genres.Length - 1 : currentGenreIndex);
    public void OnRightButton() => ShowGenre(++currentGenreIndex % genres.Length);
}
