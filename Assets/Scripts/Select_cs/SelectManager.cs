using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SelectManager : MonoBehaviour
{
    public GenreData[] genres;
    public GameObject buttonPrefab;
    public Transform buttonParent;
    public TextMeshProUGUI genreTitle;

    private int currentGenreIndex = 0;

    public void OnLeftButton()
    {
        currentGenreIndex = (currentGenreIndex - 1 + genres.Length) % genres.Length;
        ShowGenre(currentGenreIndex);
    }

    public void OnRightButton()
    {
        currentGenreIndex = (currentGenreIndex + 1) % genres.Length;
        ShowGenre(currentGenreIndex);
    }

    public void OnBPMButton()
    {
        SceneManager.LoadScene("BPMSettingScene");
    }

    void Start()
    {
        ShowGenre(currentGenreIndex);
    }

    public void ShowGenre(int index)
    {
        foreach (Transform child in buttonParent)
        {
            if (child.gameObject.CompareTag("PatternButton"))
            {
                Destroy(child.gameObject);
            }
        }

        var genre = genres[index];
        genreTitle.text = genre.genreName;

        foreach (var patternData in genre.patterns)
        {
            var btn = Instantiate(buttonPrefab, buttonParent);
            btn.tag = "PatternButton";

            btn.GetComponentInChildren<TextMeshProUGUI>().text = patternData.patternName;
            btn.GetComponent<Image>().sprite = patternData.jacketImage;

            var data = patternData;
            btn.GetComponent<Button>().onClick.AddListener(() =>
            {
                SelectedPattern.pattern = data.pattern;
                SelectedPattern.patternName = data.patternName;
                SceneManager.LoadScene("PlayScene");
            });
        }
    }
}
