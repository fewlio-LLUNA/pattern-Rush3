using UnityEngine;

[CreateAssetMenu(fileName = "NoteSpeedData", menuName = "RhythmGame/NoteSpeedData")]
public class NoteSpeedData : ScriptableObject
{
    [Range(0.1f, 6.0f)]
    public float noteSpeedMultiplier = 2.0f; // ユーザー向け表示（2.0倍速）
}
