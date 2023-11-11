using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;


public class ShaderKeywordWindow : EditorWindow
{
    // メニューに追加
    [MenuItem("Window/ShaderKeywordControlWindow")]
    public static void ShowWindow()
    {
        // ウィンドウの作成
        var window = GetWindow<ShaderKeywordWindow>();
        // ウィンドウタイトル
        window.titleContent = new GUIContent("ShaderKeywordControl");
    }

    private void OnEnable()
    {
        var root = CreateGUI();

        foreach (var VARIABLE in root)
        {
            rootVisualElement.Add(VARIABLE);
        }
    }

    private VisualElement[] CreateGUI()
    {
        var enableButton = new Button { text = "EnableCurveKeyword" };
        var disableButton = new Button { text = "DisableCurveKeyword" };

        enableButton.clicked += () => Shader.EnableKeyword("_CUSTOM_TOON_CURVED");
        disableButton.clicked += () => Shader.DisableKeyword("_CUSTOM_TOON_CURVED");

        return new VisualElement[] { enableButton, disableButton };
    }
}
