#ifndef TOON_SHADER_INCLUDE_INCLUDED
#define TOON_SHADER_INCLUDE_INCLUDED

// URP Include
#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"

#if defined(CUSTOM_TOON_PASS_UNIVERSAL_SHADOW_CASTER)
    #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Shadows.hlsl"
    #if defined(LOD_FADE_CROSSFADE)
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/LODCrossFade.hlsl"
    #endif
#endif

// Custom Include
#include "Macro.hlsl"
#include "Header.hlsl"

#if defined(_CUSTOM_TOON_CURVED)
    #include "VertexCurve.hlsl"
#endif

#endif