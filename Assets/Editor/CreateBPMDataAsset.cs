#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

public class CreateBPMDataAsset
{
    [MenuItem("Tools/Create BPMData Asset")]
    public static void CreateBPMData()
    {
        BPMData asset = ScriptableObject.CreateInstance<BPMData>();
        AssetDatabase.CreateAsset(asset, "Assets/BPMData.asset");
        AssetDatabase.SaveAssets();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = asset;// aaaa
    }
}
#endif
