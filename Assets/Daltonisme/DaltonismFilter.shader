Shader "Custom/DaltonismFilter"
{
    Properties
    {
        _MainTex ("Base (RGB)", 2D) = "white" { }
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            // Correction pour daltonisme (par exemple pour le daltonisme de type rouge-vert)
            half3 ToDaltonismFilter(half3 color)
            {
                // Ajuste les canaux pour simuler la perception des couleurs
                half3 daltonismCorrection = half3(0.5, 0.5, 1); // Exemple pour les personnes affectées par le daltonisme de type rouge-vert
                return dot(color, daltonismCorrection);
            }

            sampler2D _MainTex;

            half4 frag(v2f i) : SV_Target
            {
                half4 color = tex2D(_MainTex, i.uv);
                color.rgb = ToDaltonismFilter(color.rgb); // Applique la correction
                return color;
            }
            ENDCG
        }
    }
}