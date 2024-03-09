// Shader based on code provided by Pidhorskyi in this Stack Overflow answer:
// https://stackoverflow.com/a/36532246

Shader "Unlit/Stencil-1Pass"
{
    Properties
    {
        _Pass0Color ("Pass 0 Color", Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            Stencil
            {
                Ref 0
                Comp Equal
                Pass IncrSat 
                Fail IncrSat 
            }

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
            };

            fixed4 _Pass0Color;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = _Pass0Color;
                return col;
            }

            ENDCG
        }
    }
}
