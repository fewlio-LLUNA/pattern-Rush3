using UnityEngine;

[CreateAssetMenu(fileName = "PatternData", menuName = "RhythmGame/PatternData")]
public class PatternData : ScriptableObject
{
    public string patternName;
    public int[] pattern;
}
