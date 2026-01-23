Shader "Custom/Cheng_Shadow"
{
    Properties
    {
        _MainTex ("Sprite Texture", 2D) = "white" {}
        _Alpha ("Alpha", Range(0,1)) = 0.65
        _GrayAmount ("Gray Amount", Range(0,1)) = 0.7
        _WhiteBias ("White Bias", Range(0,0.3)) = 0.1
    }

    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "RenderType"="Transparent"
            "RenderPipeline"="UniversalPipeline"
        }

        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off
        Cull Off

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);

            float _Alpha;
            float _GrayAmount;
            float _WhiteBias;

            Varyings vert (Attributes IN)
            {
                Varyings OUT;
                OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
                OUT.uv = IN.uv;
                return OUT;
            }

            half4 frag (Varyings IN) : SV_Target
            {
                half4 col = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, IN.uv);

                // 灰度
                half gray = dot(col.rgb, half3(0.3, 0.59, 0.11));
                col.rgb = lerp(col.rgb, gray.xxx, _GrayAmount);

                // 漂白
                col.rgb += _WhiteBias;

                col.a *= _Alpha;
                return col;
            }
            ENDHLSL
        }
    }
}
