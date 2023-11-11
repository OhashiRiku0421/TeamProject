Shader "Custom/CurveToonShader"
{
    Properties
    {
        _BaseMap("Base Map", 2D) = "white" {}
        _BaseColor("Base Color", Color) = (1, 1, 1, 1)
        [Normal] _NormalMap("Normal Map", 2D) = "bump" {}
        _Metallic("Metallic", Range(0.0, 1.0)) = 0.0
        _Smoothness("Smoothness", Range(0.0, 1.0)) = 0.0
        
        [Space(20)]
        [Header(Vertex Curve)]
        [Toggle(_CUSTOM_TOON_CURVED)] _CUSTOM_TOON_CURVED("Curve Enable", float) = 1
        _CurveOffset ("Curve Offset", float) = 5
        _CurveFactor ("CurveFactor", Range(0.0, 3.0)) = 1.5
    }
    SubShader
    {
        // TODO: とりあえずPBRで作ってToonに変更する
        Tags
        {
            "RenderType" = "Opaque"
            "RenderPipeline" = "UniversalPipeline"
            "UniversalMaterialType" = "Lit"
            "IgnoreProjector" = "True"
            "Queue" = "Geometry"
        }
        
        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            // URP keywords
            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS
            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS_CASCADE
            #pragma multi_compile _ _ADDITIONAL_LIGHTS_VERTEX _ADDITIONAL_LIGHTS
            #pragma multi_compile_fragment _ _ADDITIONAL_LIGHT_SHADOWS
            #pragma multi_compile_fragment _ _SHADOWS_SOFT
            #pragma multi_compile_fragment _ _SCREEN_SPACE_OCCLUSION
            #pragma multi_compile _ LIGHTMAP_SHADOW_MIXING
            #pragma multi_compile _ SHADOWS_SHADOWMASK

            // Unity Keywords
            #pragma multi_compile _ DIRLIGHTMAP_COMBINED
            #pragma multi_compile _ LIGHTMAP_ON
            #pragma multi_compile_fog

            // Custom Keyward
            #pragma multi_compile _ _CUSTOM_TOON_CURVED
            #define CUSTOM_TOON_PASS_UNIVERSAL_FORWARD

            #include "Library/Pass/UniversalForward.hlsl"
            ENDHLSL
        }
    }
}
