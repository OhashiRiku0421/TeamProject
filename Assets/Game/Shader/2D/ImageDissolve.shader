Shader "Custom/ImageDissolveOutline"
{
    Properties
    {
        [PerRendererData] _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            // URP Include
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            // Custom Include
            #include "Library/Macro.hlsl"

            struct Attribute
            {
                float4 positionOS : POSITION;
                float2 texcoord : TEXCOORD0;
                half4 vertColor : COLOR;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float2 texCoord : TEXCOORD0;
                half4 vertColor : COLOR;
            };

            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);

            // Dissolve
            TEXTURE2D(_DissolveTex);
            SAMPLER(sampler_DissolveTex);
            
            CBUFFER_START(UnityPerMaterial)

            float4 _MainTex_ST;
            half4 _TextureSampleAdd;

            // Outline
            float _OutlineRange;
            half4 _OutlineColor;
            
            // Dissolve
            float _DissolveAmount;
            float _DissolveRange;
            half4 _DissolveColor;
            
            CBUFFER_END

            Varyings vert (Attribute input)
            {
                Varyings output = (Varyings)0;

                output.positionHCS = TransformObjectToHClip(input.positionOS.xyz);
                output.texCoord = TRANSFORM_TEX(input.texcoord, _MainTex);
                output.vertColor = input.vertColor;

                return output;
            }

            half4 frag (Varyings input) : SV_Target
            {
                return _OutlineColor;
            }
            
            ENDHLSL
        }
    }
}
