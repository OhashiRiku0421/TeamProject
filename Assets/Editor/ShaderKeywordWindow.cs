using System;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;


public class ShaderKeywordWindow : EditorWindow
{
    // private FloatField _curveOffset = default;
    //
    // private Slider _slider = default;
    //
    // private readonly int _curveOffsetPropertyID = Shader.PropertyToID("_CurveOffset");
    //
    // private readonly int _curveFactorPropertyID = Shader.PropertyToID("_CurveFactor");
    
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

    private void OnGUI()
    {
        //Shader.SetGlobalFloat(_curveOffsetPropertyID, _curveOffset.value);
        //Shader.SetGlobalFloat(_curveFactorPropertyID, _slider.value);
    }

    private VisualElement[] CreateGUI()
    {
        var enableButton = new Button { text = "EnableCurveKeyword" };
        var disableButton = new Button { text = "DisableCurveKeyword" };
        //_curveOffset = new FloatField("CurveOffset");
        //_slider = new Slider("CurveFactor", 0.0F, 3.0F);

        enableButton.clicked += () => Shader.EnableKeyword("_CUSTOM_TOON_CURVED");
        disableButton.clicked += () => Shader.DisableKeyword("_CUSTOM_TOON_CURVED");

        return new VisualElement[] { enableButton, disableButton };
    }
}
