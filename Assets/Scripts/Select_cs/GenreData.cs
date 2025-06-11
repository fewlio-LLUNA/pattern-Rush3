using UnityEngine;

[CreateAssetMenu(fileName = "GenreData", menuName = "RhythmGame/GenreData")]
public class GenreData : ScriptableObject
{
    public string genreName;
    public PatternData[] patterns;
}
