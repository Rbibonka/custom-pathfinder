Shader "Custom/URP/GrassWind_Lit_Fixed"
{
    Properties
    {
        _BaseMap ("Base Map", 2D) = "white" {}
        _BaseColor ("Base Color", Color) = (1,1,1,1)

        _WindStrength ("Wind Strength", Float) = 0.3
        _WindSpeed ("Wind Speed", Float) = 1.0
        _WindScale ("Wind Scale", Float) = 1.0
        _WindDirection ("Wind Direction (XY)", Vector) = (1,1,0,0)
    }

    SubShader
    {
        Tags
        {
            "RenderPipeline"="UniversalPipeline"
            "Queue"="Geometry"
        }

        Pass
        {
            Name "ForwardLit"
            Tags { "LightMode"="UniversalForward" }

            Cull Off // ¬ј∆Ќќ дл€ травы

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float3 normalOS   : NORMAL;
                float2 uv         : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float3 normalWS    : TEXCOORD0;
                float3 positionWS  : TEXCOORD1;
                float2 uv          : TEXCOORD2;
            };

            TEXTURE2D(_BaseMap);
            SAMPLER(sampler_BaseMap);

            CBUFFER_START(UnityPerMaterial)
                float4 _BaseColor;
                float _WindStrength;
                float _WindSpeed;
                float _WindScale;
                float4 _WindDirection;
            CBUFFER_END

            Varyings vert (Attributes v)
            {
                Varyings o;

                // Z Ч вертикаль
                float heightMask = saturate(v.positionOS.z);

                float2 windDir = normalize(_WindDirection.xy);
                float t = _Time.y * _WindSpeed;

                float wave = sin(t + dot(v.positionOS.xy, windDir) * _WindScale);

                v.positionOS.xy += windDir * wave * _WindStrength * heightMask;

                o.positionWS = TransformObjectToWorld(v.positionOS.xyz);
                o.positionHCS = TransformWorldToHClip(o.positionWS);
                o.normalWS = TransformObjectToWorldNormal(v.normalOS);
                o.uv = v.uv;

                return o;
            }

            half4 frag (Varyings i, bool isFrontFace : SV_IsFrontFace) : SV_Target
{
    half3 normal = normalize(i.normalWS);
    normal = isFrontFace ? normal : -normal;

    Light mainLight = GetMainLight();
    half NdotL = saturate(dot(normal, mainLight.direction));
    half3 ambient = SampleSH(normal);

    half3 albedo =
        SAMPLE_TEXTURE2D(_BaseMap, sampler_BaseMap, i.uv).rgb *
        _BaseColor.rgb;

    half3 color = albedo * (ambient + mainLight.color * NdotL);
    return half4(color, 1);
}


            ENDHLSL
        }
    }
}
