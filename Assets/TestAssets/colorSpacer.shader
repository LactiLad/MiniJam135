Shader "Custom/colorSpacer"
{
    Properties
    {
        _MainTex ("MainTex", 2D) = "white" {}
        _Threshold ("Threshold", Range(0,1)) = 0.5
        _ColorThreshold ("ColorThreshold", Range(0,1)) = 0.5
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            float _Threshold;
            float _ColorThreshold;

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                fixed4 value;
                fixed4 white = fixed4(1,1,1,1);
                fixed4 red = fixed4(1,0,0,1);
                fixed4 green = fixed4(0,1,0,1);
                fixed4 blue = fixed4(0,0,1,1);
                fixed4 black = fixed4(0,0,0,1);

                float grayscale = dot(col.rgb, float3(0.299, 0.587, 0.114));

                value = black;
                if (grayscale > _Threshold) {
                    value = white;
                    if ((abs(col.r - grayscale) + abs(col.g - grayscale) + abs(col.b - grayscale))/3 > _ColorThreshold) {
//                    if (abs(fixed4(grayscale,grayscale,grayscale,1) - col) > _ColorThreshold) {
                        if (col.r > col.g && col.r > col.b) {
                            value = red;
                        } else if (col.g > col.r && col.g > col.b) {
                            value = green;
                        } else 
                            value = blue;
                    }
                }
                return value;
            }
            ENDCG
        }
    }
}
