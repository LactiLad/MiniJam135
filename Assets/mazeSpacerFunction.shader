Shader "Custom/colorSpacer"
{
    Properties
    {
        _MainTex ("MainTex", 2D) = "white" {}
        _WinVal ("All to White threshold", Range(0,0.303)) = 0
        _Threshold ("Black to Crimson threshold", Range(0,1)) = 0.5
        _Threshold1 ("Crimson to Orange threshold", Range(0,1)) = 0.5
        _Threshold2 ("Orange to Yellow threshold", Range(0,1)) = 0.5
        _Threshold3 ("Yellow to White threshold", Range(0,1)) = 0.5
        
        _Shade0 ("Black Color", Color) = (0,0,0,1)
        _Shade1 ("Crimson Color", Color) = (0.25,0.25,0.25,1)
        _Shade2 ("Orange Color", Color) = (0.5,0.5,0.5,1)
        _Shade3 ("Yellow Color", Color) = (0.75,0.75,0.75,1)
        _Shade4 ("White Color", Color) = (1,1,1,1)

        _Color0 ("Pink Color", Color) = (0,0,0,1)
        _Color1 ("Green Color", Color) = (0.25,0.25,0.25,1)
        _Color2 ("Blue Color", Color) = (0.5,0.5,0.5,1)

        _OrbColor0 ("Orb Color", Color) = (0.5,0.5,0.5,1)

        _ColorThreshold ("ColorThreshold", Range(0,1)) = 0.5
        _Color0Threshold ("Pink Threshold", Range(0,1)) = 0.5
        _Color1Threshold ("Green Threshold", Range(0,1)) = 0.5
        _Color2Threshold ("Blue Threshold", Range(0,1)) = 0.5
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

            float _WinVal;
            float _Threshold;
            float _Threshold1;
            float _Threshold2;
            float _Threshold3;
            float _ColorThreshold;

            fixed4 _Shade0;
            fixed4 _Shade1;
            fixed4 _Shade2;
            fixed4 _Shade3;
            fixed4 _Shade4;

            fixed4 _Color0;
            fixed4 _Color1;
            fixed4 _Color2;

            fixed4 _OrbColor0;

            float _Color0Threshold;
            float _Color1Threshold;
            float _Color2Threshold;

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
                float luminance = (col.r * 0.3) + (col.g * 0.59) + (col.b * 0.11);
                float grayscale = dot(col.rgb, float3(0.299, 0.587, 0.114));

                _ColorThreshold += _WinVal;
                _Threshold -= _WinVal;
                _Threshold1 -= _WinVal;
                _Threshold2 -= _WinVal;
                _Threshold3 -= _WinVal;

                value = _Shade0;
                if (luminance > _Threshold) {
                    value = _Shade1;
                    if (luminance > _Threshold1) {
                        value = _Shade2;
                        if (luminance > _Threshold2) {
                            value = _Shade3;
                            if (luminance > _Threshold3) {
                                value = _Shade4;
                            }
                        }
                    }
                }
                if ((abs(col.r - grayscale) + abs(col.g - grayscale) + abs(col.b - grayscale))/3 > _ColorThreshold) {
                    if (abs(dot(col.rgb, _Color0.rgb)) > _Color0Threshold) {
                        value = _Color0;
                    } else if (abs(dot(col.rgb, _Color1.rgb)) > _Color1Threshold) {
                        value = _Color1;
                    } else if (abs(dot(col.rgb, _Color2.rgb)) > _Color1Threshold) {
                        value = _Color2;
                    }
                }

                return value;
            }
            ENDCG
        }
    }
}
