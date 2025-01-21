using UnityEngine;
using UnityEditor;
using TMPro;

#if UNITY_EDITOR
public class FontChangerEditor : EditorWindow
{
    private TMP_FontAsset newFont;

    [MenuItem("Tools/TMP Font Changer")]
    public static void ShowWindow()
    {
        GetWindow<FontChangerEditor>("TMP Font Changer");
    }

    private void OnGUI()
    {
        GUILayout.Label("Change Font for All TMP Text Components", EditorStyles.boldLabel);
        newFont = (TMP_FontAsset)EditorGUILayout.ObjectField("New Font", newFont, typeof(TMP_FontAsset), false);

        if (GUILayout.Button("Change Font"))
        {
            ChangeFont();
        }
    }

    private void ChangeFont()
    {
        TMP_Text[] tmpTexts = GameObject.FindObjectsOfType<TMP_Text>();

        foreach (TMP_Text tmpText in tmpTexts)
        {
            tmpText.font = newFont;
            EditorUtility.SetDirty(tmpText);
        }

        Debug.Log("Font changed for all TMP Text components.");
    }
}
#endif